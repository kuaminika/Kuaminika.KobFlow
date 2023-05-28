﻿using Kuaminika.KobFlow.MerchantService;
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
        public KRequestReceipt<List<MerchantModel>> Get()
        {
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

        private KRequestReceipt<T> handleError<T> (KRequestReceipt<T> result, Exception ex)
        {
            result.Error = true;
            result.Message = $"something went wrong - {ex.Message}";
            return result;
        }

        [HttpPost(Name = "AddMerchants")]
        public KRequestReceipt<MerchantModel> Add(MerchantModel addMe)
        {
            try
            {
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
        public KRequestReceipt<MerchantModel> Delete(MerchantModel victim)
        {
            try
            {
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
        public KRequestReceipt<MerchantModel> Update(MerchantModel victim)
        {
            try
            {
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