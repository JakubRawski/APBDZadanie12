using System.ComponentModel.DataAnnotations;



namespace Tutorial5.DTOs;


public class AssignClientToTripRequest
{
    [Required]
    [MaxLength(120)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(120)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(120)]
    public string Email { get; set; }

    [Required]
    [Phone]
    [MaxLength(120)]
    public string Telephone { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 11)] 
    public string Pesel { get; set; }

    public int IdTrip { get; set; } 
    public string TripName { get; set; } 
    public DateTime? PaymentDate { get; set; }
}
