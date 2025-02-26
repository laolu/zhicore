using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Equipment.Dtos;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备基础应用服务
    /// </summary>
    public class EquipmentBaseAppService : ApplicationService, IEquipmentBaseAppService
    {
        private readonly EquipmentManager _equipmentManager;
        private readonly ILogger<EquipmentBaseAppService> _logger;

        public EquipmentBaseAppService(
            EquipmentManager equipmentManager,
            ILogger<EquipmentBaseAppService> logger)
        {
            _equipmentManager = equipmentManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建设备
        /// </summary>
        public async Task<EquipmentDto> CreateAsync(CreateEquipmentDto input)
        {
            try
            {
                _logger.LogInformation("开始创建设备，编码：{Code}", input.Code);
                var equipment = await _equipmentManager.CreateAsync(
                    input.Code,
                    input.Name);
                _logger.LogInformation("设备创建成功，ID：{Id}", equipment.Id);
                return ObjectMapper.Map<Equipment, EquipmentDto>(equipment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建设备失败，编码：{Code}", input.Code);
                throw new UserFriendlyException("创建设备失败", ex);
            }
        }

        /// <summary>
        /// 更新设备
        /// </summary>
        public async Task<EquipmentDto> UpdateAsync(UpdateEquipmentDto input)
        {
            try
            {
                _logger.LogInformation("开始更新设备，ID：{Id}", input.Id);
                var equipment = await _equipmentManager.GetAsync(input.Id);
                equipment = await _equipmentManager.UpdateAsync(
                    equipment,
                    input.Name);
                _logger.LogInformation("设备更新成功，ID：{Id}", equipment.Id);
                return ObjectMapper.Map<Equipment, EquipmentDto>(equipment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新设备失败，ID：{Id}", input.Id);
                throw new UserFriendlyException("更新设备失败", ex);
            }
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("开始删除设备，ID：{Id}", id);
                var equipment = await _equipmentManager.GetAsync(id);
                await _equipmentManager.DeleteAsync(equipment);
                _logger.LogInformation("设备删除成功，ID：{Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除设备失败，ID：{Id}", id);
                throw new UserFriendlyException("删除设备失败", ex);
            }
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        public async Task<EquipmentDto> GetAsync(Guid id)
        {
            try
            {
                var equipment = await _equipmentManager.GetAsync(id);
                return ObjectMapper.Map<Equipment, EquipmentDto>(equipment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取设备信息失败，ID：{Id}", id);
                throw new UserFriendlyException("获取设备信息失败", ex);
            }
        }

        /// <summary>
        /// 获取设备列表
        /// </summary>
        public async Task<List<EquipmentDto>> GetListAsync()
        {
            try
            {
                var equipments = await _equipmentManager.GetListAsync();
                return ObjectMapper.Map<List<Equipment>, List<EquipmentDto>>(equipments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取设备列表失败");
                throw new UserFriendlyException("获取设备列表失败", ex);
            }
        }
    }
}