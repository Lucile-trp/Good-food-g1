using System.ComponentModel.DataAnnotations;

namespace Host.Enums
{
    public enum ProductType
    {
        [Display(Name = "Type 1")]
        Type1 = 0,

        [Display(Name = "Type 2")]
        Type2 = 1,

        [Display(Name = "Type 3")]
        Type3 = 2
    }
}
