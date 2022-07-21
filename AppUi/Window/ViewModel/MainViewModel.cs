using AppUi.Services;
using AppUi.Window.Command;
using AppUi.Window.DI;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace AppUi.Window.ViewModel
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        private readonly IDiContainer _container;

        private readonly IDialogService _dialogService;
        private readonly ICaptureService _captureService;
        private readonly ITimerService _timerService;

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

        #region ctor

        public MainViewModel(IDiContainer container)
        {
            _container = container;

            _dialogService  = _container.Navigate<IDialogService>("DialogService");
            _captureService = _container.Navigate<ICaptureService>("CaptureService");
            _timerService   = _container.Navigate<ITimerService>("TimerService");
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
                        _captureService.HideWindowAndStart(CaptureType.Screenshot, "screenshot", @"C:\Users\micro\Documents");
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
                        _timerService.Start(Timer_Tick);

                        _threadStart = new ThreadStart(() 
                            => { _captureService.Start(CaptureType.Screenvideo, "video", @"C:\Users\micro\Documents"); });

                        _thread = new Thread(_threadStart, 100);
                        _thread.Start();
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

        private RelayCommand _stopRecord;
        public RelayCommand StopRecord
        {
            get
            {
                return _stopRecord ??
                    (_stopRecord = new RelayCommand(obj => 
                    {
                        _captureService.Stop();
                        _thread?.Abort();
                        _thread = null;

                        _timerService.Stop();
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
                        _dialogService.StartProcess("explorer.exe", @"C:\Users\micro\Documents\Win_Capture");
                    }));
            }
        }

        private RelayCommand _clearTimer;
        public RelayCommand ClearTimer
        {
            get
            {
                return _clearTimer ??
                    (_clearTimer = new RelayCommand(obj => 
                    {
                        Hours   = string.Format("{0:00}", 0);
                        Minutes = string.Format("{0:00}", 0);
                        Seconds = string.Format("{0:00}", 0);
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