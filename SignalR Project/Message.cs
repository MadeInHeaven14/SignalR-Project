using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SignalR_Project
{
    public class Message
    {
        [BsonId]
        public ObjectId _id;
        public string message { get; set; }
        public string userName { get; set; }
        public string Email { get; set; }
    }
}
