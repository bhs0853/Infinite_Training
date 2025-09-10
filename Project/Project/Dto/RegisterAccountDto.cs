using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Dto
{
    public class RegisterAccountDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50)]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Father's Name is required")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit mobile number")]
        public string Mobile { get; set; }

        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Aadhar number is required")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhar must be 12 digits")]
        public string Aadhar { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

       
        [Required(ErrorMessage = "Address Line 1 is required")]
        public string ResAddress1 { get; set; }
        public string ResAddress2 { get; set; }
        public string ResLandmark { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string ResState { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string ResCity { get; set; }

        [Required(ErrorMessage = "Pincode is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode must be 6 digits")]
        public string ResPincode { get; set; }

        
        [Required(ErrorMessage = "Address Line 1 is required")]
        public string PerAddress1 { get; set; }
        public string PerAddress2 { get; set; }
        public string PerLandmark { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string PerState { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string PerCity { get; set; }

        [Required(ErrorMessage = "Pincode is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode must be 6 digits")]
        public string PerPincode { get; set; }


        [Required(ErrorMessage = "Occupation type is required")]
        public string OccupationType { get; set; }

        [Required(ErrorMessage = "Source of income is required")]
        public string SourceIncome { get; set; }

        [Required(ErrorMessage = "Annual income is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Enter a valid income")]
        public decimal AnnualIncome { get; set; }

        public bool DebitCard { get; set; }
        public bool NetBanking { get; set; }
    }
}