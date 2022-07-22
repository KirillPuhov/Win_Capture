using Settings.init;

namespace Settings
{
    public sealed class ApplicationSettings
    {
        private readonly IniFile _init = IniFile.GetInstance();

        private string _folderDirectory;


        public ApplicationSettings()
        {
            _folderDirectory = _init.ReadINI("Application", "FolderDirectory");
        }


        public string FolderDirectory
        {
            get { return _folderDirectory; }
            set
            {
                _folderDirectory = value;
                _init.Write("Application", "FolderDirectory", _folderDirectory);
            }
        }
    }
}
