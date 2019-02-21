using FluentValidation;

namespace FirstPhoneBook
{
    class PhoneBookContentValidator : AbstractValidator<PhoneBookContent>
    {
        public PhoneBookContentValidator()
        {
            RuleFor(phoneBookContent => phoneBookContent.Name).NotNull().MinimumLength(1).MaximumLength(256);

        }
    }
}
