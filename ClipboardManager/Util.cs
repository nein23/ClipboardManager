using ClipboardManager.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardManager
{
    class Util
    {
        private static readonly int maxImg = 400;
        
        public enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        internal static string getStringPreview(string str, int length, bool showLineStart)
        {
            string preview = null;
            if (str != null)
            {
                string[] lines = str.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length > 0)
                {
                    preview = lines[0];
                    preview = preview.TrimStart(' ');
                    int plength = preview.Length;
                    if (plength > length)
                    {
                        if (showLineStart) preview = preview.Substring(0, length);
                        else preview = preview.Substring(preview.Length - length, length);
                    }
                    if (lines.Length > 1 || plength > length) preview += "...";
                }
            }
            return preview;
        }

        internal static string createToolTip(string[] strArr, int width, int height, bool showLineBegin)
        {
            bool empty = true;
            for (int i = 0; i < strArr.Length; i++)
            {
                string str = strArr[i];
                if ("".Equals(str)) strArr[i] = null;
                else
                {
                    empty = false;
                    break;
                }
            }
            if (empty) return null;
            for (int i = strArr.Length-1; i >= 0; i--)
            {
                string str = strArr[i];
                if ("".Equals(str)) strArr[i] = null;
                else break;
            }
            string toolTip = null;
            int lines = 0;
            for (int i = 0; i < strArr.Length; i++)
            {
                if (lines < height)
                {
                    string line = strArr[i];
                    if (line == null) continue;
                    if(lines == 0) line = line.TrimStart(' ');
                    int length = line.Length;
                    if (length > width)
                    {
                        if (showLineBegin) toolTip += line.Substring(0, width) + "...";
                        else toolTip += "..." + line.Substring(strArr[i].Length - width, width);
                    }
                    else toolTip += line;
                    lines++;
                    if (i < strArr.Length - 1 && lines < height && strArr[i+1] != null) toolTip += "\n";
                }
            }
            return toolTip;
        }

        internal static Dictionary<string, object> getContents(IDataObject iData)
        {
            if (iData != null)
            {
                Dictionary<string, object> contents = new Dictionary<string, object>();
                foreach (string format in iData.GetFormats())
                {
                    if (!DataFormats.MetafilePict.Equals(format) && iData.GetDataPresent(format))
                    {
                        try
                        {
                            object o = iData.GetData(format);
                            if (o != null) contents.Add(format, o);
                        }
                        catch { }
                    }
                }
                return contents;
            }
            return null;
        }

        internal static Tuple<string, Image, string, Image, int> getItemData(IDataObject iData)
        {
            string text = null;
            Image image = null;
            string toolTip = null;
            Image toolTipImage = null;
            int type = -1;
            if (iData != null)
            {
                bool audio = false;
                bool fdl = false;
                bool img = false;
                bool txt = false;
                bool unknown = false;
                int found = 0;
                if (Clipboard.ContainsAudio()) { found += 1; audio = true; }
                if (Clipboard.ContainsFileDropList()) { found += 1; fdl = true; }
                if (Clipboard.ContainsImage()) { found += 1; img = true; }
                if (Clipboard.ContainsText()) { found += 1; txt = true; }
                if(found == 0) { found += 1; unknown = true; }
                if (found > 1)
                {
                    text = "Multiple Data: "
                        + (txt ? "Text" : "") + " | "
                        + (fdl ? "Files/Folders" : "") + " | "
                        + (img ? "Image" : "") + " | "
                        + (audio ? "Audio Data" : "") + " | "
                        + (unknown ? "Unknown Data" : "");
                    text = text.TrimEnd(new char[] { ' ', '|' });
                    image = Resources.multi;
                    type = ClipboardToolStripMenuItem.TYPE_MULTI;
                    if (img)
                    {
                        Image originalImage = null;
                        if (iData.GetDataPresent(DataFormats.Dib))
                        {
                            byte[] dib = ((System.IO.MemoryStream)iData.GetData(DataFormats.Dib)).ToArray();
                            originalImage = byteToBmp(dib);
                        }
                        if (originalImage == null) originalImage = Clipboard.GetImage();

                        if (originalImage != null)
                        {
                            toolTipImage = scaleImage(originalImage, maxImg, maxImg);
                        }
                    }
                    else if (iData.GetDataPresent(DataFormats.Text))
                    {
                        string data = iData.GetData(DataFormats.Text) as string;
                        string[] split = data.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                        toolTip = Util.createToolTip(split, 50, 20, true);
                    }
                    else if (iData.GetDataPresent(DataFormats.FileDrop))
                    {
                        string[] files = iData.GetData(DataFormats.FileDrop) as string[];
                        toolTip = Util.createToolTip(files, 50, 20, false);
                    }
                }
                else if (txt)
                {
                    if (iData.GetDataPresent(DataFormats.Text))
                    {
                        string data = iData.GetData(DataFormats.Text) as string;
                        if (data != null)
                        {
                            string preview = Util.getStringPreview(data, 50, true);
                            if (preview != null) text = preview;
                            string[] split = data.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                            toolTip = Util.createToolTip(split, 50, 20, true);
                        }
                    }
                    if (text == null) text = "Text";
                    image = Resources.txt;
                    type = ClipboardToolStripMenuItem.TYPE_TEXT;
                }
                else if (fdl)
                {
                    if (iData.GetDataPresent(DataFormats.FileDrop))
                    {
                        string[] files = iData.GetData(DataFormats.FileDrop) as string[];
                        if (files.Length > 0)
                        {
                            if (files.Length == 1) text = Util.getStringPreview(files[0], 50, false);
                            else text = files.Length + " Files/Folders";
                        }
                        toolTip = Util.createToolTip(files, 50, 20, false);
                    }
                    if (text == null) text = "Files/Folders";
                    image = Resources.fileFolder;
                    type = ClipboardToolStripMenuItem.TYPE_FILES;
                }
                else if (img)
                {
                    Image originalImage = null;
                    if (iData.GetDataPresent(DataFormats.Dib))
                    {
                        byte[] dib = ((System.IO.MemoryStream)iData.GetData(DataFormats.Dib)).ToArray();
                        originalImage = byteToBmp(dib);
                    }

                    if (originalImage == null) originalImage = Clipboard.GetImage();

                    if (originalImage != null)
                    {
                        image = scaleImage(originalImage, 16, 16);
                        toolTipImage = scaleImage(originalImage, maxImg, maxImg);
                        if (image != null)
                        {
                            text = "Image - " + originalImage.Width + " x " + originalImage.Height + " " + originalImage.PixelFormat;
                        }
                    }
                    if (text == null) text = "Image";
                    if (image == null) image = Resources.image;
                    toolTip = "Image";
                    type = ClipboardToolStripMenuItem.TYPE_IMAGES;
                }
                else if (audio)
                {
                    text = "Audio Data";
                    image = Resources.speaker;
                    type = ClipboardToolStripMenuItem.TYPE_AUDIO;
                }
                else
                {
                    text = "Unknown Data";
                    image = Resources.help;
                    type = ClipboardToolStripMenuItem.TYPE_UNKNOWN;
                }
            }
            return new Tuple<string, Image, string, Image, int>(text, image, toolTip, toolTipImage, type);
        }

        internal static Image scaleImage(Image image, int maxW, int maxH)
        {
            int w = image.Width;
            int h = image.Height;
            if (w >= h)
            {
                if (w > maxW)
                {
                    double factor = (double)w / (double)maxW;
                    w = maxW;
                    h = Convert.ToInt32(h / factor);
                }
            }
            else
            {
                if (h > maxH)
                {
                    double factor = (double)h / (double)maxH;
                    w = Convert.ToInt32(w / factor);
                    h = maxH;
                }
            } 
            return new Bitmap(image, new Size(w, h));
        }

        private static Bitmap byteToBmp(byte[] dib)
        {
            int width = BitConverter.ToInt32(dib, 4);
            int height = BitConverter.ToInt32(dib, 8);
            short bpp = BitConverter.ToInt16(dib, 14);
            if (bpp == 32)
            {
                GCHandle gch = GCHandle.Alloc(dib, GCHandleType.Pinned);
                Bitmap bmp = null;
                try
                {
                    var ptr = new IntPtr((long)gch.AddrOfPinnedObject() + 40);
                    bmp = new Bitmap(width, height, width * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, ptr);
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
                    return new Bitmap(bmp);
                }
                finally
                {
                    gch.Free();
                    if (bmp != null) bmp.Dispose();
                }
            }
            return null;
        }


        [DllImport("user32.dll")]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
