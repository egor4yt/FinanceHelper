using System.Collections.Concurrent;
using System.Reflection;
using FinanceHelper.Application.Services.Localization.Interfaces;

namespace FinanceHelper.Application.Services.Localization;

internal class StringLocalizerFactory : IStringLocalizerFactory
{
    private const string LocalizationFilesRelativePath = "Localization";
    private readonly ConcurrentDictionary<string, IStringLocalizer> _localizerCache = new ConcurrentDictionary<string, IStringLocalizer>();

    public IStringLocalizer Create(Type objectToLocalizationType)
    {
        if (_localizerCache.TryGetValue(objectToLocalizationType.AssemblyQualifiedName!, out var localizer) == false)
        {
            var typeInfo = objectToLocalizationType.GetTypeInfo();
            var baseName = GetLocalizationFilePrefix(typeInfo);

            localizer = CreateLocalizationManagerStringLocalizer(baseName);

            _localizerCache.TryAdd(objectToLocalizationType.AssemblyQualifiedName!, localizer);
        }


        return localizer;
    }

    /// <summary>Creates a <see cref="LocalizationManager" /></summary>
    /// <param name="baseName">The base name of the localization file to search for.</param>
    /// <returns>A <see cref="LocalizationManager" /> for the given and <paramref name="baseName" />.</returns>
    private static LocalizationManagerStringLocalizer CreateLocalizationManagerStringLocalizer(string baseName)
    {
        var localizationManager = new LocalizationManager(baseName);
        return new LocalizationManagerStringLocalizer(localizationManager);
    }

    /// <summary>
    ///     Gets the localization file prefix used to look up the localization file.
    /// </summary>
    /// <returns>The prefix for localization file lookup.</returns>
    private static string GetLocalizationFilePrefix(Type typeInfo)
    {
        var baseNamespace = Path.GetDirectoryName(typeInfo.Assembly.Location)!;

        var localizationRelativePath = GetLocalizationRelativePath();

        if (typeInfo == null) throw new NullReferenceException($"{nameof(typeInfo)} was null");
        if (string.IsNullOrWhiteSpace(localizationRelativePath)) return typeInfo.FullName!;

        return Path.Combine(baseNamespace, localizationRelativePath, TrimPrefix(typeInfo.Name, baseNamespace + "."));
    }

    private static string TrimPrefix(string name, string prefix)
    {
        return name.StartsWith(prefix, StringComparison.Ordinal) ? name[prefix.Length..] : name;
    }

    private static string GetLocalizationRelativePath()
    {
        var localizationLocationLocation = LocalizationFilesRelativePath;

        localizationLocationLocation = localizationLocationLocation
            .Replace(Path.DirectorySeparatorChar, '.')
            .Replace(Path.AltDirectorySeparatorChar, '.');

        return localizationLocationLocation;
    }
}