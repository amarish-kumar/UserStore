using System;

namespace Training.Services
{
    public class CreateUserCommand
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DoB { get; set; }
    }
}