using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LexiconLMS.Models
{
    public enum GenericCompareOperator
    {
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        Equal,
        NotEqual
    }

    public sealed class GenericCompareAttribute : ValidationAttribute, IClientValidatable
    {
        public string CompareToPropertyName { get; set; }
        public GenericCompareOperator OperatorName { get; set; } = GenericCompareOperator.GreaterThanOrEqual;

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
            ControllerContext context)
        {
            var errorMessage = FormatErrorMessage(metadata.DisplayName);
            var compareRule = new ModelClientValidationRule
            {
                ErrorMessage = errorMessage,
                ValidationType = "genericcompare"
            };
            compareRule.ValidationParameters.Add("comparetopropertyname", CompareToPropertyName);
            compareRule.ValidationParameters.Add("operatorname", OperatorName.ToString());
            yield return compareRule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string operstring;
            switch (OperatorName)
            {
                case GenericCompareOperator.GreaterThan:
                    operstring = "greater than";
                    break;
                case GenericCompareOperator.GreaterThanOrEqual:
                    operstring = "greater than or equal to";
                    break;
                case GenericCompareOperator.LessThan:
                    operstring = "less than";
                    break;
                case GenericCompareOperator.LessThanOrEqual:
                    operstring = "less than or equal to";
                    break;
                case GenericCompareOperator.Equal:
                    operstring = "equal to";
                    break;
                case GenericCompareOperator.NotEqual:
                    operstring = "not equal to";
                    break;
                default:
                    return null; // We can't compare as we don't know what method to use
            }

            var basePropertyInfo = validationContext.ObjectType.GetProperty(CompareToPropertyName);

            var valOther = (IComparable) basePropertyInfo.GetValue(validationContext.ObjectInstance, index: null);
            var valThis = (IComparable) value;

            var compareResult = valThis.CompareTo(valOther);

            if (OperatorName == GenericCompareOperator.GreaterThan && compareResult <= 0 ||
                OperatorName == GenericCompareOperator.GreaterThanOrEqual && compareResult < 0 ||
                OperatorName == GenericCompareOperator.LessThan && compareResult >= 0 ||
                OperatorName == GenericCompareOperator.LessThanOrEqual && compareResult > 0 ||
                OperatorName == GenericCompareOperator.Equal && compareResult == 0 ||
                OperatorName == GenericCompareOperator.NotEqual && compareResult != 0)
            {
                return new ValidationResult(string.Format(ErrorMessage, operstring));
            }
            return null;
        }
    }
}
