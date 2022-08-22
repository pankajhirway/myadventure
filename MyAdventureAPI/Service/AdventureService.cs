using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyAdventureAPI.models;
using Newtonsoft.Json.Linq;

namespace MyAdventureAPI.Service
{
   
    public class AdventureService
    {
         private readonly IMongoCollection<Adventure> _adventureCollection;

        String seeddata = "{\r\n  \"Name\": \"What should I Order ?\",\r\n  \"Steps\": [\r\n    {\r\n      \"Question\": \"Do you want to Order Food ?\",\r\n      \"Id\": \"1\",\r\n      \"Options\": [\r\n        {\r\n          \"Value\": \"Yes\",\r\n          \"NextId\": \"2\"\r\n        },\r\n        {\r\n          \"Value\": \"No\",\r\n          \"NextId\": \"3\"\r\n        }\r\n      ]\r\n    },\r\n    {\r\n      \"Question\": \"What do you feel like eating ?\",\r\n      \"Id\": \"2\",\r\n      \"Options\": [\r\n        {\r\n          \"Value\": \"Indian\",\r\n          \"NextId\": \"4\"\r\n        },\r\n        {\r\n          \"Value\": \"Chinese\",\r\n          \"NextId\": \"5\"\r\n        }\r\n      ]\r\n    },\r\n    {\r\n      \"Question\": \"Okay, You are not hungry is Seems!!\",\r\n      \"Id\": \"3\",\r\n      \"Options\": []\r\n    },\r\n    {\r\n      \"Question\": \"Okay, Which Indian dish to order!!\",\r\n      \"Id\": \"4\",\r\n      \"Options\": [\r\n        {\r\n          \"Value\": \"Daal Roti\",\r\n          \"NextId\": \"6\"\r\n        },\r\n        {\r\n          \"Value\": \"Biryani\",\r\n          \"NextId\": \"7\"\r\n        }\r\n      ]\r\n    },\r\n    {\r\n      \"Question\": \"Bad Luck, Chinese take out is close !!\",\r\n      \"Id\": \"5\",\r\n      \"Options\": []\r\n    },\r\n    {\r\n      \"Question\": \"Okay, Enjoy your Daal Roti!!\",\r\n      \"Id\": \"6\",\r\n      \"Options\": []\r\n    },\r\n    {\r\n      \"Question\": \"Okay, Enjoy your Biryani!!\",\r\n      \"Id\": \"7\",\r\n      \"Options\": []\r\n    }\r\n  ]\r\n}";

        private readonly IMongoCollection<AdventureSession> _adventureSessionCollection;

        //For Testing
        public AdventureService(IMongoCollection<Adventure> adventureCollection, IMongoCollection<AdventureSession> adventuresSessionCollection)
        {
            this._adventureCollection = adventureCollection;
            this._adventureSessionCollection = adventuresSessionCollection;
        }

        public AdventureService(
                IOptions<AdventuretoreDatabaseSettings> adventureStoreDatabaseSettings)
            {
                var mongoClient = new MongoClient(
                    adventureStoreDatabaseSettings.Value.ConnectionString);


            var mongoDatabase = mongoClient.GetDatabase(
                    adventureStoreDatabaseSettings.Value.DatabaseName);

                _adventureCollection = mongoDatabase.GetCollection<Adventure>(
                    adventureStoreDatabaseSettings.Value.AdventureCollectionName);

            _adventureSessionCollection = mongoDatabase.GetCollection<AdventureSession>(
                   adventureStoreDatabaseSettings.Value.AdventureSessionCollectionName);


            Adventure obj = JsonSerializer.Deserialize<Adventure>(seeddata);
            var data =_adventureCollection.Find(a => a.Name == obj.Name).FirstOrDefault();
            if (data == null)
            _adventureCollection.InsertOne(obj);
        }

            public async Task<List<Adventure>> GetAsync() =>
                await _adventureCollection.Find(_ => true).ToListAsync();

            public async Task<Adventure?> GetAsync(string id) =>
                await _adventureCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<AdventureSession?> GetAsyncSession(string id) =>
              await _adventureSessionCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Adventure newAdventure) =>
                await _adventureCollection.InsertOneAsync(newAdventure);


        public async Task CreateAsync(AdventureSession newAdventure) =>
           await _adventureSessionCollection.InsertOneAsync(newAdventure);

        public async Task UpdateSessionAsync(string id,AdventureSession updateAdventure) =>
          await _adventureSessionCollection.ReplaceOneAsync(x => x.Id == id, updateAdventure);

        public async Task UpdateAsync(string id, Adventure updateAdventure) =>
                await _adventureCollection.ReplaceOneAsync(x => x.Id == id, updateAdventure);

            public async Task RemoveAsync(string id) =>
                await _adventureCollection.DeleteOneAsync(x => x.Id == id);
        }
    
}
