using ASP.NETCoreBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace ASP.NETCoreBlazor.Pages
{
    public partial class FinancialTransaction
    {
        [Parameter]
        public int? id { get; set; }

        private string btnText = string.Empty;
        private Models.FinancialTransaction financialTransaction = new Models.FinancialTransaction
        {
            Description = "New Financial Transaction",
            Date = DateTime.Today
        };
        private List<Models.OperationType> operationsType = new List<Models.OperationType>();

        protected override async void OnInitialized()
        {
            btnText = id == null ? "Добавить новую финансовую транзакцию" : "Изменить финансовую транзакцию";
            var result = await OperationTypeRepository.GetAllAsync();
            if (result is not null)
            {
                operationsType = (List<Models.OperationType>)result;
                if (operationsType.Count > 0)
                {
                    financialTransaction.TypeId = operationsType.First().Id;
                }
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (id is not null)
            {
                var result = await FinancialTransactionRepository.GetByIdAsync((int)id);
                if (result is not null)
                {
                    financialTransaction = result;
                }
                else
                {
                    NavigationManager.NavigateTo("/FinancialTransaction");
                }
            }
        }

        private async Task ValidSubmit()
        {
            if (id is null)
            {
                await FinancialTransactionRepository.CreateAsync(financialTransaction);
                NavigationManager.NavigateTo("/FinancialTransactions");
            }
            else
            {
                await FinancialTransactionRepository.UpdateAsync(financialTransaction);
                NavigationManager.NavigateTo("/FinancialTransactions");
            }
        }

        private async Task DeleteFinancialTransaction()
        {
            await FinancialTransactionRepository.DeleteAsync(financialTransaction);
            NavigationManager.NavigateTo("/FinancialTransactions");
        }
    }
}