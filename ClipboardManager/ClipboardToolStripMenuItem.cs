using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ClipboardManager.Properties;

namespace ClipboardManager
{
    internal class ClipboardToolStripMenuItem : ToolStripMenuItem
    {
        public ClipboardContent Content { get; }

        private ToolTip _toolTip;
        private int _toolTipOffset;

        public ClipboardToolStripMenuItem(ClipboardContent content)
        {
            Content = content;

            Init();

            AutoToolTip = false;

            MouseEnter += ClipboardToolStripMenuItem_MouseEnter;
            MouseLeave += ClipboardToolStripMenuItem_MouseLeave;
        }
        
        private void Init()
        {
            if (!Content.IsEmpty())
            {
                Image = Resources.txt;
                string text = null;
                if (Content.HasFormat(DataFormats.UnicodeText))
                    text = Content.Data[DataFormats.UnicodeText];
                else if (Content.HasFormat(DataFormats.Text))
                    text = Content.Data[DataFormats.Text];
                else if (Content.HasFormat(DataFormats.StringFormat))
                    text = Content.Data[DataFormats.StringFormat];
                else if (Content.HasFormat(DataFormats.Rtf))
                    text = Content.Data[DataFormats.Rtf];
                else if (Content.HasFormat(DataFormats.CommaSeparatedValue))
                    text = Content.Data[DataFormats.CommaSeparatedValue];
                else if (Content.HasFormat(DataFormats.Html))
                    text = Content.Data[DataFormats.Html];

                if (text != null)
                {
                    Text = CreatePreviewString(text, 50, true);
                    ToolTipText = CreateToolTipText(text.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None).ToList(),
                        50, 20, true);
                }
            }

            _toolTip = new ToolTip();
            if (ToolTipText != null)
            {
                string[] split = ToolTipText.Split(new [] {"\r\n", "\n"}, StringSplitOptions.None);
                int numLines = split.Length;
                _toolTipOffset = -1 * (5 + numLines * 15);
            }
            else
            {
                _toolTipOffset = 0;
            }
        }

        #region Handler

        protected override void Dispose(bool disposing)
        {
            _toolTip.Dispose();
            base.Dispose(disposing);
        }

        private void ClipboardToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            _toolTip?.Show(ToolTipText, Owner, Bounds.X, _toolTipOffset);
        }

        private void ClipboardToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            _toolTip.Hide(Owner);
        }

        private string CreateToolTipText(List<string> lines, int width, int height, bool showLineBegin)
        {
            lines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
            if (lines.Count == 0)
            {
                return null;
            }
            if (lines.Count > height)
            {
                lines = lines.GetRange(0, height);
                lines.Add("...");
            }

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                if (line == null) continue;
                if (line.Length > width)
                {
                    if (showLineBegin) lines[i] = line.Substring(0, width) + "...";
                    else lines[i] = "..." + line.Substring(line.Length - width, width);
                }
                else
                {
                    lines[i] += line;
                }
            }
            return string.Join("\n", lines);
        }
        
        private string CreatePreviewString(string str, int length, bool showLineStart)
        {
            string[] lines = str.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length > 0)
            {
                string preview = lines[0].Trim();
                if (preview.Length > length)
                {
                    preview = showLineStart
                        ? preview.Substring(0, length) + "..."
                        : "..." + preview.Substring(preview.Length - length, length);
                }
                return preview;
            }
            return null;
        }

        #endregion
    }
}
