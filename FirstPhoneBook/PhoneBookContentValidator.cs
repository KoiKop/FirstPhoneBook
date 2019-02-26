using FluentValidation;

namespace FirstPhoneBook
{
    class PhoneBookContentValidator : AbstractValidator<PhoneBookContent>
    {
        public PhoneBookContentValidator()
        {
            RuleFor(x => x.Name).NotNull().MinimumLength(1).MaximumLength(255);
            RuleFor(x => x.Phone).NotNull().MaximumLength(20);
            RuleFor(x => x.Email).NotNull().EmailAddress().MaximumLength(255);
            RuleFor(x => x.Address).NotNull().MaximumLength(255);
        }
    }
}
