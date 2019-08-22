using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using wedding.Models;

namespace wedding.Models
{
    public class Wedding
    {
        [Key]
        [Column("id")]
        public int WedId {get;set;}
        [Column("user_id")]
        public int UserId {get;set;}
        [Column("wed1")]
        [Required]
        public string Wed1 {get;set;}
        [Column("wed2")]
        [Required]
        public string Wed2 {get;set;}
        [Column("date")]
        [Required]
        public DateTime Date {get;set;}
        [Column("address")]
        [Required]
        public string Address {get;set;}

        [Column("created_at")]
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        [Column("updated_at")]
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        // Navigation Properties    
        public User Creator {get;set;}
        public List<Action> Answer {get;set;}
    }
}