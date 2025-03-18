using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Models
{
    public class PriceSaleValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var product = (Product)validationContext.ObjectInstance;

            if (product.PriceSale.HasValue && product.PriceSale > product.OriginalPrice)
            {
                return new ValidationResult("Giá khuyến mãi không được lớn hơn giá gốc.");
            }

            return ValidationResult.Success;
        }
    }
}

