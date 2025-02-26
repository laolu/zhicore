using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序配置应用服务
    /// </summary>
    public class OperationConfigurationAppService : ApplicationService, IOperationConfigurationAppService
    {
        private readonly OperationConfigurationManager _configManager;
        private readonly ILogger<OperationConfigurationAppService> _logger;

        public OperationConfigurationAppService(
            OperationConfigurationManager configManager,
            ILogger<OperationConfigurationAppService> logger)
        {
            _configManager = configManager;
            _logger = logger;
        }

        /// <summary>
        /// 设置工序配置参数
        /// </summary>
        public async Task<OperationConfigurationDto> SetConfigurationAsync(SetOperationConfigurationDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始设置工序配置参数，工序ID：{OperationId}", input.OperationId);
                var config = await _configManager.SetConfigurationAsync(
                    input.OperationId,
                    input.Parameters);
                _logger.LogInformation("工序配置参数设置成功，工序ID：{OperationId}", input.OperationId);
                return ObjectMapper.Map<OperationConfiguration, OperationConfigurationDto>(config);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "设置工序配置参数失败，工序ID：{OperationId}", input.OperationId);
                throw new UserFriendlyException("设置工序配置参数失败", ex);
            }
        }

        /// <summary>
        /// 获取工序配置参数
        /// </summary>
        public async Task<OperationConfigurationDto> GetConfigurationAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序配置参数，工序ID：{OperationId}", operationId);
                var config = await _configManager.GetConfigurationAsync(operationId);
                return ObjectMapper.Map<OperationConfiguration, OperationConfigurationDto>(config);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序配置参数失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序配置参数失败", ex);
            }
        }

        /// <summary>
        /// 验证工序配置参数
        /// </summary>
        public async Task<ValidationResultDto> ValidateConfigurationAsync(ValidateOperationConfigurationDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始验证工序配置参数，工序ID：{OperationId}", input.OperationId);
                var result = await _configManager.ValidateConfigurationAsync(
                    input.OperationId,
                    input.Parameters);
                return ObjectMapper.Map<ValidationResult, ValidationResultDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "验证工序配置参数失败，工序ID：{OperationId}", input.OperationId);
                throw new UserFriendlyException("验证工序配置参数失败", ex);
            }
        }
    }
}