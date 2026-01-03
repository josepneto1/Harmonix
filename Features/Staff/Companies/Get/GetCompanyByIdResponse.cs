namespace Harmonix.Features.Staff.Companies.Get;

public record GetCompanyByIdResponse(
    Guid Id,
    string Name,
    string Alias,
    DateTimeOffset CreatedAt,
    DateTimeOffset ExpirationDate
);
