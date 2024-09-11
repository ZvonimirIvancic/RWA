﻿using DAL.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Administrator.ViewModels
{
    public class VMUser
    {
        public int Id { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "User name is required")]
        public string Username { get; set; } = null!;

        [DisplayName("User First Name")]
        [Required(ErrorMessage = "User first name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name should be between 2 and 50 characters long")]

        public string FirstName { get; set; } = null!;

        [DisplayName("User Last Name")]
        [Required(ErrorMessage = "User last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name should be between 2 and 50 characters long")]

        public string LastName { get; set; } = null!;

        [DisplayName("User Email Adress")]
        [Required, EmailAddress(ErrorMessage = "User Email adress is required")]
        public string Email { get; set; } = null!;

        [DisplayName("User Phone number")]
        [Required, Phone(ErrorMessage = "User phone number is required")]

        public string? Phone { get; set; }

    }
}
