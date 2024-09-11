using System.ComponentModel.DataAnnotations;

namespace Host.Enums
{
    public enum OrderState
    {
        [Display(Name = "En cours de préparation")]
        PreparationInProcess = 0,

        [Display(Name = "En cours de livraison")]
        DeliveryInProcess = 1,

        [Display(Name = "Livraison terminée")]
        DeliveryCompleted = 2
    }
}
