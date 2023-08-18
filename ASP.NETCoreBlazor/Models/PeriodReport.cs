using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreBlazor.Models
{
    public class PeriodReport
    {
        [Display(Name = "Начальная дата финансовой транзакции")]
        [Required(ErrorMessage = "Вам нужно ввести начальную дату финансовой транзакции")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Конечная дата финансовой транзакции")]
        [Required(ErrorMessage = "Вам нужно ввести конечную дату финансовой транзакции")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Всего дохода")]
        public float TotalIncome { get; set; }

        [Display(Name = "Всего расхода")]
        public float TotalExpenses { get; set; }
        public ICollection<FinancialTransaction> Transactions { get; set; } = new List<FinancialTransaction>();
    }
}