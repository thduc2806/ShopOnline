using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.ViewModels.System.Users
{
	public class LoginRequestValidator : AbstractValidator<LoginRequest>
	{
		public LoginRequestValidator()
		{
			RuleFor(x => x.Username).NotEmpty().WithMessage("Username required");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Username required")
				.MinimumLength(6).WithMessage("Password less than 6 characters");

		}
	}
}
