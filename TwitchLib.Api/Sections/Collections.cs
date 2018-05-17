using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Collections
    {
        public Collections(TwitchAPI api)
        {
            V5 = new V5Api(api);
        }

        public V5Api V5 { get; }

        public class V5Api : ApiSection
        {
            public V5Api(TwitchAPI api) : base(api)
            {
            }
            #region GetCollectionMetadata
            public Task<Models.v5.Collections.CollectionMetadata> GetCollectionMetadataAsync(string collectionId)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                return Api.TwitchGetGenericAsync<Models.v5.Collections.CollectionMetadata>($"/collections/{collectionId}", ApiVersion.v5);
            }
            #endregion
            #region GetCollection
            public Task<Models.v5.Collections.Collection> GetCollectionAsync(string collectionId, bool? includeAllItems = null)
            {
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (includeAllItems.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("include_all_items", ((bool)includeAllItems).ToString()));
                return Api.TwitchGetGenericAsync<Models.v5.Collections.Collection>($"/collections/{collectionId}/items", ApiVersion.v5, getParams);
            }
            #endregion
            #region GetCollectionsByChannel
            public Task<Models.v5.Collections.CollectionsByChannel> GetCollectionsByChannelAsync(string channelId, long? limit = null, string cursor = null, string containingItem = null)
            {
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for catching a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
                if (!string.IsNullOrWhiteSpace(cursor))
                    getParams.Add(new KeyValuePair<string, string>("cursor", cursor));
                if (!string.IsNullOrWhiteSpace(containingItem))
                    getParams.Add(new KeyValuePair<string, string>("containing_item", containingItem.StartsWith("video:") ? containingItem : $"video:{containingItem}"));
                
                return Api.TwitchGetGenericAsync<Models.v5.Collections.CollectionsByChannel>($"/channels/{channelId}/collections", ApiVersion.v5, getParams);
            }
            #endregion
            #region CreateCollection
            public Task<Models.v5.Collections.CollectionMetadata> CreateCollectionAsync(string channelId, string collectionTitle, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid for a collection creation. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(collectionTitle)) { throw new BadParameterException("The collection title is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                string payload = "{\"title\": \"" + collectionTitle + "\"}";
                return Api.TwitchPostGenericAsync<Models.v5.Collections.CollectionMetadata>($"/channels/{channelId}/collections", ApiVersion.v5, payload, accessToken: authToken);
            }
            #endregion
            #region UpdateCollection
            public async Task UpdateCollectionAsync(string collectionId, string newCollectionTitle, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(newCollectionTitle)) { throw new BadParameterException("The new collection title is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                string payload = "{\"title\": \"" + newCollectionTitle + "\"}";
                await Api.TwitchPutAsync($"/collections/{collectionId}", ApiVersion.v5, payload, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region CreateCollectionThumbnail
            public async Task CreateCollectionThumbnailAsync(string collectionId, string itemId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                string payload = "{\"item_id\": \"" + itemId + "\"}";
                await Api.TwitchPutAsync($"/collections/{collectionId}/thumbnail", ApiVersion.v5, payload, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region DeleteCollection
            public async Task DeleteCollectionAsync(string collectionId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.TwitchDeleteAsync($"/collections/{collectionId}", ApiVersion.v5, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region AddItemToCollection
            public Task<Models.v5.Collections.CollectionItem> AddItemToCollectionAsync(string collectionId, string itemId, string itemType, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (itemType != "video") { throw new BadParameterException($"The item_type {itemType} is not valid for a collection. Item type MUST be \"video\"."); }
                string payload = "{\"id\": \"" + itemId + "\", \"type\": \"" + itemType + "\"}";
                return Api.TwitchPostGenericAsync<Models.v5.Collections.CollectionItem>($"/collections/{collectionId}/items", ApiVersion.v5, payload, accessToken: authToken);
            }
            #endregion
            #region DeleteItemFromCollection
            public async Task DeleteItemFromCollectionAsync(string collectionId, string itemId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.TwitchDeleteAsync($"/collections/{collectionId}/items/{itemId}", ApiVersion.v5, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region MoveItemWithinCollection
            public async Task MoveItemWithinCollectionAsync(string collectionId, string itemId, int position, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Collections_Edit, authToken);
                if (string.IsNullOrWhiteSpace(collectionId)) { throw new BadParameterException("The collection id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(itemId)) { throw new BadParameterException("The item id is not valid for a collection. It is not allowed to be null, empty or filled with whitespaces."); }
                if (position < 1) { throw new BadParameterException("The position is not valid for a collection. It is not allowed to be less than 1."); }
                string payload = "{\"position\": \"" + position + "\"}";
                await Api.TwitchPutAsync($"/collections/{collectionId}/items/{itemId}", ApiVersion.v5, payload, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
        }

    }
}
