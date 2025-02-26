using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序标准应用服务
    /// </summary>
    public class OperationStandardAppService : ApplicationService, IOperationStandardAppService
    {
        private readonly OperationStandardManager _standardManager;
        private readonly ILogger<OperationStandardAppService> _logger;

        public OperationStandardAppService(
            OperationStandardManager standardManager,
            ILogger<OperationStandardAppService> logger)
        {
            _standardManager = standardManager;
            _logger = logger;
        }

        /// <summary>
        /// 添加工序标准
        /// </summary>
        public async Task<OperationStandardDto> AddStandardAsync(CreateOperationStandardDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始添加工序标准，工序ID：{OperationId}", input.OperationId);
                var standard = await _standardManager.AddStandardAsync(
                    input.OperationId,
                    input.Title,
                    input.Content,
                    input.Category);
                _logger.LogInformation("工序标准添加成功，ID：{Id}", standard.Id);
                return ObjectMapper.Map<OperationStandard, OperationStandardDto>(standard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加工序标准失败，工序ID：{OperationId}", input.OperationId);
                throw new UserFriendlyException("添加工序标准失败", ex);
            }
        }

        /// <summary>
        /// 更新工序标准
        /// </summary>
        public async Task<OperationStandardDto> UpdateStandardAsync(UpdateOperationStandardDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始更新工序标准，标准ID：{StandardId}", input.Id);
                var standard = await _standardManager.UpdateStandardAsync(
                    input.Id,
                    input.Title,
                    input.Content,
                    input.Category);
                _logger.LogInformation("工序标准更新成功，ID：{Id}", standard.Id);
                return ObjectMapper.Map<OperationStandard, OperationStandardDto>(standard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新工序标准失败，标准ID：{StandardId}", input.Id);
                throw new UserFriendlyException("更新工序标准失败", ex);
            }
        }

        /// <summary>
        /// 获取工序标准列表
        /// </summary>
        public async Task<List<OperationStandardDto>> GetStandardsAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序标准列表，工序ID：{OperationId}", operationId);
                var standards = await _standardManager.GetStandardsAsync(operationId);
                return ObjectMapper.Map<List<OperationStandard>, List<OperationStandardDto>>(standards);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序标准列表失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序标准列表失败", ex);
            }
        }
    }
}