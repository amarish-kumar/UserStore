#region

using System;

#endregion

namespace TrainingTake2.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DoB { get; set; }
        public string Password { get; set; }
    }
}