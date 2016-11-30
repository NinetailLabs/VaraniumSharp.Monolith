using VaraniumSharp.Monolith.Interfaces;

namespace VaraniumSharp.Monolith.Tests.Helpers
{
    public class PushoverConfigDummy : IPushoverConfiguration
    {
        #region Constructor

        public PushoverConfigDummy(bool enable)
        {
            IsEnabled = enable;
        }

        #endregion

        #region Properties

        public string ApiToken { get; set; }
        public string DefaultSendKey { get; set; }
        public bool IsEnabled { get; }

        #endregion
    }
}