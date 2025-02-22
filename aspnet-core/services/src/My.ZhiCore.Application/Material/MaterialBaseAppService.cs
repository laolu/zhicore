using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料基础应用服务
    /// </summary>
    public class MaterialBaseAppService : ApplicationService
    {
        private readonly MaterialManager _materialManager;
        private readonly ILogger<MaterialBaseAppService> _logger;

        public MaterialBaseAppService(
            MaterialManager materialManager,
            ILogger<MaterialBaseAppService> logger)
        {
            _materialManager = materialManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建物料
        /// </summary>
        public async Task<MaterialDto> CreateAsync(CreateMaterialDto input)
        {
            try
            {
                _logger.LogInformation("开始创建物料，编码：{Code}", input.Code);
                var material = await _materialManager.CreateAsync(
                    input.Code,
                    input.Name,
                    input.Specification,
                    input.CategoryId);
                _logger.LogInformation("物料创建成功，ID：{Id}", material.Id);
                return ObjectMapper.Map<Material, MaterialDto>(material);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建物料失败，编码：{Code}", input.Code);
                throw new UserFriendlyException("创建物料失败", ex);
            }
        }

        /// <summary>
        /// 更新物料
        /// </summary>
        public async Task<MaterialDto> UpdateAsync(Guid id, UpdateMaterialDto input)
        {
            try
            {
                _logger.LogInformation("开始更新物料，ID：{Id}", id);
                var material = await _materialManager.UpdateAsync(
                    id,
                    input.Name,
                    input.Specification,
                    input.CategoryId);
                _logger.LogInformation("物料更新成功，ID：{Id}", id);
                return ObjectMapper.Map<Material, MaterialDto>(material);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新物料失败，ID：{Id}", id);
                throw new UserFriendlyException("更新物料失败", ex);
            }
        }

        /// <summary>
        /// 删除物料
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("开始删除物料，ID：{Id}", id);
                await _materialManager.DeleteAsync(id);
                _logger.LogInformation("物料删除成功，ID：{Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除物料失败，ID：{Id}", id);
                throw new UserFriendlyException("删除物料失败", ex);
            }
        }

        /// <summary>
        /// 获取物料
        /// </summary>
        public async Task<MaterialDto> GetAsync(Guid id)
        {
            var material = await _materialManager.GetAsync(id);
            return ObjectMapper.Map<Material, MaterialDto>(material);
        }

        /// <summary>
        /// 获取物料列表
        /// </summary>
        public async Task<List<MaterialDto>> GetListAsync()
        {
            var materials = await _materialManager.GetListAsync();
            return ObjectMapper.Map<List<Material>, List<MaterialDto>>(materials);
        }
    }
}