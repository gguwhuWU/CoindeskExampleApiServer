using CoindeskExampleApiServer.Models.DB;
using CoindeskExampleApiServer.Models.Enum;

namespace CoindeskExampleApiServer.Extensions
{
    static class CopilotExtension
    {
        public static string GetDisplayName(this Copilot data, SupportedLanguageType supportedLanguageType)
        {
            switch (supportedLanguageType)
            {
                case SupportedLanguageType.English_US:
                    {
                        return data.USName;
                    }
                case SupportedLanguageType.Chinese_TW:
                    {
                        return data.ZWName;
                    }
                case SupportedLanguageType.Japanese_JP:
                    {
                        return data.JPName;
                    }
                default:
                    {
                        return data.USName;
                    }
            }
        }
    }
}
