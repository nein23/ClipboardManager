using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ClipboardManager.Util
{
    public static class ClipboardUtil
    {

        public enum ClipboardDataFormat : int { Text = 1, Files = 2, Images = 4, Audio = 8, Unknown = 16 }

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

        internal static byte[] serializeObject(object o)
        {
            if (o != null && o.GetType().IsSerializable)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();
                formatter.Serialize(stream, o);
                return stream.ToArray();
            }
            return null;
        }

        internal static Dictionary<string, string> getHashs(Dictionary<string, byte[]> contents)
        {
            if (contents != null)
            {
                Dictionary<string, string> hashs = new Dictionary<string, string>();
                foreach (KeyValuePair<string, byte[]> pair in contents)
                {
                    if (pair.Key != null && pair.Value != null)
                    {
                        string format = pair.Key;
                        byte[] data = pair.Value;
                        string hash = createSHA1(data);
                        hashs.Add(format, hash);
                    }
                }
                return hashs;
            }
            return null;
        }

        internal static string createSHA1(byte[] data)
        {
            string hash;
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                hash = Convert.ToBase64String(sha1.ComputeHash(data));
            }
            return hash;
        }

        internal static string createMD5(byte[] data)
        {
            string hash;
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] raw = md5.ComputeHash(data);
                hash = Convert.ToBase64String(raw);
            }
            return hash;
        }

        internal static ClipboardToolStripMenuItem findDuplicate(ToolStripItemCollection items, ClipboardToolStripMenuItem item)
        {
            if (items != null || items.Count > 0 || item != null)
            {
                foreach (ToolStripItem i in items)
                {
                    if (i != null && i is ClipboardToolStripMenuItem)
                    {
                        ClipboardToolStripMenuItem ci = (ClipboardToolStripMenuItem)i;
                        if (item.Formats == ci.Formats && item.DataSize == ci.DataSize && item.Data != null && ci.Data != null)
                        {
                            if (compareHashs(ci.Hashs, item.Hashs)) return ci;
                        }
                    }
                }
            }
            return null;
        }

        private static bool compareHashs(Dictionary<string, string> d0, Dictionary<string, string> d1)
        {
            if (d0 != null && d1 != null && d0.Count == d1.Count)
            {
                foreach (string key in d0.Keys)
                {
                    if (d1.ContainsKey(key))
                    {
                        string hash0 = d0[key];
                        string hash1 = d1[key];
                        if (hash0 != null && hash1 != null)
                        {
                            if (hash0.Equals(hash1)) continue;
                        }
                    }
                    return false;
                }
                return true;
            }
            return false;
        }

        internal static bool containsClipboardDataFormat(int formats, ClipboardDataFormat format)
        {
            return ((formats & (int)format) != 0);
        }

        internal static bool isSingleDataFormat(int i)
        {
            return (i & (i - 1)) == 0;
        }

        internal static Settings.SizeFilterSettings getMaxSizeForClipboardType(int formats, Settings settings)
        {
            if (!isSingleDataFormat(formats)) return settings.SizeFilterMulti;
            else if (containsClipboardDataFormat(formats, ClipboardDataFormat.Text)) return settings.SizeFilterText;
            else if (containsClipboardDataFormat(formats, ClipboardDataFormat.Files)) return settings.SizeFilterFiles;
            else if (containsClipboardDataFormat(formats, ClipboardDataFormat.Images)) return settings.SizeFilterImages;
            else if (containsClipboardDataFormat(formats, ClipboardDataFormat.Audio)) return settings.SizeFilterAudio;
            else return settings.SizeFilterUnknown;
        }

        internal static int getClipboardFormats()
        {
            int formats = 0;
            if (Clipboard.ContainsAudio()) formats |= (int)ClipboardDataFormat.Audio;
            if (Clipboard.ContainsFileDropList()) formats |= (int)ClipboardDataFormat.Files;
            if (Clipboard.ContainsImage()) formats |= (int)ClipboardDataFormat.Images;
            if (Clipboard.ContainsText()) formats |= (int)ClipboardDataFormat.Text;
            if (formats == 0) formats |= (int)ClipboardDataFormat.Unknown;
            return formats;
        }

        #region User32.dll

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        #endregion
    }
}
