namespace Harmonix.Features.Staff.Companies.Activate;

public record SetCompanyStatusRequest(Guid CompanyId, bool IsActive);
