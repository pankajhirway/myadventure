using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyAdventureAPI.models;

namespace MyAdventureAPI.Service
{
    public class AdventureService
    {
         private readonly IMongoCollection<Adventure> _adventureCollection;

        private readonly IMongoCollection<AdventureSession> _adventureSessionCollection;

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
