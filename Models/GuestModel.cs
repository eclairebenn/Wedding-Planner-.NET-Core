using System;
using System.Collections.Generic;

namespace wedding_planner.Models
{
    public class Guest : BaseEntity
    {
        public int GuestId {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public int WeddingId {get;set;}
        public Wedding Wedding {get;set;}
        
    }
}