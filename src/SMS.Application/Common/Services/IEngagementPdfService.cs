using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.Entities;

namespace SMS.Application.Common.Services;

public interface IEngagementPdfService
{
    /// <summary>
    /// Generate a PDF document for an engagement contract
    /// </summary>
    /// <param name="engagement">The engagement with lines and tranches</param>
    /// <param name="student">The student information</param>
    /// <returns>PDF file as byte array</returns>
    byte[] GenerateEngagementContract(Engagement engagement, Student student);
}
