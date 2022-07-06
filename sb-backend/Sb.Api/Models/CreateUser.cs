﻿namespace Sb.Api.Models
{
    public class CreateUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Guid? EmailConfirmationToken { get; set; }
    }
}
