using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料追溯管理器，负责处理物料追溯记录的业务逻辑和生命周期管理
    /// </summary>
    public class MaterialTraceabilityManager : DomainService
    {
        private readonly IRepository<MaterialTraceability, Guid> _materialTraceabilityRepository;

        public MaterialTraceabilityManager(
            IRepository<MaterialTraceability, Guid> materialTraceabilityRepository)
        {
            _materialTraceabilityRepository = materialTraceabilityRepository;
        }

        /// <summary>
        /// 创建物料追溯记录
        /// </summary>
        public async Task<MaterialTraceability> CreateAsync(
            Guid materialId,
            string batchNumber,
            string sourceType,
            string sourceDocumentNumber,
            string sourceParty,
            DateTime? manufactureDate,
            DateTime? expiryDate,
            decimal quantity,
            string location)
        {
            var traceability = new MaterialTraceability(
                GuidGenerator.Create(),
                materialId,
                batchNumber,
                sourceType,
                sourceDocumentNumber,
                sourceParty,
                manufactureDate,
                expiryDate,
                quantity,
                location);

            return await _materialTraceabilityRepository.InsertAsync(traceability);
        }

        /// <summary>
        /// 记录质检结果
        /// </summary>
        public async Task RecordInspectionAsync(
            Guid traceabilityId,
            string inspectionResult,
            string inspector,
            DateTime inspectionDate,
            string qualityStatus)
        {
            var traceability = await _materialTraceabilityRepository.GetAsync(traceabilityId);
            traceability.RecordInspection(inspectionResult, inspector, inspectionDate, qualityStatus);
            await _materialTraceabilityRepository.UpdateAsync(traceability);
        }

        /// <summary>
        /// 更新物料状态
        /// </summary>
        public async Task UpdateStatusAsync(Guid traceabilityId, string newStatus, string remarks = null)
        {
            var traceability = await _materialTraceabilityRepository.GetAsync(traceabilityId);
            traceability.UpdateStatus(newStatus, remarks);
            await _materialTraceabilityRepository.UpdateAsync(traceability);
        }

        /// <summary>
        /// 更新数量
        /// </summary>
        public async Task UpdateQuantityAsync(Guid traceabilityId, decimal newQuantity)
        {
            var traceability = await _materialTraceabilityRepository.GetAsync(traceabilityId);
            traceability.UpdateQuantity(newQuantity);
            await _materialTraceabilityRepository.UpdateAsync(traceability);
        }

        /// <summary>
        /// 更新存储位置
        /// </summary>
        public async Task UpdateLocationAsync(Guid traceabilityId, string newLocation)
        {
            var traceability = await _materialTraceabilityRepository.GetAsync(traceabilityId);
            traceability.UpdateLocation(newLocation);
            await _materialTraceabilityRepository.UpdateAsync(traceability);
        }
    }
}