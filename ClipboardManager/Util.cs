using ClipboardManager.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
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
                    if (format != null 
                        && !DataFormats.MetafilePict.Equals(format) 
                        && !"Shell Object Offsets".Equals(format)
                        && iData.GetDataPresent(format))
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

        public enum ClipboardDataFormat : int { Text = 1, Files = 2, Image = 4, Audio = 8, Unknown = 16 }

        public struct ClipboardItemData
        {
            private int formats;
            private string text;
            private Image image;
            private string toolTip;
            private Image toolTipImage;

            public int Foramts { get { return formats; } }
            public string Text { get { return text; } }
            public Image Image { get { return image; } }
            public string ToolTip { get { return toolTip; } }
            public Image ToolTipImage { get { return toolTipImage; } }

            public ClipboardItemData(int formats, string text, Image image, string toolTip, Image toolTipImage)
            {
                this.formats = formats;
                this.text = text;
                this.image = image;
                this.toolTip = toolTip;
                this.toolTipImage = toolTipImage;
            }
        }

        internal static bool isPowerOfTwo(int i)
        {
            return (i & (i - 1)) == 0;
        }

        internal static bool containsClipboardDataFormat(int formats, ClipboardDataFormat format)
        {
            return ((formats & (int)format) != 0);
        }

        internal static ClipboardItemData getItemData(IDataObject iData)
        {
            string text = null;
            Image image = null;
            string toolTip = null;
            Image toolTipImage = null;
            int formats = 0;

            if (iData != null)
            {
                if (Clipboard.ContainsAudio()) formats |= (int)ClipboardDataFormat.Audio;
                if (Clipboard.ContainsFileDropList()) formats |= (int)ClipboardDataFormat.Files;
                if (Clipboard.ContainsImage()) formats |= (int)ClipboardDataFormat.Image;
                if (Clipboard.ContainsText()) formats |= (int)ClipboardDataFormat.Text;
                if(formats == 0) formats |= (int)ClipboardDataFormat.Unknown;

                if ((formats & (formats -1)) != 0)
                {
                    text = "Multiple Data: "
                        + (containsClipboardDataFormat(formats, ClipboardDataFormat.Text) ? "Text" : "") + " | "
                        + (containsClipboardDataFormat(formats, ClipboardDataFormat.Files) ? "Files/Folders" : "") + " | "
                        + (containsClipboardDataFormat(formats, ClipboardDataFormat.Image) ? "Image" : "") + " | "
                        + (containsClipboardDataFormat(formats, ClipboardDataFormat.Audio) ? "Audio Data" : "") + " | "
                        + (containsClipboardDataFormat(formats, ClipboardDataFormat.Unknown) ? "Unknown Data" : "");
                    text = text.TrimEnd(new char[] { ' ', '|' });
                    image = Resources.multi;
                    if (containsClipboardDataFormat(formats, ClipboardDataFormat.Image))
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
                else if (containsClipboardDataFormat(formats, ClipboardDataFormat.Text))
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
                }
                else if (containsClipboardDataFormat(formats, ClipboardDataFormat.Files))
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
                }
                else if (containsClipboardDataFormat(formats, ClipboardDataFormat.Image))
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
                }
                else if (containsClipboardDataFormat(formats, ClipboardDataFormat.Audio))
                {
                    text = "Audio Data";
                    image = Resources.speaker;
                }
                else
                {
                    text = "Unknown Data";
                    image = Resources.help;
                }
            }
            ClipboardItemData itemData = new ClipboardItemData(formats, text, image, toolTip, toolTipImage);
            return itemData;
        }

        internal static ClipboardToolStripMenuItem historyItemsHaveSameContent(ToolStripItemCollection items, ClipboardToolStripMenuItem item)
        {
            if(items != null || items.Count > 0 || item != null)
            {
                foreach(ToolStripItem i in items)
                {
                    if(i != null && i is ClipboardToolStripMenuItem)
                    {
                        ClipboardToolStripMenuItem ci = (ClipboardToolStripMenuItem)i;
                        if(item.Formats == ci.Formats && item.Data != null && ci.Data != null)
                        {
                            if (dictonariesContainSameData(ci.Data, item.Data)) return ci;
                        }
                    }
                }
            }
            return null;
        }

        private static bool dictonariesContainSameData(Dictionary<string, object> d0, Dictionary<string, object> d1)
        {
            if (d0 != null && d1 != null && d0.Count == d1.Count)
            {
                foreach(string key in d0.Keys)
                {
                    if (d1.ContainsKey(key))
                    {
                        object d0Val = d0[key];
                        object d1Val = d1[key];
                        if (d0Val != null && d1Val != null)
                        {
                            Type d0ValType = d0Val.GetType();
                            Type d1ValType = d1Val.GetType();
                            if (d0ValType == d1ValType)
                            {
                                if (d0ValType.IsSerializable && d1ValType.IsSerializable)
                                {
                                    BinaryFormatter formatter = new BinaryFormatter();
                                    MemoryStream d0Stream = new MemoryStream();
                                    formatter.Serialize(d0Stream, d0Val);
                                    formatter = new BinaryFormatter();
                                    MemoryStream d1Stream = new MemoryStream();
                                    formatter.Serialize(d1Stream, d1Val);
                                    byte[] d0ValBytes = d0Stream.ToArray();
                                    byte[] d1ValBytes = d1Stream.ToArray();
                                    if (CompareArrays(d0ValBytes, d1ValBytes)) continue;
                                }
                            }
                        }
                    }
                    return false;
                }
                return true;
            }
            return false;
        }

        private static bool CompareArrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
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
            if (w <= 0) w = 1;
            if (h <= 0) h = 1;
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

        public static Version getNewestVersion()
        {
            try
            {
                WebClient client = new WebClient();
                string str = client.DownloadString(ClipboardManager.VERSION_URL);
                Version newestVersion = null;
                Version.TryParse(str, out newestVersion);
                return newestVersion;
            }
            catch
            {
                return null;
            }
        }

        public static Tuple<string, string, string> checkForUpdate()
        {
            WebClient client = new WebClient();
            string title;
            string text;
            string newVersionAvailable = null;

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            Version newestVersion = getNewestVersion();
            if (newestVersion == null)
            {
                title = "Connection Error";
                text = "Unable to request information";
                return Tuple.Create(title, text, newVersionAvailable);
            }
            else if (newestVersion > version)
            {
                title = "New Version available";
                newVersionAvailable = Util.TrimVersion(newestVersion);
                text = "Version " + newVersionAvailable + " iss available";
            }
            else
            {
                title = "No Updates available";
                text = "Version " + Util.TrimVersion(version) + " is up-to-date";
            }
            return Tuple.Create(title, text, newVersionAvailable);
        }

        public static string TrimVersion(Version v)
        {
            string s = v.ToString();
            while (s.EndsWith(".0"))
            {
                s = s.Substring(0, s.Length - 2);
            }
            return s;
        }

        internal static void openLeftClickContextMenu(NotifyIcon ni, ClipboardContextMenu leftContextMenu)
        {
            ContextMenuStrip rightContextMenu = ni.ContextMenuStrip;
            ni.ContextMenuStrip = leftContextMenu;
            leftContextMenu.MousePos = Cursor.Position;
            MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(ni, null);
            ni.ContextMenuStrip = rightContextMenu;
        }

        internal static string getTempFile()
        {
            try { return Path.GetTempFileName(); }
            catch { return null; }
        }

        internal static string getTempFolder()
        {
            try { return Path.GetTempPath(); }
            catch { return null; }
        }


        [DllImport("user32.dll")]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
