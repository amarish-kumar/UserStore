#region

using System;

#endregion

namespace Training.Services
{
    public class Command
    {
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DoB { get; set; }
        public Operation Operation { get; set; }
    }

    public enum Operation
    {
        Create,
        Update
    }
}