using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SignalR_Project
{
    public class Anecdote
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id;
        public string Anecdote_Text { get; set; }
    }
}
