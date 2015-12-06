using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetLiberty.Http.Samples
{
    public class Basics
    {
        private readonly WebApiClient<Thing, int> _client;

        public class Thing
        {
            public int Id { get; set; }
            public string SomeProperty { get; set; }
        }

        public Basics()
        {
            // Second type parameter is the ID type
            _client = new WebApiClient<Thing, int>(
                new Uri("http://localhost:5000/api/things/"));
        }

        public async void GetManyAsync()
        {
            // GET /api/things/
            IEnumerable<Thing> things = await _client.GetAsync();
        }

        public void GetMany()
        {
            // GET /api/things/
            IEnumerable<Thing> things = _client.Get();
        }

        public async void GetSingleAsync()
        {
            // GET /api/things/1
            Thing thing = await _client.GetAsync(1);
        }

        public void GetSingle()
        {
            // GET /api/things/1
            Thing thing = _client.Get(1);
        }

        public async void PostAsync()
        {
            Thing created = await _client.PostAsync(new Thing
            {
                Id = 2,
                SomeProperty = "Some value"
            });
        }

        public void Post()
        {
            Thing created = _client.Post(new Thing
            {
                Id = 2,
                SomeProperty = "Some value"
            });
        }

        public async void PutAsync()
        {
            Thing updated = await _client.PutAsync(3, new Thing
            {
                Id = 3,
                SomeProperty = "Some value"
            });
        }

        public void Put()
        {
            Thing updated = _client.Put(3, new Thing
            {
                Id = 3,
                SomeProperty = "Some value"
            });
        }

        public async void DeleteAsync()
        {
            await _client.DeleteAsync(4);
        }

        public void Delete()
        {
            _client.Delete(4);
        }
    }
}
