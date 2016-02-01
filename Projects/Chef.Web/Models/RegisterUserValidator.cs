using FluentValidation;
using System.Linq;

namespace Chef.Web.Models.Accounts
{
    /// <summary>
    /// Validator for registration on the ui, Register.cshtml and ExternalLoginConfirmation.cshtml.
    /// </summary>
    /// <remarks>
    /// Note: it's best practice that BLL also has these rules implemented, not just on UI tier.
    /// Our <see cref="ProfileService.CreateUserProfile"/> has these implemented.
    /// </remarks>
    public class RegisterUserValidator : AbstractValidator<ExternalLoginConfirmationViewModel>
    {
        public RegisterUserValidator()
        {
            // null or empty, without this check, an empty name won't be caught
            RuleFor(x => x.UserName).NotEmpty().WithMessage(Msg.USERNAME_MUST_BE_WITHIN_LENGTH);
            // length
            RuleFor(x => x.UserName).Length(Config.USERNAME_MINLENGTH, Config.USERNAME_MAXLENGTH)
                .WithMessage(string.Format(Msg.USERNAME_MUST_BE_WITHIN_LENGTH, Config.USERNAME_MINLENGTH, Config.USERNAME_MAXLENGTH));
            // regex
            RuleFor(x => x.UserName).Matches(Config.USERNAME_REGEX).WithMessage(Msg.USERNAME_MUST_BE_ALPHANUMERICA);
            // reserved names
            RuleFor(x => x.UserName).Must(n => !Config.USERNAME_RESERVED.Contains(n)).WithMessage(Msg.USERNAME_IS_TAKEN, x => x.UserName);
        }
    }
}