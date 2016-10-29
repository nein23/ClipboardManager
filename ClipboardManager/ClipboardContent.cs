using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ClipboardManager
{
    public class ClipboardContent
    {
        private readonly string[] _textFormats = {DataFormats.StringFormat,DataFormats.UnicodeText,DataFormats.Text,DataFormats.OemText,DataFormats.Rtf,DataFormats.CommaSeparatedValue};

        public Dictionary<string, string> Data { get; }

        public static ClipboardContent GetCurrentClipboardContent()
        {
            ClipboardContent c = new ClipboardContent();
            if (c.IsEmpty()) return null;
            return c;
        }

        private ClipboardContent()
        {
            Data = new Dictionary<string, string>();
            GetContent();
        }

        private void GetContent()
        {
            foreach (string textFormat in _textFormats)
            {
                if (Clipboard.ContainsData(textFormat))
                {
                    string text = Clipboard.GetData(textFormat) as string;
                    Data.Add(textFormat, text);
                }
            }
        }

        public void Restore()
        {
            IDataObject iData = new DataObject();
            if (!IsEmpty())
            {
                foreach (KeyValuePair<string, string> format in Data)
                {
                    if (format.Value != null)
                    {
                        iData.SetData(format.Key, format.Value);
                    }
                }
                if (iData.GetFormats().Length > 0)
                {
                    Clipboard.SetDataObject(iData);
                }
            }
        }

        public bool IsEmpty()
        {
            return Data == null ||  Data.Count == 0;
        }

        public bool HasFormat(string format)
        {
            return Data.ContainsKey(format);
        }

        public bool IsDuplicate(ClipboardContent posDup)
        {
            if (posDup == null) return false;
            if (Data.Except(posDup.Data).Concat(posDup.Data.Except(Data)).Any())
            {
                return false;
            }
            return true;
        }
    }
}
