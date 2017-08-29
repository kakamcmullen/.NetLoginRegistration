using System;
using Registration.Models;

namespace Registration.Models
{
    public class User : BaseEntity // not dependent name
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DOB {get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //Enter model functions
    }


}
