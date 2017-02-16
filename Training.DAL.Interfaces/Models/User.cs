using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Training.DAL.Interfaces.Models
{
    public class User
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public BsonDateTime DoB { get; set; }
        public string Email { get; set; }
        public BsonDateTime CreatedDate { get; set; }
        public BsonDateTime UpdatedDate { get; set; }
        public string Password { get; set; }
    }
}