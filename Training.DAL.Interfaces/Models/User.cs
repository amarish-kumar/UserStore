#region

using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#endregion

namespace Training.DAL.Interfaces.Models
{
    public class User
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime DoB { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}