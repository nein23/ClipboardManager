using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ClipboardManager.Util
{
    public static class HotkeyUtil
    {

        public enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        public struct HotkeyStruct
        {
            public int mod;
            public int key;

            public HotkeyStruct(int mod, int key)
            {
                this.mod = mod;
                this.key = key;
            }
        }


        internal static void RegisterAllHotkeys(IntPtr handle, List<HotkeyStruct> hks)
        {
            UnregisterAllHotKeys(handle, hks.Count);
            for(int i = 0; i < hks.Count; i++)
            {
                HotkeyStruct hk = hks[i];
                if (hk.mod != 0 || hk.key != 0)
                {
                    RegisterHotKey(handle, i, hk.mod, hk.key);
                }
            }
        }

        internal static void UnregisterAllHotKeys(IntPtr handle, int count)
        {
            for (int i = 0; i < count; i++)
            {
                UnregisterHotKey(handle, i);
            }
        }

        #region PInvoke

        [DllImport("user32.dll")]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion
    }
}
