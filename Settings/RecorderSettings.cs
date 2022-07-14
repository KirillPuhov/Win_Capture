using ScreenRecorderLib;

namespace Settings
{
    public sealed class RecorderSettings
    {
        private RecorderOptions _options;

        private AudioBitrate  _bitrate;
        private AudioChannels _channels;
        private bool          _isAudioEnabled;
        private int           _fps;

        public RecorderSettings(AudioBitrate bitrate, AudioChannels channels, bool isAudioEnabled, int fps)
        {
            _bitrate        = bitrate;
            _channels       = channels;
            _isAudioEnabled = isAudioEnabled;
            _fps            = fps;
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
                    //Displays a colored dot under the mouse cursor when the left mouse button is pressed.	
                    IsMouseClicksDetected         = true,
                    MouseLeftClickDetectionColor  = "#FFFF00",
                    MouseRightClickDetectionColor = "#FFFF00",
                    MouseClickDetectionRadius     = 30,
                    MouseClickDetectionDuration   = 100,
                    IsMousePointerEnabled         = true,
                    /* Polling checks every millisecond if a mouse button is pressed.
                       Hook is more accurate, but may affect mouse performance as every mouse update must be processed.*/
                    MouseClickDetectionMode = MouseDetectionMode.Hook
                },
            };

            return _options;
        }
    }
}
