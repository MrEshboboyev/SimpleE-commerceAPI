﻿namespace SimpleE_commerceAPI.Application.Common.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string RoleName { get; set; }
    }
}
