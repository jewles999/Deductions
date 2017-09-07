using DeductionsAPI.Models.ViewModels;

namespace DeductionsAPI.Repo
{
    public interface IPaycheckRepo
    {
        PaycheckViewModel CalculatePaycheck(int EmployeeId);
    }
}
