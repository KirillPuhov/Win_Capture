using AppUi.Services;
using AppUi.Window.Command;
using Settings;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace AppUi.Window.ViewModel
{
    public sealed class ConfigurationViewModel : INotifyPropertyChanged
    {
        private readonly IDialogService _dialogService;

        private readonly ApplicationSettings _applicationSettings;
        private readonly RecorderSettings _recorderSettings;

        public ConfigurationViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            _applicationSettings = new ApplicationSettings();
            _recorderSettings    = new RecorderSettings();

            FolderDirectory = _applicationSettings.FolderDirectory;
            SetValues();
        }

        private string _folderDir;
        public string FolderDirectory
        {
            get { return _folderDir; }
            set
            {
                _folderDir = value;
                OnPropertyChanged("FolderDirectory");
            }
        }


        private string[] _audioBitrate = new string[] { "bitrate_128kbps", "bitrate_192kbps", "bitrate_96kbps", "bitrate_160kbps" };
        public string[] AudioBitrate
        {
            get { return _audioBitrate; }
            set
            {
                _audioBitrate = value;
                OnPropertyChanged("AudioBitrate");
            }
        }

        private string _audioBitrateSelected;
        public string AudioBitrateSelected
        {
            get { return _audioBitrateSelected; }
            set
            {
                _audioBitrateSelected = value;
                OnPropertyChanged("AudioBitrateSelected");
            }
        }


        private string[] _audioChannels = new string[] { "Stereo", "Mono", "FivePointOne" };
        public string[] AudioChannels
        {
            get { return _audioChannels; }
            set
            {
                _audioChannels = value;
                OnPropertyChanged("AudioChannels");
            }
        }

        private string _audioChannelSelected;
        public string AudioChannelSelected
        {
            get { return _audioChannelSelected; }
            set
            {
                _audioChannelSelected = value;
                OnPropertyChanged("AudioChannelSelected");
            }
        }


        private string _fps;
        public string FPS
        {
            get { return _fps; }
            set
            {
                _fps = value;
                OnPropertyChanged("Fps");
            }
        }


        private bool[] _isAudioEnabled = new bool[] { true, false };
        public bool[] IsAudioEnabled
        {
            get { return _isAudioEnabled; }
            set
            {
                _isAudioEnabled = value;
                OnPropertyChanged("IsAudioEnabled");
            }
        }

        private bool _isAudioEnabledSelected;
        public bool IsAudioEnabledSelected
        {
            get { return _isAudioEnabledSelected; }
            set
            {
                _isAudioEnabledSelected = value;
                OnPropertyChanged("IsAudioEnabledSelected");
            }
        }


        private bool[] _isMouseClicksDetected = new bool[] { true, false };
        public bool[] IsMouseClicksDetected
        {
            get { return _isMouseClicksDetected; }
            set
            {
                _isMouseClicksDetected = value;
                OnPropertyChanged("IsMouseClicksDetected");
            }
        }

        private bool _isMouseClicksDetectedSelected;
        public bool IsMouseClicksDetectedSelected
        {
            get { return _isMouseClicksDetectedSelected; }
            set
            {
                _isMouseClicksDetectedSelected = value;
                OnPropertyChanged("IsMouseClicksDetectedSelected");
            }
        }


        private bool[] _isMousePointerEnabled = new bool[] { true, false };
        public bool[] IsMousePointerEnabled
        {
            get { return _isMousePointerEnabled; }
            set
            {
                _isMousePointerEnabled = value;
                OnPropertyChanged("IsMousePointerEnabled");
            }
        }

        private bool _isMousePointerEnabledSelected;
        public bool IsMousePointerEnabledSelected
        {
            get { return _isMousePointerEnabledSelected; }
            set
            {
                _isMousePointerEnabledSelected = value;
                OnPropertyChanged("IsMousePointerEnabledSelected");
            }
        }


        private void SetValues() 
        {
            var _settings = _recorderSettings.GetSettings();
            AudioBitrateSelected = _settings.GetValue(0).ToString();
            AudioChannelSelected = _settings.GetValue(1).ToString();

            bool.TryParse(_settings.GetValue(2).ToString(), out bool _audioEnabledSelected);
            IsAudioEnabledSelected = _audioEnabledSelected;

            FPS = _settings.GetValue(3).ToString();

            bool.TryParse(_settings.GetValue(4).ToString(), out bool _isClicksDetectedSelected);
            IsMouseClicksDetectedSelected = _isClicksDetectedSelected;

            bool.TryParse(_settings.GetValue(5).ToString(), out bool _isPointerEnabledSelected);
            IsMousePointerEnabledSelected = _isPointerEnabledSelected;
        }


        private RelayCommand _changeFolder;
        public RelayCommand ChangeFolder
        {
            get
            {
                return _changeFolder ??
                    (_changeFolder = new RelayCommand(obj =>
                    {
                        _dialogService.ShowDialog();
                        FolderDirectory = _dialogService.Result;
                        _applicationSettings.FolderDirectory = FolderDirectory;
                    }));
            }
        }

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                    (_saveCommand = new RelayCommand(obj => 
                    {
                        _recorderSettings.SetSettings(AudioBitrateSelected, AudioChannelSelected, IsAudioEnabledSelected, FPS, IsMouseClicksDetectedSelected, IsMousePointerEnabledSelected);
                        _dialogService.ShowInfo("Сonfiguration saved");
                    }));
            }
        }

        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                    (_deleteCommand = new RelayCommand(obj => 
                    {
                        DeleteFiles(FolderDirectory + "\\Win_Capture\\Video");
                        DeleteFiles(FolderDirectory + "\\Win_Capture\\Screenshots");

                        _dialogService.ShowInfo("All files have been deleted");
                    }));
            }
        }

        private void DeleteFiles(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
