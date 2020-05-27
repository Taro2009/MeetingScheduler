using System.ComponentModel.DataAnnotations;

namespace MeetingScheduler.Model
{
    // Az MVVM patternből a "Model" rész, amit az egész project során mindenhol használunk. 
    public class Person
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }




    }
}
