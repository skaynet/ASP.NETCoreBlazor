using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreBlazor.Models
{
    public class FinancialTransaction
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Описание финансовой транзакции")]
        [Required(ErrorMessage = "Вам нужно ввести описание финансовой транзакции")]
        public string Description { get; set; } = null!;

        [Display(Name = "Сумма финансовой транзакции")]
        [Required(ErrorMessage = "Вам нужно ввести сумму финансовой транзакции")]
        public float Amount { get; set; }

        [Display(Name = "Дата финансовой транзакции")]
        [Required(ErrorMessage = "Вам нужно ввести дату финансовой транзакции")]
        public DateTime Date { get; set; }

        [Display(Name = "Тип финансовой операции")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Вам нужно выбрать тип финансовой операции")]
        public int TypeId { get; set; }

        public virtual OperationType? Type { get; set; }
    }
}
