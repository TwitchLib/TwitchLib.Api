using System.Collections.Generic;

namespace TwitchLib.Api.Helix.Models.Subscriptions;

/// <summary>
/// A class to represent the request query data for a <see href="https://dev.twitch.tv/docs/api/reference/#get-broadcaster-subscriptions">Get Broadcaster Subscriptions</see> request.
/// </summary>
public class GetBroadcasterSubscriptionsRequest
{
    /// <summary>
    /// The broadcaster’s ID. This ID must match the user ID in the access token.
    /// </summary>
    public string BroadcasterId { get; set; } = null!;

    /// <summary>
    /// Filters the list to include only the specified subscribers.
    /// </summary>
    public List<string>? UserIds { get; set; }

    /// <summary>
    /// The maximum number of items to return per page in the response.
    /// The minimum page size is 1 item per page and the maximum is 100 items per page.
    /// The default is 20.
    /// </summary>
    public int? First { get; set; }

    /// <summary>
    /// The cursor used to get the next page of results.
    /// Do not specify if you set the <see cref="UserIds"/> query parameter.
    /// The Pagination object in the response contains the cursor’s value.
    /// </summary>
    public string? After { get; set; }

    /// <summary>
    /// The cursor used to get the previous page of results.
    /// Do not specify if you set the <see cref="UserIds"/> query parameter.
    /// The Pagination object in the response contains the cursor’s value.
    /// </summary>
    public string? Before { get; set; }

    public virtual List<KeyValuePair<string, string>> ToParams()
    {
        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", BroadcasterId),
            new("first", First?.ToString() ?? "20")
        };

        if (UserIds?.Count > 0)
        {
            foreach (var userId in UserIds)
            {
                getParams.Add(new("user_id", userId));
            }
        }

        if (!string.IsNullOrEmpty(After))
            getParams.Add(new("after", After));

        if (!string.IsNullOrEmpty(Before))
            getParams.Add(new("after", Before));

        return getParams;
    }
}
