using System;
namespace MyAdventureAPI.models
{
    public class AdventuretoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string AdventureCollectionName { get; set; } = null!;

        public string AdventureSessionCollectionName { get; set; } = null!;
    }
}
