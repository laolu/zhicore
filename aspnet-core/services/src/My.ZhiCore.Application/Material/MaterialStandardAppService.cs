using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料标准应用服务，用于管理物料的标准和规范
    /// </summary>
    public class MaterialStandardAppService : ApplicationService
    {
        private readonly IRepository<Material, Guid> _materialRepository;
        private readonly MaterialManager _materialManager;
        private readonly ILogger<MaterialStandardAppService> _logger;

        public MaterialStandardAppService(
            IRepository<Material, Guid> materialRepository,
            MaterialManager materialManager,
            ILogger<MaterialStandardAppService> logger)
        {
            _materialRepository = materialRepository;
            _materialManager = materialManager;
            _logger = logger;
        }

        /// <summary>
        /// 设置物料标准
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="standardCode">标准编号</param>
        /// <param name="standardName">标准名称</param>
        /// <param name="standardVersion">标准版本</param>
        /// <param name="description">标准描述</param>
        public async Task SetStandardAsync(
            Guid materialId,
            string standardCode,
            string standardName,
            string standardVersion,
            string description)
        {
            var material = await _materialRepository.GetAsync(materialId);
            await _materialManager.SetStandardAsync(
                material,
                standardCode,
                standardName,
                standardVersion,
                description);
            _logger.LogInformation($"物料 {material.Name} 已设置标准：{standardName} ({standardVersion})");
        }

        /// <summary>
        /// 更新物料标准
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="standardVersion">新标准版本</param>
        /// <param name="description">更新说明</param>
        public async Task UpdateStandardAsync(
            Guid materialId,
            string standardVersion,
            string description)
        {
            var material = await _materialRepository.GetAsync(materialId);
            await _materialManager.UpdateStandardAsync(material, standardVersion, description);
            _logger.LogInformation($"物料 {material.Name} 的标准已更新到版本 {standardVersion}");
        }

        /// <summary>
        /// 获取物料标准历史记录
        /// </summary>
        /// <param name="materialId">物料ID</param>
        public async Task<List<MaterialStandardHistory>> GetStandardHistoryAsync(Guid materialId)
        {
            return await Repository.GetListAsync<MaterialStandardHistory>(
                h => h.MaterialId == materialId,
                orderBy: q => q.OrderByDescending(h => h.CreationTime));
        }

        /// <summary>
        /// 验证物料是否符合标准
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <returns>验证结果</returns>
        public async Task<MaterialStandardValidationResult> ValidateStandardAsync(Guid materialId)
        {
            var material = await _materialRepository.GetAsync(materialId);
            var result = await _materialManager.ValidateStandardAsync(material);
            _logger.LogInformation($"物料 {material.Name} 的标准验证结果：{result.IsValid}");
            return result;
        }

        /// <summary>
        /// 获取使用特定标准的物料列表
        /// </summary>
        /// <param name="standardCode">标准编号</param>
        /// <param name="standardVersion">标准版本</param>
        public async Task<List<Material>> GetMaterialsByStandardAsync(
            string standardCode,
            string standardVersion = null)
        {
            return await _materialRepository.GetListAsync(
                m => m.StandardCode == standardCode &&
                     (standardVersion == null || m.StandardVersion == standardVersion));
        }
    }
}