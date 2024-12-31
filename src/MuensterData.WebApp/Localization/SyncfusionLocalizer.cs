using Syncfusion.Blazor;
using System.Resources;

namespace MuensterData.WebApp.Localization;
public class SyncfusionLocalizer : ISyncfusionStringLocalizer
{
    public string GetText(string key)
    {
        return ResourceManager.GetString(key) ?? string.Empty;
    }

    public ResourceManager ResourceManager => Resources.SfResources.ResourceManager;
}
