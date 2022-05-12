namespace MiniCRM.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Client
    {
        public int Id { get; set; }
        
        [Required]
        // No StringLenght validations because there are actually names that are only 1 character and there's not upper limit.
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; }

    }
}
