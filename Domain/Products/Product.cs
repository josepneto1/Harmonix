using Harmonix.Domain.Common;
using Harmonix.Domain.Common.Errors;
using Harmonix.Domain.Common.Validations;
using Harmonix.Domain.Common.ValueObjects;
using Harmonix.Domain.Companies;

namespace Harmonix.Domain.Products;

public class Product : BaseEntity
{
    public Guid CompanyId { get; private set; }
    public string Code { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public Description? Description { get; private set; }
    public Company Company { get; private set; } = null!;

    protected Product() { }

    private Product(Guid companyId, string name, Description? description)
    {
        Id = GenerateNewId();
        CompanyId = companyId;
        Name = name;
        Description = description;
    }

    public static Result<Product> Create(Guid companyId, string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Product>.Fail(CommonErrors.InvalidName);

        name = name.Trim();

        if (!Validate.IsValidText(name, minLength: 3, maxLength: 100))
            return Result<Product>.Fail(CommonErrors.InvalidName);

        Description? descriptionToSave = null;
        if (description is not null)
        {
            var descriptonResult = Description.Create(description);
            if (descriptonResult.IsFailure)
                return Result<Product>.Fail(descriptonResult.Error);

            descriptionToSave = descriptonResult.Data;
        }

        var product = new Product(companyId, name, descriptionToSave);
        return Result<Product>.Success(product);
    }
}
