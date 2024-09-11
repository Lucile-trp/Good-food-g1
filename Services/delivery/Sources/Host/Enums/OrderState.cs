using System.ComponentModel.DataAnnotations;

namespace Host.Enums
{
    public enum OrderState
    {
        [Display(Name = "En attente")]
        Waiting = 0,

        [Display(Name = "Validée par le restaurant")]
        Validated = 1,

        [Display(Name = "Préparation")]
        Preparation = 2,

        [Display(Name = "Prête")]
        Ready = 3,

        [Display(Name = "Prise en charge")]
        Support = 4,

        [Display(Name = "Livrée")]
        Delivered = 5,

        [Display(Name = "Annulé")]
        Canceled = 6
    }
}
