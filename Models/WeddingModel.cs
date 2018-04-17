using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CustomDataAnnotations;

namespace wedding_planner.Models
{
    public class Wedding : BaseEntity
    {
        public int WeddingId {get;set;}

        [Required]
        [Display(Name="Wedder One")]
        public string NameOne {get;set;}
        
        [Required]
        [Display(Name="Wedder Two")]
        public string NameTwo {get;set;}

        [Required]
        public string Address {get;set;}

        [Required]
        [CurrentDate(ErrorMessage = "Date must be after or equal to current date")]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}

        public DateTime CreatedAt {get;set;}

        [ForeignKey("Creator")]
        public int UserId {get;set;}
        public User Creator {get;set;}

        public List<Guest> Attendees {get;set;}

        public Wedding()
        {
            Attendees = new List<Guest>();
            CreatedAt = DateTime.Now;
        }
        
    }
}