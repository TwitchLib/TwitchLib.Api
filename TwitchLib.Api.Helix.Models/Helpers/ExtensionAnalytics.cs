namespace TwitchLib.Api.Helix.Models.Helpers;

/// <summary>
/// Extension Analytics Helper object.
/// </summary>
public class ExtensionAnalytics
{
    /// <summary>
    /// Date.
    /// </summary>
    public string Date { get; protected set; }

    /// <summary>
    /// Extension Name.
    /// </summary>
    public string ExtensionName { get; protected set; }

    /// <summary>
    /// Extension Client Id.
    /// </summary>
    public string ExtensionClientId { get; protected set; }

    /// <summary>
    /// Installs count.
    /// </summary>
    public int Installs { get; protected set; }

    /// <summary>
    /// Uninstalls count.
    /// </summary>
    public int Uninstalls { get; protected set; }

    /// <summary>
    /// Activations count.
    /// </summary>
    public int Activations { get; protected set; }

    /// <summary>
    /// Unique Active Channels count.
    /// </summary>
    public int UniqueActiveChannels { get; protected set; }

    /// <summary>
    /// Renders count.
    /// </summary>
    public int Renders { get; protected set; }

    /// <summary>
    /// Unique Renders count.
    /// </summary>
    public int UniqueRenders { get; protected set; }

    /// <summary>
    /// Views count.
    /// </summary>
    public int Views { get; protected set; }

    /// <summary>
    /// Unique Viewers count.
    /// </summary>
    public int UniqueViewers { get; protected set; }

    /// <summary>
    /// Unique Interactors count.
    /// </summary>
    public int UniqueInteractors { get; protected set; }

    /// <summary>
    /// Clicks count.
    /// </summary>
    public int Clicks { get; protected set; }

    /// <summary>
    /// Clicks per interactor count.
    /// </summary>
    public double ClicksPerInteractor { get; protected set; }

    /// <summary>
    /// Interaction rate count.
    /// </summary>
    public double InteractionRate { get; protected set; }

    /// <summary>
    /// Extension Analytics processing helper
    /// </summary>
    /// <param name="row"></param>
    public ExtensionAnalytics(string row)
    {
        var p = row.Split(',');
        Date = p[0];
        ExtensionName = p[1];
        ExtensionClientId = p[2];
        Installs = int.Parse(p[3]);
        Uninstalls = int.Parse(p[4]);
        Activations = int.Parse(p[5]);
        UniqueActiveChannels = int.Parse(p[6]);
        Renders = int.Parse(p[7]);
        UniqueRenders = int.Parse(p[8]);
        Views = int.Parse(p[9]);
        UniqueViewers = int.Parse(p[10]);
        UniqueInteractors = int.Parse(p[11]);
        Clicks = int.Parse(p[12]);
        ClicksPerInteractor = double.Parse(p[13]);
        InteractionRate = double.Parse(p[14]);
    }
}
