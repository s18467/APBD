using System.ComponentModel.DataAnnotations;

namespace Warehouse.Attributes
{
    public class ValidDateTimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not DateTime date)
            {
                return false;
            }

            return date.Year > 2000 && date < DateTime.Today;
        }
    }
}