namespace TwitchLib.Api.Helix.Models.Chat;

/// <summary>
/// The colors used to highlight the announcement.
/// </summary>
public class AnnouncementColors
{
    private AnnouncementColors(string value) { Value = value; }

    /// <summary>
    /// Value
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Blue
    /// </summary>
    public static AnnouncementColors Blue { get { return new AnnouncementColors("blue"); } }

    /// <summary>
    /// Green
    /// </summary>
    public static AnnouncementColors Green { get { return new AnnouncementColors("green"); } }

    /// <summary>
    /// Orange
    /// </summary>
    public static AnnouncementColors Orange { get { return new AnnouncementColors("orange"); } }

    /// <summary>
    /// Purple
    /// </summary>
    public static AnnouncementColors Purple { get { return new AnnouncementColors("purple"); } }

    /// <summary>
    /// Primary
    /// </summary>
    public static AnnouncementColors Primary { get { return new AnnouncementColors("primary"); } }
}
