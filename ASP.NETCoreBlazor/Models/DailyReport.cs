using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreBlazor.Models
{
    public class DailyReport
    {
        [Display(Name = "Дата финансовой транзакции")]
        [Required(ErrorMessage = "Вам нужно ввести дату финансовой транзакции")]
        public DateTime Date { get; set; }

        [Display(Name = "Всего дохода")]
        public float TotalIncome { get; set; }

        [Display(Name = "Всего расхода")]
        public float TotalExpenses { get; set; }
        public ICollection<FinancialTransaction> Transactions { get; set; } = new List<FinancialTransaction>();
    }
}