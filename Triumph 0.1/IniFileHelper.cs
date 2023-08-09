using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Triumph_0._1
{
    internal class IniFileHelper
    {
        private readonly string path;

        public IniFileHelper(string path)
        {
            this.path = "../../" + path;
        }

        // 读取INI文件中的指定键的值
        public string ReadIniValue(string group, string key)
        {
            StringBuilder value = new StringBuilder(255);
            GetPrivateProfileString(group, key, "", value, 255, path);
            return value.ToString();
        }

        // 写入INI文件中的指定键的值
        public void WriteIniValue(string group, string key, string newValue)
        {
            WritePrivateProfileString(group, key, newValue, path);
        }

        // P/Invoke声明
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string group, string key, string value, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string group, string key, string defaultValue, StringBuilder value, int size, string filePath);
    }
}
