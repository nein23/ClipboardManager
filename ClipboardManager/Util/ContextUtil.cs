using System;
using System.Reflection;
using System.Windows.Forms;

namespace ClipboardManager.Util
{
    class ContextUtil
    {
        internal static void openLeftClickContextMenu(NotifyIcon ni, ClipboardContextMenu leftContextMenu)
        {
            ContextMenuStrip rightContextMenu = ni.ContextMenuStrip;
            ni.ContextMenuStrip = leftContextMenu;
            leftContextMenu.MousePos = Cursor.Position;
            MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(ni, null);
            ni.ContextMenuStrip = rightContextMenu;
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
            for (int i = strArr.Length - 1; i >= 0; i--)
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
                    if (lines == 0) line = line.TrimStart(' ');
                    int length = line.Length;
                    if (length > width)
                    {
                        if (showLineBegin) toolTip += line.Substring(0, width) + "...";
                        else toolTip += "..." + line.Substring(strArr[i].Length - width, width);
                    }
                    else toolTip += line;
                    lines++;
                    if (i < strArr.Length - 1 && lines < height && strArr[i + 1] != null) toolTip += "\n";
                }
            }
            return toolTip;
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
    }
}
