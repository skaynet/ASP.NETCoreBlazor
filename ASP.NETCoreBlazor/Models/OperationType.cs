using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreBlazor.Models
{
    public class OperationType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Название финансовой операции")]
        [Required(ErrorMessage = "Вам нужно ввести название финансовой операции")]
        public string Name { get; set; } = null!;

        public ICollection<FinancialTransaction>? FinancialTransactions { get; set; }
    }
}
