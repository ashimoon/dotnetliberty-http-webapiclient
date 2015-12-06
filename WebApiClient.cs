using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace DotNetLiberty.Http
{
    public class WebApiClient<TEntity, TKey>
    {
        private const string ApplicationJson = "application/json";
        private readonly HttpClient _client;
        private readonly DataContractJsonSerializer _serialier;
        private readonly DataContractJsonSerializer _listSerializer;

        public WebApiClient(Uri baseAddress)
        {
            _serialier = new DataContractJsonSerializer(typeof (TEntity));
            _listSerializer = new DataContractJsonSerializer(typeof (List<TEntity>));
            _client = new HttpClient
            {
                BaseAddress = baseAddress,
            };
            _client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
        }

        public async Task DeleteAsync(TKey id)
        {
            var result = await _client.DeleteAsync(id.ToString());
            result.ThrowIfUnsuccessful();
        }

        public void Delete(TKey id)
        {
            var task = DeleteAsync(id);
            task.WaitOrUnwrapException();
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            var result = await _client.GetAsync(string.Empty);
            result.ThrowIfUnsuccessful();
            return await ReadEntities(result);
        }

        public IEnumerable<TEntity> Get()
        {
            var task = GetAsync();
            task.WaitOrUnwrapException();
            return task.Result;
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            var result = await _client.GetAsync(id.ToString());
            result.ThrowIfUnsuccessful();
            return await ReadEntity(result);
        }

        public TEntity Get(TKey id)
        {
            var task = GetAsync(id);
            task.WaitOrUnwrapException();
            return task.Result;
        }

        public async Task<TEntity> PostAsync(TEntity value)
        {
            var result = await _client.PostAsync(string.Empty, JsonContent(value));
            result.ThrowIfUnsuccessful();
            return await ReadEntity(result);
        }

        public TEntity Post(TEntity value)
        {
            var task = PostAsync(value);
            task.WaitOrUnwrapException();
            return task.Result;
        }

        public async Task<TEntity> PutAsync(TKey id, TEntity value)
        {
            var result = await _client.PutAsync(id.ToString(), JsonContent(value));
            result.ThrowIfUnsuccessful();
            return await ReadEntity(result);
        }

        public TEntity Put(TKey id, TEntity value)
        {
            var task = PutAsync(id, value);
            task.WaitOrUnwrapException();
            return task.Result;
        }

        private async Task<List<TEntity>> ReadEntities(HttpResponseMessage response)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            if (stream.Length == 0) return new List<TEntity>();
            return (List<TEntity>) _listSerializer.ReadObject(stream);
        }

        private async Task<TEntity> ReadEntity(HttpResponseMessage response)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            if (stream.Length == 0) return default(TEntity);
            return (TEntity) _serialier.ReadObject(stream);
        }

        private JsonContent JsonContent(TEntity entity)
        {
            using (var stream = new MemoryStream())
            {
                _serialier.WriteObject(stream, entity);
                return new JsonContent(stream);
            }
        }
    }
}