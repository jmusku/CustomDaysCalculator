using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Jeevan.Musku.DateCalculator.Utilities;

namespace Jeevan.Musku.DateCalculator.Validators
{
    public class DateValidator : ValidationAttribute
    {
        //To validate the string to be in the date pattern and validate from and to dates.
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                string inputValue = value.ToString();
                string validPattern = @"(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](18|[2-9][0-9])\d\d$)|(^29[\/]02[\/](18|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)";
                Match match = Regex.Match(inputValue, validPattern);
                if (match.Success)
                {
                    var property = validationContext.ObjectType.GetProperty("ToDate");
                    if (property == null)
                        return new ValidationResult("Property 'ToDate' is Null");
                    var toDate = property.GetValue(validationContext.ObjectInstance, null);
                    string strToDate = toDate == null ? "" : toDate.ToString();
                    Match checkToDate = Regex.Match(strToDate, validPattern);
                    if (checkToDate.Success)
                    {
                        CustomDateUtility cdu = new CustomDateUtility();
                        if (!cdu.CheckIfDatesValid(inputValue, strToDate))
                        {
                            return new ValidationResult("To date should be greater than from date");
                        }
                    }
                }
                else
                    return new ValidationResult("Invalid date format");
                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid date format");
            }
        }
    }
}