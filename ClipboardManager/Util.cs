using ClipboardManager.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardManager
{
    class Util
    {
        private static readonly int maxImg = 400;

        public static string getStringPreview(string str, int length, bool showLineStart)
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

        public static string createToolTip(string[] strArr, int width, int height, bool showLineBegin)
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

        public static Dictionary<string, object> getContents(IDataObject iData)
        {
            if (iData != null)
            {
                Dictionary<string, object> contents = new Dictionary<string, object>();
                foreach (string format in iData.GetFormats())
                {
                    if (iData.GetDataPresent(format))
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

        public static Tuple<string, string, Image> getItemData(IDataObject iData)
        {
            string text = null;
            string toolTip = null;
            Image image = null;
            if (iData != null)
            {
                bool audio = false;
                bool fdl = false;
                bool img = false;
                bool txt = false;
                int found = 0;
                if (Clipboard.ContainsAudio()) { found += 1; audio = true; }
                if (Clipboard.ContainsFileDropList()) { found += 1; fdl = true; }
                if (Clipboard.ContainsImage()) { found += 1; img = true; }
                if (Clipboard.ContainsText()) { found += 1; txt = true; }

                if (found > 1)
                {
                    text = "Multiple Formats";
                    image = Resources.help;
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
                    if(text == null) text = "Text";
                    image = Resources.txt;
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
                }
                else if (img)
                {

                    image = Clipboard.GetImage();
                    image = scaleImage(image, maxImg, maxImg);
                    if (image != null)
                    {
                        text = "Image - " + image.Width + " x " + image.Height + " " + image.PixelFormat;
                    }
                    else
                    {
                        text = "Image";
                        if (image == null) image = Resources.image;
                    }
                }
                else if (audio)
                {
                    text = "Audiostream";
                    image = Resources.speaker;
                }
                else
                {
                    text = "Unknown Data Format";
                    image = Resources.help;
                }
            }
            return new Tuple<string, string, Image>(text, toolTip, image);
        }

        public static Image scaleImage(Image image, int maxW, int maxH)
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
    }
}
