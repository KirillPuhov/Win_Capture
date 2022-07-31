using AppUi.Services;
using AppUi.View;
using AppUi.Window.Command;
using AppUi.Window.DI;
using Domain.Models;
using Settings;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace AppUi.Window.ViewModel
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        private readonly IDiContainer _container;

        private readonly IDialogService  _dialogService;
        private readonly ICaptureService _captureService;
        private readonly IRecentService  _recentService;

        private readonly ApplicationSettings _applicationSettings;


        private ITimerService _timerService;


        private string _hours = string.Format("{0:00}", 0);
        public string Hours
        {
            get { return _hours; }
            set
            {
                _hours = value;
                OnPropertyChanged("Hours");
            }
        }

        private string _minutes = string.Format("{0:00}", 0);
        public string Minutes
        {
            get { return _minutes; }
            set
            {
                _minutes = value;
                OnPropertyChanged("Minutes");
            }
        }

        private string _seconds = string.Format("{0:00}", 0);
        public string Seconds
        {
            get { return _seconds; }
            set
            {
                _seconds = value;
                OnPropertyChanged("Seconds");
            }
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

        private bool _isPause = false;
        public bool IsPause
        {
            get { return _isPause; }
            set
            {
                _isPause = value;
            }
        }

        private bool _isRecord = false;
        public bool IsRecord
        {
            get { return _isRecord; }
            set
            {
                _isRecord = value;
            }
        }

        public ObservableCollection<IOutFile> RecentList { get; set; }

        #region ctor

        public MainViewModel(IDiContainer container)
        {
            _container = container;

            _dialogService  = _container.Navigate<IDialogService>("DialogService");
            _captureService = _container.Navigate<ICaptureService>("CaptureService");
            _recentService  = _container.Navigate<IRecentService>("RecentService");

            _applicationSettings = new ApplicationSettings();
            FolderDirectory = _applicationSettings.FolderDirectory;
            RecentList = _recentService.RecentFiles;
        }

        #endregion

        private RelayCommand _screenshot;
        public RelayCommand Screenshot
        {
            get
            {
                return _screenshot ??
                    (_screenshot = new RelayCommand(obj => 
                    {
                        _captureService.HideWindowAndStart(CaptureType.Screenshot, "screenshot", FolderDirectory);

                        var _outFile = _captureService.GetOutFile();
                        _recentService.AddRecentFile(_outFile);
                    }));
            }
        }

        private ThreadStart _threadStart;
        private Thread _thread;

        private RelayCommand _startRecord;
        public RelayCommand StartRecord
        {
            get
            {
                return _startRecord ??
                    (_startRecord = new RelayCommand(obj => 
                    {
                        if (!IsRecord)
                        {
                            _timerService = _container.Navigate<ITimerService>("TimerService");
                            _timerService.Start(Timer_Tick);

                            _threadStart = new ThreadStart(()
                                => { _captureService.Start(CaptureType.Screenvideo, "video", FolderDirectory); });

                            _thread = new Thread(_threadStart, 100);
                            _thread.Start();
                            IsRecord = true;
                        }
                        else
                        {
                            _captureService.Stop();
                            _thread?.Abort();
                            _thread = null;

                            _timerService.Stop();   
                            ClearTimerMethod();

                            var _outFile = _captureService.GetOutFile();
                            _recentService.AddRecentFile(_outFile);

                            IsRecord = false;
                        }
                    }));
            }
        }

        private int _hour   = 0;
        private int _minute = 0;
        private int _second = 0;

        private void Timer_Tick(object sender, EventArgs e)
        {
            _second++;

            if (_second == 60)
            {
                _minute += 1;
                _second  = 0;
            }

            if (_minute == 60)
            {
                _hour  += 1;
                _minute = 0;
            }

            Hours   = string.Format("{0:00}", _hour);
            Minutes = string.Format("{0:00}", _minute);
            Seconds = string.Format("{0:00}", _second);
        }

        private void ClearTimerMethod()
        {
            _hour   = 0;
            _minute = 0;
            _second = 0;

            Hours = string.Format("{0:00}", 0);
            Minutes = string.Format("{0:00}", 0);
            Seconds = string.Format("{0:00}", 0);
        }

        private RelayCommand _pause;
        public RelayCommand Pause
        {
            get
            {
                return _pause ??
                    (_pause = new RelayCommand(obj => 
                    {
                        if (!IsPause)
                        {
                            _captureService.Pause();
                            _timerService.Pause();
                            IsPause = true;
                        }
                        else
                        {
                            _captureService.Resume();
                            _timerService.Resume();
                            IsPause = false;
                        }
                    }));
            }
        }

        private RelayCommand _clearRecent;
        public RelayCommand ClearRecent
        {
            get
            {
                return _clearRecent ??
                    (_clearRecent = new RelayCommand(obj =>
                    {
                        _recentService.ClearRecentList();
                    }));
            }
        }

        private RelayCommand _openFolder;
        public RelayCommand OpenFolder
        {
            get
            {
                return _openFolder ??
                    (_openFolder = new RelayCommand(obj => 
                    {
                        _dialogService.OpenFolder();
                    }));
            }
        }

        private RelayCommand _openConfiguration;
        public RelayCommand OpenConfiguration
        {
            get
            {
                return _openConfiguration ??
                    (_openConfiguration = new RelayCommand(obj => 
                    {
                        _dialogService.OpenView(new ConfigurationWindow());
                    }));
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


//TODO: исправить завершение без выключения записи
//TODO: добавить try catch