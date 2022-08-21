using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyAdventureAPI.models
{
    public class AdventureStep
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public List<AdventureOption> Options { get; set; }

    }

}
