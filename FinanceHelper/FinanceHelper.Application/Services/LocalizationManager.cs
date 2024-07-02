using System.Globalization;
using System.Reflection;
using System.Text.Json;
using FinanceHelper.Application.Models;
using Serilog;

namespace FinanceHelper.Application.Services;

/// <summary>
///     Includes all localizations of the one object
/// </summary>
internal class LocalizationManager(Assembly assembly, string localizationFileDirectory)
{
    private const string LocalizationFileExtension = "json";

    /// <summary>
    ///     Localization code : Localized strings
    /// </summary>
    private readonly Dictionary<string, IList<LocalizedString>> _localizedStrings = new Dictionary<string, IList<LocalizedString>>();

    /// <summary>
    ///     Looks up a localization string value for a particular name.
    ///     Looks in the specified CultureInfo
    ///     Returns null if the resource wasn't found.
    /// </summary>
    /// <param name="keyword">String keyword</param>
    /// <param name="culture">Target culture</param>
    /// <returns>Localized value or null, if localization doesn't exist</returns>
    public string? GetString(string keyword, CultureInfo culture)
    {
        if (_localizedStrings.TryGetValue(culture.Name, out var localizedStrings) == false) localizedStrings = ReadLocalizedStringsFromFile(culture);

        return localizedStrings?.FirstOrDefault(x => x.Keyword == keyword)?.Value;
    }

    /// <summary>
    ///     Add a new localization string value for a particular name and culture.
    /// </summary>
    /// <param name="keyword">String keyword</param>
    /// <param name="value">Localized string value</param>
    /// <param name="culture">Target culture</param>
    public void AddString(string keyword, string value, CultureInfo culture)
    {
        var newLocalizedString = new LocalizedString(keyword, value);

        if (_localizedStrings.TryGetValue(culture.Name, out var localizedStrings) == false)
        {
            var newList = new List<LocalizedString>();
            newList.Add(newLocalizedString);
            _localizedStrings.Add(culture.Name, newList);
        }

        else localizedStrings.Add(newLocalizedString);
    }

    /// <summary>
    ///     Reads localized strings in localization file or return null
    /// </summary>
    /// <returns>localized strings if localization file exists</returns>
    private IList<LocalizedString>? ReadLocalizedStringsFromFile(CultureInfo culture)
    {
        var localizationFileLocation = GetLocalizationFileLocation(culture);
        if (File.Exists(localizationFileLocation) == false)
        {
            Log.Warning("Localization file doesn't exists {@LocalizationFileLocation}", localizationFileLocation);
            return null;
        }

        var fileStream = File.OpenRead(localizationFileLocation);
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, string>>(fileStream);

        return jsonData?.Select(x => new LocalizedString(x.Key, x.Value)).ToList();
    }

    private string GetLocalizationFileLocation(CultureInfo culture)
    {
        var localizationFileName = culture.TwoLetterISOLanguageName + "." + LocalizationFileExtension;
        return Path.Combine(localizationFileDirectory, localizationFileName);
    }
}