using System;
using System.Collections.Generic;

namespace wedding_planner.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public float Balance {get;set;}
        public List<Guest> Attending {get;set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User()
        {
            Attending = new List<Guest>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Balance = 0;
        }
    }
}