namespace Harmonix.Features.Staff.Companies.Update;

public record UpdateCompanyRequest(
    string Name,
    string Alias,
    DateTimeOffset CreatedAt,
    DateTimeOffset ExpirationDate
);
