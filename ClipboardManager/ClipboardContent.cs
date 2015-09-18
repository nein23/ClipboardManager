using ClipboardManager.Properties;
using ClipboardManager.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ClipboardManager
{
    class ClipboardContent
    {
        private Dictionary<string, object> data;
        private Dictionary<string, string> hashs;
        private long size;

        private int formats;
        private string text;
        private Image image;
        private string toolTip;
        private Image toolTipImage;

        public Dictionary<string, object> Data { get { return data; } }
        public Dictionary<string, string> Hashs { get { return hashs; } }
        public long Size { get { return size; } }

        public int Foramts { get { return formats; } }
        public string Text { get { return text; } }
        public Image Image { get { return image; } }
        public string ToolTip { get { return toolTip; } }
        public Image ToolTipImage { get { return toolTipImage; } }

        public ClipboardContent(IDataObject iData)
        {
            createData(iData);
            computeHashsAndSize();
            computeItemData(iData);
        }

        private void createData(IDataObject iData)
        {
            data = new Dictionary<string, object>();
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
                        if (o != null) Data.Add(format, o);
                    }
                    catch { }
                }
            }
        }

        private void computeHashsAndSize()
        {
            if(data != null)
            {
                hashs = new Dictionary<string, string>();
                size = 0;
                foreach (KeyValuePair<string, object> entry in data)
                {
                    string format = entry.Key;
                    object obj = entry.Value;
                    if (format != null && obj != null) {
                        byte[] serObj = ClipboardUtil.serializeObject(obj);
                        if (serObj != null)
                        {
                            size += serObj.Length;
                            string hash = ClipboardUtil.createMD5(serObj);
                            if (hash != null) hashs.Add(format, hash);
                        }
                    }
                }
                if(data.Count != hashs.Count)
                {
                    data = null;
                    hashs = null;
                    size = 0;
                }
            }
        }

        private void computeItemData(IDataObject iData)
        {
            if(iData != null)
            {
                formats = ClipboardUtil.getClipboardFormats();

                if (!ClipboardUtil.isSingleDataFormat(formats)) computeMultiData(iData);
                else if (ClipboardUtil.containsClipboardDataFormat(formats, ClipboardUtil.ClipboardDataFormat.Text)) computeTextData(iData);
                else if (ClipboardUtil.containsClipboardDataFormat(formats, ClipboardUtil.ClipboardDataFormat.Files)) computeFilesData(iData);
                else if (ClipboardUtil.containsClipboardDataFormat(formats, ClipboardUtil.ClipboardDataFormat.Images)) computeImagesData(iData);
                else if (ClipboardUtil.containsClipboardDataFormat(formats, ClipboardUtil.ClipboardDataFormat.Audio)) computeAudioData(iData);
                else computeUnknownData(iData);
            }
        }

        private void computeMultiData(IDataObject iData)
        {
            text = ("Multiple Data: "
                           + (ClipboardUtil.containsClipboardDataFormat(formats, ClipboardUtil.ClipboardDataFormat.Text) ? "Text" : "") + " | "
                           + (ClipboardUtil.containsClipboardDataFormat(formats, ClipboardUtil.ClipboardDataFormat.Files) ? "Files/Folders" : "") + " | "
                           + (ClipboardUtil.containsClipboardDataFormat(formats, ClipboardUtil.ClipboardDataFormat.Images) ? "Image" : "") + " | "
                           + (ClipboardUtil.containsClipboardDataFormat(formats, ClipboardUtil.ClipboardDataFormat.Audio) ? "Audio Data" : "") + " | "
                           + (ClipboardUtil.containsClipboardDataFormat(formats, ClipboardUtil.ClipboardDataFormat.Unknown) ? "Unknown Data" : "")
                           ).TrimEnd(new char[] { ' ', '|' });
            image = Resources.multi;
            if (ClipboardUtil.containsClipboardDataFormat(formats, ClipboardUtil.ClipboardDataFormat.Images))
            {
                Image originalImage = null;
                if (iData.GetDataPresent(DataFormats.Dib))
                {
                    object dibData = iData.GetData(DataFormats.Dib);
                    if (dibData != null && dibData is MemoryStream)
                    {
                        MemoryStream dibStream = (MemoryStream)iData.GetData(DataFormats.Dib);
                        originalImage = ImageUtil.BitmapFromDIB(dibStream);
                    }
                }
                if (originalImage == null) originalImage = Clipboard.GetImage();
                if (originalImage != null) toolTipImage = ImageUtil.scaleImage(originalImage, ImageUtil.maxImg, ImageUtil.maxImg);
            }
            else if (iData.GetDataPresent(DataFormats.Text))
            {
                string data = iData.GetData(DataFormats.Text) as string;
                string[] split = data.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                toolTip = ContextUtil.createToolTip(split, 50, 20, true);
            }
            else if (iData.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = iData.GetData(DataFormats.FileDrop) as string[];
                toolTip = ContextUtil.createToolTip(files, 50, 20, false);
            }
        }

        private void computeTextData(IDataObject iData)
        {
            if (iData.GetDataPresent(DataFormats.Text))
            {
                string data = iData.GetData(DataFormats.Text) as string;
                if (data != null)
                {
                    string preview = ContextUtil.getStringPreview(data, 50, true);
                    if (preview != null) text = preview;
                    string[] split = data.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                    toolTip = ContextUtil.createToolTip(split, 50, 20, true);
                }
            }
            if (text == null) text = "Text";
            image = Resources.txt;
        }

        private void computeFilesData(IDataObject iData)
        {
            if (iData.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = iData.GetData(DataFormats.FileDrop) as string[];
                if (files.Length > 0)
                {
                    if (files.Length == 1) text = ContextUtil.getStringPreview(files[0], 50, false);
                    else text = files.Length + " Files/Folders";
                }
                toolTip = ContextUtil.createToolTip(files, 50, 20, false);
            }
            if (text == null) text = "Files/Folders";
            image = Resources.fileFolder;
        }

        private void computeImagesData(IDataObject iData)
        {
            Image originalImage = null;
            if (iData.GetDataPresent(DataFormats.Dib))
            {
                object dibData = iData.GetData(DataFormats.Dib);
                if (dibData != null && dibData is MemoryStream)
                {
                    MemoryStream dibStream = (MemoryStream)iData.GetData(DataFormats.Dib);
                    originalImage = ImageUtil.BitmapFromDIB(dibStream);
                }
            }
            if (originalImage == null) originalImage = Clipboard.GetImage();
            if (originalImage != null)
            {
                image = ImageUtil.scaleImage(originalImage, 16, 16);
                toolTipImage = ImageUtil.scaleImage(originalImage, ImageUtil.maxImg, ImageUtil.maxImg);
                if (image != null) text = "Image - " + originalImage.Width + " x " + originalImage.Height + " " + originalImage.PixelFormat;
            }
            if (text == null) text = "Image";
            if (image == null) image = Resources.image;
            toolTip = "Image";
        }

        private void computeAudioData(IDataObject iData)
        {
            text = "Audio Data";
            image = Resources.speaker;
        }

        private void computeUnknownData(IDataObject iData)
        {
            text = "Unknown Data";
            image = Resources.help;
        }

        public bool isEmpty()
        {
            return !(data != null && hashs != null && data.Count > 0);
        }
    }
}
