using Kuaminika.KobFlow.ExpenseService;
using Kuaniminka.KobFlow.ToolBox;
using Microsoft.AspNetCore.Mvc;

namespace Kuaminika.KobFlow.API.Expense.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        IExpenseService service;
        IKLogTool logTool;

        public ExpenseController(IConfiguration configs)
        {
            KContainer container = new KContainer(configs);
            service = container.Service;
            logTool = container.LogTool;
        }

        [HttpGet(Name = "Get")]
        public KRequestReceipt<List<ExpenseModel>> Get()
        {
            List<ExpenseModel> merchants = new List<ExpenseModel>();
            try
            {
                merchants = service.GetAll();
                return new KRequestReceipt<List<ExpenseModel>>(merchants);
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result = new KRequestReceipt<List<ExpenseModel>>(merchants);
                return handleError(result, ex);
            }
        }

        private KRequestReceipt<T> handleError<T>(KRequestReceipt<T> result, Exception ex)
        {
            result.Error = true;
            result.Message = $"something went wrong - {ex.Message}";
            return result;
        }

        [HttpPost(Name = "Add")]
        public KRequestReceipt<ExpenseModel> Add(ExpenseModel addMe)
        {
            try
            {
                ExpenseModel recorded = service.Add(addMe);

                KRequestReceipt<ExpenseModel> receipt = new KRequestReceipt<ExpenseModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result = new KRequestReceipt<ExpenseModel>(addMe);
                return handleError(result, ex);

            }
        }


        [HttpPost(Name = "Delete")]
        public KRequestReceipt<ExpenseModel> Delete(ExpenseModel victim)
        {
            try
            {
                ExpenseModel recorded = service.Delete(victim);

                KRequestReceipt<ExpenseModel> receipt = new KRequestReceipt<ExpenseModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<ExpenseModel>(victim);
                return handleError(result, ex);
            }
        }

        [HttpPost(Name = "Update")]
        public KRequestReceipt<ExpenseModel> Update(ExpenseModel victim)
        {
            try
            {
                ExpenseModel updatded = service.Update(victim);

                KRequestReceipt<ExpenseModel> receipt = new KRequestReceipt<ExpenseModel>(updatded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<ExpenseModel>(victim);
                return handleError(result, ex);
            }
        }
    }
}