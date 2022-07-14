using ScreenRecorderLib;
using Settings.init;
using System;

namespace Settings
{
    public sealed class RecorderSettings
    {
        private readonly IniFile _init = IniFile.GetInstance();

        private RecorderOptions _options;

        private readonly AudioBitrate  _bitrate;
        private readonly AudioChannels _channels;
        private readonly bool          _isAudioEnabled;
        private readonly int           _fps;

        private readonly bool _isMouseClicksDetected;
        private readonly bool _isMousePointerEnabled;
        private readonly string _mouseLeftClickDetectionColor;
        private readonly string _mouseRightClickDetectionColor;

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

        public AudioBitrate Bitrate  => _bitrate;

        public AudioChannels Chanels => _channels;

        public bool IsAudioEnabled   => _isAudioEnabled;

        public int Fps               => _fps;

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
