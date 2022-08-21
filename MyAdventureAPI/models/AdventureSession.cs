using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;

namespace MyAdventureAPI.models
{
    public class AdventureSession
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string AdventureId { get; set; }

        public List<AdventureStepRecord> StepsTaken { get; set; }

        public bool IsComplete { get; set; }
    }


    public class AdventureSessionStep
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string SessionId { get; set; }

        public string StepId { get; set; }

        public string Choice { get; set; }
         
    }
}
