using System.Collections.Generic;

namespace TwitchLib.Api.Helix.Models.Clips.CreateClipFromVod;

public class CreatedClipFromVodRequest
{
    /// <summary>
    /// The user ID of the editor for the channel you want to create a clip for. If using the broadcaster’s auth token, this is the same as broadcaster_id. This must match the user_id in the user access token.
    /// </summary>
    public string EditorId { get; set; } = null!;

    /// <summary>
    /// The user ID for the channel you want to create a clip for.
    /// </summary>
    public string BroadcasterId { get; set; } = null!;

    /// <summary>
    /// ID of the VOD the user wants to clip.
    /// </summary>
    public string VodId { get; set; } = null!;

    /// <summary>
    /// Offset in the VOD to create the clip.
    /// </summary>
    public int VodOffset { get; set; }

    /// <summary>
    /// The length of the clip, in seconds. Precision is 0.1. Defaults to 30. Min: 5 seconds, Max: 60 seconds.
    /// </summary>
    public float? Duration { get; set; }

    /// <summary>
    /// The title of the clip.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <inheritdoc/>
    public virtual List<KeyValuePair<string, string>> ToParams()
    {
        var getParams = new List<KeyValuePair<string, string>>
        {
            new("editor_id", EditorId),
            new("broadcaster_id", BroadcasterId),
            new("vod_offset", VodId),
            new("duration", VodOffset.ToString()),
            new("title", Title),
        };

        if (Duration is not null)
        {
            getParams.Add(new("user_id", Duration.Value.ToString()));
        }

        return getParams;
    }
}
