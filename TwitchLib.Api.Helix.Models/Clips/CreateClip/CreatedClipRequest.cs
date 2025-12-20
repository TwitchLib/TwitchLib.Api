using System.Collections.Generic;

namespace TwitchLib.Api.Helix.Models.Clips.CreateClip;

public class CreatedClipRequest
{
    /// <summary>
    /// The ID of the broadcaster whose stream you want to create a clip from.
    /// </summary>
    public string BroadcasterId { get; set; } = null!;

    /// <summary>
    /// The title of the clip.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The length of the clip in seconds. Possible values range from 5 to 60 inclusively with a precision of 0.1. The default is 30.
    /// </summary>
    public float? Duration { get; set; }

    /// <inheritdoc/>
    public virtual List<KeyValuePair<string, string>> ToParams()
    {
        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", BroadcasterId),
        };

        if (Title is not null)
            getParams.Add(new("title", Title));

        if (Duration is not null)
            getParams.Add(new("duration", Duration.Value.ToString()));

        return getParams;
    }
}
