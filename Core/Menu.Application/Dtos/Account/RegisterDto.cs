using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Menu.Application.Dtos.Account
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegisterDtoValidation : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname cannot be empty.");

            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email cannot be empty and must be valid.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("The password cannot be empty!")
                .Must(r =>
                {
                    Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,50}$");
                    return passwordRegex.IsMatch(r);
                }).WithMessage("Password format is not correct!")
                .Must(p => !p.Contains(" ")).WithMessage("The password cannot contain spaces!");




        }
    }
}
