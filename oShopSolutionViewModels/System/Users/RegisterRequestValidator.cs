using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.System.Users
{
	public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
	{
		public RegisterRequestValidator()
		{
			RuleFor(x => x.FullName).NotEmpty().WithMessage("Fullname is Emty");
			RuleFor(x => x.DOB).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("More than 100 old");
			RuleFor(x => x.Email).NotEmpty().WithMessage("Email is emty")
				.Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
				.WithMessage("Email wrong format");
			RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is emty")
				.MaximumLength(12).WithMessage("Phone Number more than 12 characters");
			RuleFor(x => x.Username).NotEmpty().WithMessage("Username required");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Password required")
				.MinimumLength(6).WithMessage("Password less than 6 characters");
			RuleFor(x => x).Custom((request, context) =>
			{
				if (request.Password != request.ConfirmPassword)
				{
					context.AddFailure("Confirm Password is wrong");
				}
			});
		}
	}
}
