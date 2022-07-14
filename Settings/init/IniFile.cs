using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Settings.init
{
    public class IniFile
    {
        private readonly string _path;

        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string iniPath) =>
            _path = new FileInfo(iniPath).FullName.ToString();

        public string ReadINI(string section, string key)
        {
            var _retVal = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", _retVal, 255, _path);
            return _retVal.ToString();
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _path);
        }

        public void DeleteKey(string key, string section = null)
        {
            Write(section, key, null);
        }

        public void DeleteSection(string section = null)
        {
            Write(section, null, null);
        }

        public bool KeyExists(string key, string section = null)
        {
            return ReadINI(section, key).Length > 0;
        }


        private static readonly Lazy<IniFile> _lazy =
            new Lazy<IniFile>(() => new IniFile(Environment.CurrentDirectory + "\\settings.ini"));

        public static IniFile GetInstance()
        {
            return _lazy.Value;
        }
    }
}
