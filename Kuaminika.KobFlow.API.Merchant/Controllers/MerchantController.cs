using Kuaminika.KobFlow.MerchantService;
using Kuaniminka.KobFlow.ToolBox;
using Microsoft.AspNetCore.Mvc;

namespace Kuaminika.KobFlow.API.Merchant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MerchantController:ControllerBase
    {
        IMerchhantService merchantService;
        IKLogTool logTool;
        
        public MerchantController(IConfiguration configs)
        {
            KContainer container = new KContainer(configs);
            merchantService = container.Service;
            logTool = container.LogTool;
        }

        [HttpGet(Name = "GetMerchants")]
        [Route("/")]
        public KRequestReceipt<List<MerchantModel>> Get()
        {
            this.logTool.Action = "GetAllMerchants";
            List<MerchantModel> merchants = new List<MerchantModel>();
            try
            {
                merchants = merchantService.GetAllMerchants();
                return new KRequestReceipt<List<MerchantModel>>(merchants);
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result =     new KRequestReceipt<List<MerchantModel>>(merchants);
                return handleError(result, ex);
            }
        }

        [Route("GetAssigned")]
        public KRequestReceipt<List<MerchantModel_Assigned>> AllAssignedRecords()
        {
            
            List<MerchantModel_Assigned> merchants = new List<MerchantModel_Assigned>();
            try
            {
                this.logTool.Action = "GetAllAssignedMerchants";
                merchants = merchantService.GetAllAssignedRecords();
                return new KRequestReceipt<List<MerchantModel_Assigned>>(merchants);
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);
                var result = new KRequestReceipt<List<MerchantModel_Assigned>>(merchants);
                return handleError(result, ex);
            }
        }

        private KRequestReceipt<T> handleError<T> (KRequestReceipt<T> result, Exception ex)
        {
            result.Error = true;
            result.Message = $"something went wrong - {ex.Message}";
            return result;
        }

        [HttpPost(Name = "AddMerchants")]
        [Route("Add")]
        public KRequestReceipt<MerchantModel> Add(MerchantModel addMe)
        {
            try
            {
                this.logTool.Action = "AddMerchant";
                MerchantModel recorded = merchantService.AddMerchant(addMe);

                KRequestReceipt<MerchantModel> receipt = new KRequestReceipt<MerchantModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method==null?"":method.Name);
                var result = new KRequestReceipt<MerchantModel>(addMe);
                return handleError(result, ex);

            }
        }


        [HttpPost(Name = "DeleteMerchant")]
        [Route("Delete")]
        public KRequestReceipt<MerchantModel> Delete(MerchantModel victim)
        {
            try
            {
                this.logTool.Action = "DeleteMerchant";
                MerchantModel recorded = merchantService.DeleteMerchant(victim);

                KRequestReceipt<MerchantModel> receipt = new KRequestReceipt<MerchantModel>(recorded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<MerchantModel>(victim);
                return handleError(result, ex);
            }
        }

        [HttpPost(Name = "UpdateMerchant")]

        [Route("Update")]
        public KRequestReceipt<MerchantModel> Update(MerchantModel victim)
        {
            try
            {
                this.logTool.Action = "UpdateMerchant";
                MerchantModel updatded = merchantService.UpdateMerchant(victim);

                KRequestReceipt<MerchantModel> receipt = new KRequestReceipt<MerchantModel>(updatded);
                return receipt;
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                logTool.LogError($"{ex.Message}\n{ex.StackTrace}", method == null ? "" : method.Name);

                var result = new KRequestReceipt<MerchantModel>(victim);
                return handleError(result, ex);
            }
        }
    }
}