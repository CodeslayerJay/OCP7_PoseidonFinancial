using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ModelValidators
{
    public class ValidatorBase<TEntity> : AbstractValidator<TEntity> where TEntity : class
    {

        protected const string NotValidNumberMsg = "Must be a number with value(s) 0-9";

        protected virtual bool ValidNumber(string number)
        {
            if (Decimal.TryParse(number, out decimal decimalVal))
            {
                return decimalVal >= 0.0M;
            }
            
            if (Double.TryParse(number, out double doubleVal))
            {
                return doubleVal >= 0.0;
            }

            if (Int32.TryParse(number, out int intVal))
            {
                return intVal >= 0;
            }
                
            if (float.TryParse(number, out float floatVal))
            {
                return floatVal >= 0.0;
            }
                
            return false;
        }

        protected virtual bool PriceMustBeACurrency(string price)
        {
            var result = Decimal.TryParse(price.ToString(), NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal value);

            return result;
        }


        protected virtual bool PriceMustBeGreaterThanZero(string price)
        {
            var result = Decimal.TryParse(price.ToString(), NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal value);

            if (result)
            {
                return (value > 0) ? true : false;
            }

            return result;
        }
    }
}
