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


        public AudioBitrate Bitrate
        {
            get { return _bitrate; }
            set
            {
                _bitrate = value;
                _init.Write("Recorder", "Bitrate", Enum.GetName(typeof(AudioBitrate), _bitrate));
            }
        }

        public AudioChannels Chanels
        {
            get { return _channels; }
            set
            {
                _channels = value;
                _init.Write("Recorder", "Chanels", Enum.GetName(typeof(AudioChannels), _channels));
            }
        }

        public bool IsAudioEnabled
        {
            get { return _isAudioEnabled; }
            set
            {
                _isAudioEnabled = value;
                _init.Write("Recorder", "IsAudioEnabled", _isAudioEnabled.ToString());
            }
        }

        public int Fps
        {
            get { return _fps; }
            set
            {
                _fps = value;
                _init.Write("Recorder", "Fps", _fps.ToString());
            }
        }

        
        public bool IsMouseClicksDetected
        {
            get { return _isMouseClicksDetected; }
            set
            {
                _isMouseClicksDetected = value;
                _init.Write("Recorder", "IsMouseClicksDetected", _isMouseClicksDetected.ToString());
            }
        }

        public bool IsMousePointerEnabled
        {
            get { return _isMousePointerEnabled; }
            set
            {
                _isMousePointerEnabled = value;
                _init.Write("Recorder", "IsMousePointerEnabled", _isMousePointerEnabled.ToString());
            }
        }

        public string MouseLeftClickDetectionColor
        {
            get { return _mouseLeftClickDetectionColor; }
            set
            {
                _mouseLeftClickDetectionColor = value;
                _init.Write("Recorder", "MouseLeftClickDetectionColor", _mouseLeftClickDetectionColor);
            }
        }

        public string MouseRightClickDetectionColor
        {
            get { return _mouseRightClickDetectionColor; }
            set
            {
                _mouseRightClickDetectionColor = value;
                _init.Write("Recorder", "MouseRightClickDetectionColor", _mouseRightClickDetectionColor);
            }
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
