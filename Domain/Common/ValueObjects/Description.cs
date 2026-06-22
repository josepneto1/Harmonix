using Harmonix.Domain.Common.Errors;
using Harmonix.Domain.Common.Validations;

namespace Harmonix.Domain.Common.ValueObjects
{
    public sealed record Description
    {
        private const short MaxLength = 500;
        public string Value { get; }

        private Description(string value) => Value = value;

        public static Result<Description> Create(string? description, short maxLength = MaxLength)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result<Description>.Fail(CommonErrors.InvalidDescription);

            description = description.Trim();

            if (!Validate.IsValidText(description, minLength: null, maxLength))
                return Result<Description>.Fail(CommonErrors.InvalidDescription);

            return Result<Description>.Success(new Description(description));
        }
    }
}
