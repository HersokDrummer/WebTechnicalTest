using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TechnicalTest.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string SecondLastName { get; set; }
        [Required]
        public int Age { get; set; }
        [EmailAddress]
        public string email { get; set; }
    }
}