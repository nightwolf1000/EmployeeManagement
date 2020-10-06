using System;
using EmployeeManagement.Extensions;
using Microsoft.AspNetCore.Mvc;
using static Constants;

namespace EmployeeManagement.Controllers
{
    public class ValidationController : Controller
    {
        [AcceptVerbs("Get", "Post")]
        public IActionResult IsBirthDateValid(string birthDate)
        {
            if (!birthDate.IsValidDateTime()) 
                return Json(InvalidDateMessages.InvalidDateFormat);
            
            if (birthDate.ToDateTime() > DateTime.Today)
                return Json(InvalidDateMessages.BirthDateCannotBeAFutureDate);

            if (birthDate.ToDateTime().ComputeAgeInYears() < 18)
                return Json(InvalidDateMessages.EmployeeMustBe18orOver);

            return Json(true);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsHireDateValid(string hireDate, string birthDate)
        {            
            if (!hireDate.IsValidDateTime())
                return Json(InvalidDateMessages.InvalidDateFormat);

            if (hireDate.ToDateTime() > DateTime.Today)
                return Json(InvalidDateMessages.HireDateCannotBeAFutureDate);

            if (!birthDate.IsValidDateTime())
                return Json(true);

            if (hireDate.ToDateTime() < birthDate.ToDateTime())
                return Json(InvalidDateMessages.HireDateCannotBeBeforeBirthDate);

            if (birthDate.ToDateTime().AddYears(18) > hireDate.ToDateTime())
                return Json(InvalidDateMessages.HireDateMustBe18orMoreYearsAfterBirthDate);

            return Json(true);            
        }
    }
}
