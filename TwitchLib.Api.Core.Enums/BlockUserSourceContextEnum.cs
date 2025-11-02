#nullable disable
namespace TwitchLib.Api.Core.Enums;

/// <summary>
/// Enum representing where the source context was for the blocked user
/// </summary>
public enum BlockUserSourceContextEnum
{
    /// <summary>
    /// In chat
    /// </summary>
    Chat,

    /// <summary>
    /// In a whisper
    /// </summary>
    Whisper
}
