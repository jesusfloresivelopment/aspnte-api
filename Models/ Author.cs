using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace lab1_api.Models
{
    public class Author
    {
        public int Id {get; set;}
        [Required]
        public string Name {get; set;}
        public List<Book> Books { get; set ;}
        
    }
    
}