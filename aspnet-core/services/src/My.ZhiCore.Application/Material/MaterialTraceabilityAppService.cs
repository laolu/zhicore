using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料追溯应用服务
    /// </summary>
    public class MaterialTraceabilityAppService : ApplicationService
    {
        private readonly MaterialTraceabilityManager _traceabilityManager;
        private readonly ILogger<MaterialTraceabilityAppService> _logger;

        public MaterialTraceabilityAppService(
            MaterialTraceabilityManager traceabilityManager,
            ILogger<MaterialTraceabilityAppService> logger)
        {
            _traceabilityManager = traceabilityManager;
            _logger = logger;
        }

        /// <summary>
        /// 记录物料流转
        /// </summary>
        public async Task<MaterialTraceabilityDto> RecordFlowAsync(RecordMaterialFlowDto input)
        {
            try
            {
                _logger.LogInformation("开始记录物料流转，物料ID：{MaterialId}", input.MaterialId);
                var traceability = await _traceabilityManager.RecordFlowAsync(
                    input.MaterialId,
                    input.FlowType,
                    input.FromLocationId,
                    input.ToLocationId,
                    input.Quantity,
                    input.BatchNo,
                    input.OperatorId,
                    input.OperationTime,
                    input.Remark);
                _logger.LogInformation("物料流转记录成功，记录ID：{Id}", traceability.Id);
                return ObjectMapper.Map<MaterialTraceability, MaterialTraceabilityDto>(traceability);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "记录物料流转失败，物料ID：{MaterialId}", input.MaterialId);
                throw new UserFriendlyException("记录物料流转失败", ex);
            }
        }

        /// <summary>
        /// 获取物料流转记录
        /// </summary>
        public async Task<List<MaterialTraceabilityDto>> GetFlowRecordsAsync(Guid materialId, string batchNo = null)
        {
            var records = await _traceabilityManager.GetFlowRecordsAsync(materialId, batchNo);
            return ObjectMapper.Map<List<MaterialTraceability>, List<MaterialTraceabilityDto>>(records);
        }

        /// <summary>
        /// 获取物料批次追溯信息
        /// </summary>
        public async Task<MaterialTraceabilityDto> GetBatchTraceabilityAsync(string batchNo)
        {
            var traceability = await _traceabilityManager.GetBatchTraceabilityAsync(batchNo);
            return ObjectMapper.Map<MaterialTraceability, MaterialTraceabilityDto>(traceability);
        }
    }
}