using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CyMvc.ViewModels
{
    public class ClientInfoViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public int ZipCode { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string NameOfFather { get; set; } = null!;
        [Required]
        public string NameOfMother { get; set; } = null!;
        [Required]
        public string CivilStatus { get; set; } = null!;
        [Required]
        public string Religion { get; set; } = null!;
        [Required]
        public string Occupation { get; set; } = null!;

    }
}
