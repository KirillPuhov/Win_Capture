using ScreenRecorderLib;
using Settings.init;
using System;

namespace Settings
{
    public sealed class RecorderSettings
    {
        private readonly IniFile _init = IniFile.GetInstance();

        private RecorderOptions _options;

        private AudioBitrate  _bitrate;
        private AudioChannels _channels;
        private bool          _isAudioEnabled;
        private int           _fps;

        private bool   _isMouseClicksDetected;
        private bool   _isMousePointerEnabled;
        private string _mouseLeftClickDetectionColor;
        private string _mouseRightClickDetectionColor;

        public RecorderSettings()
        {
            Enum.TryParse(_init.ReadINI("Recorder", "Bitrate"), out AudioBitrate bitrate);
            _bitrate = bitrate;

            Enum.TryParse(_init.ReadINI("Recorder", "Channels"), out AudioChannels channels);
            _channels = channels;

            bool.TryParse(_init.ReadINI("Recorder", "IsAudioEnabled"), out bool isAudioEnabled);
            _isAudioEnabled = isAudioEnabled;

            int.TryParse(_init.ReadINI("Recorder", "Fps"), out int fps);
            _fps = fps;

            bool.TryParse(_init.ReadINI("Recorder", "IsMouseClicksDetected"), out bool isMouseClicksDetected);
            _isMouseClicksDetected = isMouseClicksDetected;

            bool.TryParse(_init.ReadINI("Recorder", "IsMousePointerEnabled"), out bool isMousePointerEnabled);
            _isMousePointerEnabled = isMousePointerEnabled;

            _mouseLeftClickDetectionColor = _init.ReadINI("Recorder", "MouseLeftClickDetectionColor");
            _mouseRightClickDetectionColor = _init.ReadINI("Recorder", "MouseRightClickDetectionColor");
        }


        private AudioBitrate Bitrate
        {
            get { return _bitrate; }
            set
            {
                _bitrate = value;
                _init.Write("Recorder", "Bitrate", Enum.GetName(typeof(AudioBitrate), _bitrate));
            }
        }

        private AudioChannels Channels
        {
            get { return _channels; }
            set
            {
                _channels = value;
                _init.Write("Recorder", "Chanels", Enum.GetName(typeof(AudioChannels), _channels));
            }
        }

        private bool IsAudioEnabled
        {
            get { return _isAudioEnabled; }
            set
            {
                _isAudioEnabled = value;
                _init.Write("Recorder", "IsAudioEnabled", _isAudioEnabled.ToString());
            }
        }

        private int Fps
        {
            get { return _fps; }
            set
            {
                _fps = value;
                _init.Write("Recorder", "Fps", _fps.ToString());
            }
        }

        
        private bool IsMouseClicksDetected
        {
            get { return _isMouseClicksDetected; }
            set
            {
                _isMouseClicksDetected = value;
                _init.Write("Recorder", "IsMouseClicksDetected", _isMouseClicksDetected.ToString());
            }
        }

        private bool IsMousePointerEnabled
        {
            get { return _isMousePointerEnabled; }
            set
            {
                _isMousePointerEnabled = value;
                _init.Write("Recorder", "IsMousePointerEnabled", _isMousePointerEnabled.ToString());
            }
        }

        private string MouseLeftClickDetectionColor
        {
            get { return _mouseLeftClickDetectionColor; }
            set
            {
                _mouseLeftClickDetectionColor = value;
                _init.Write("Recorder", "MouseLeftClickDetectionColor", _mouseLeftClickDetectionColor);
            }
        }

        private string MouseRightClickDetectionColor
        {
            get { return _mouseRightClickDetectionColor; }
            set
            {
                _mouseRightClickDetectionColor = value;
                _init.Write("Recorder", "MouseRightClickDetectionColor", _mouseRightClickDetectionColor);
            }
        }

        public void SetSettings(string bitrate = null, string channels = null, bool isAudioEnabled = false, string fps = null, bool isMouseClicksDetected = false, bool isMousePointerEnabled = false)
        {
            if (bitrate != null)
            {
                Enum.TryParse(bitrate, out AudioBitrate _bitrate);
                Bitrate = _bitrate;
            }

            if (channels != null)
            {
                Enum.TryParse(channels, out AudioChannels _channels);
                Channels = _channels;
            }

            int.TryParse(fps, out _fps);
            Fps = _fps;

            IsAudioEnabled        = isAudioEnabled;
            IsMouseClicksDetected = isMouseClicksDetected;
            IsMousePointerEnabled = isMousePointerEnabled;
        }

        public string[] GetSettings()
        {
            string[] settings = new string[6];
            settings.SetValue(_init.ReadINI("Recorder", "Bitrate"), 0);
            settings.SetValue(_init.ReadINI("Recorder", "Channels"), 1);
            settings.SetValue(_init.ReadINI("Recorder", "IsAudioEnabled"), 2);
            settings.SetValue(_init.ReadINI("Recorder", "Fps"), 3);
            settings.SetValue(_init.ReadINI("Recorder", "IsMouseClicksDetected"), 4);
            settings.SetValue(_init.ReadINI("Recorder", "IsMousePointerEnabled"), 5);

            return settings;
        }

        public RecorderOptions GetOptions()
        {
            _options = new RecorderOptions
            {
                AudioOptions = new AudioOptions
                {
                    Bitrate        = _bitrate,
                    Channels       = _channels,
                    IsAudioEnabled = _isAudioEnabled,
                },
                VideoEncoderOptions = new VideoEncoderOptions
                {
                    Bitrate          = 8000 * 1000,
                    Framerate        = _fps,
                    IsFixedFramerate = true,

                    Encoder = new H264VideoEncoder
                    {
                        BitrateMode    = H264BitrateControlMode.CBR,
                        EncoderProfile = H264Profile.Main,
                    },

                    IsFragmentedMp4Enabled    = true,
                    IsThrottlingDisabled      = false,
                    IsHardwareEncodingEnabled = true,
                    IsLowLatencyEnabled       = false,
                    IsMp4FastStartEnabled     = false
                },
                MouseOptions = new MouseOptions
                {
                    IsMouseClicksDetected         = _isMouseClicksDetected,
                    MouseLeftClickDetectionColor  = _mouseLeftClickDetectionColor,
                    MouseRightClickDetectionColor = _mouseRightClickDetectionColor,
                    MouseClickDetectionRadius     = 30,
                    MouseClickDetectionDuration   = 100,
                    IsMousePointerEnabled         = _isMousePointerEnabled,

                    MouseClickDetectionMode = MouseDetectionMode.Hook
                },
            };

            return _options;
        }
    }
}
