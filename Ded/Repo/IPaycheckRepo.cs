using System;
using System.Collections.Generic;
using DeductionsAPI.Models.ViewModels;

namespace DeductionsAPI.Repo
{
    public interface IPaycheckRepo
    {
        PaycheckViewModel CalculatePaycheck(int EmployeeId);
    }
}
