using System.Threading.Tasks;
using Windows.Storage;
using ZiphyTestOnWinUIApp.Contracts.Services;
using ZiphyTestOnWinUIApp.Core.Helpers;

namespace ZiphyTestOnWinUIApp.Services;

public class LocalSettingsServicePackaged : ILocalSettingsService
    {
        public async Task<T> ReadSettingAsync<T>(string key)
        {
            object obj = null;

            if (ApplicationData.Current.LocalSettings.Values.TryGetValue(key, out obj))
            {
                return await Json.ToObjectAsync<T>((string)obj);
            }

            return default;
        }

        public async Task SaveSettingAsync<T>(string key, T value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = await Json.StringifyAsync(value);
        }
}