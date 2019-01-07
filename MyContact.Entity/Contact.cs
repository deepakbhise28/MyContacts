using System.ComponentModel.DataAnnotations;

namespace MyContact.Entity
{
    public class Contact
    {
        public long Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Minimum 2 and maximum 50 letters are allowed.", MinimumLength = 2)]
        //[RegularExpression("^([a-zA-Z]{2,}\\s[a-zA-z]{1,}'?-?[a-zA-Z]{2,}\\s?([a-zA-Z]{1,})?)", ErrorMessage = "Valid Charactors include (A-Z) (a-z) (' space -)")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Minimum 2 and maximum 50 letters are allowed.", MinimumLength = 2)]
        //[RegularExpression("^([a-zA-Z]{2,}\\s[a-zA-z]{1,}'?-?[a-zA-Z]{2,}\\s?([a-zA-Z]{1,})?)", ErrorMessage = "Valid Charactors include (A-Z) (a-z) (' space -)")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(20, ErrorMessage = "Minimum 10 and maximum 20 letters are allowed.", MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Status")]
        public bool Status { get; set; }
    }
}
