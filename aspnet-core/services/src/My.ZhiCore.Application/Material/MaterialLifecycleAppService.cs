using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料生命周期应用服务，用于管理物料的生命周期状态
    /// </summary>
    public class MaterialLifecycleAppService : ApplicationService
    {
        private readonly IRepository<Material, Guid> _materialRepository;
        private readonly MaterialManager _materialManager;
        private readonly ILogger<MaterialLifecycleAppService> _logger;

        public MaterialLifecycleAppService(
            IRepository<Material, Guid> materialRepository,
            MaterialManager materialManager,
            ILogger<MaterialLifecycleAppService> logger)
        {
            _materialRepository = materialRepository;
            _materialManager = materialManager;
            _logger = logger;
        }

        /// <summary>
        /// 更新物料生命周期状态
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="lifecycleStatus">生命周期状态</param>
        /// <param name="remarks">备注</param>
        public async Task UpdateLifecycleStatusAsync(
            Guid materialId,
            MaterialLifecycleStatus lifecycleStatus,
            string remarks)
        {
            var material = await _materialRepository.GetAsync(materialId);
            await _materialManager.UpdateLifecycleStatusAsync(material, lifecycleStatus, remarks);
            _logger.LogInformation($"物料 {material.Name} 的生命周期状态已更新为 {lifecycleStatus}");
        }

        /// <summary>
        /// 获取物料生命周期历史记录
        /// </summary>
        /// <param name="materialId">物料ID</param>
        public async Task<List<MaterialLifecycleHistory>> GetLifecycleHistoryAsync(Guid materialId)
        {
            return await Repository.GetListAsync<MaterialLifecycleHistory>(
                h => h.MaterialId == materialId,
                orderBy: q => q.OrderByDescending(h => h.CreationTime));
        }

        /// <summary>
        /// 废弃物料
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="reason">废弃原因</param>
        public async Task DeprecateAsync(Guid materialId, string reason)
        {
            var material = await _materialRepository.GetAsync(materialId);
            await _materialManager.DeprecateAsync(material, reason);
            _logger.LogWarning($"物料 {material.Name} 已被废弃，原因：{reason}");
        }

        /// <summary>
        /// 恢复废弃的物料
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="reason">恢复原因</param>
        public async Task RestoreAsync(Guid materialId, string reason)
        {
            var material = await _materialRepository.GetAsync(materialId);
            await _materialManager.RestoreAsync(material, reason);
            _logger.LogInformation($"物料 {material.Name} 已恢复使用，原因：{reason}");
        }
    }
}