using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序预警应用服务
    /// </summary>
    public class OperationAlertAppService : ApplicationService, IOperationAlertAppService
    {
        private readonly OperationAlertManager _alertManager;
        private readonly ILogger<OperationAlertAppService> _logger;

        public OperationAlertAppService(
            OperationAlertManager alertManager,
            ILogger<OperationAlertAppService> logger)
        {
            _alertManager = alertManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建预警规则
        /// </summary>
        public async Task<OperationAlertRuleDto> CreateAlertRuleAsync(CreateOperationAlertRuleDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始创建工序预警规则，工序ID：{OperationId}", input.OperationId);
                var rule = await _alertManager.CreateAlertRuleAsync(
                    input.OperationId,
                    input.Name,
                    input.Description,
                    input.Condition,
                    input.Severity,
                    input.Actions);
                _logger.LogInformation("工序预警规则创建成功，规则ID：{RuleId}", rule.Id);
                return ObjectMapper.Map<OperationAlertRule, OperationAlertRuleDto>(rule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建工序预警规则失败，工序ID：{OperationId}", input.OperationId);
                throw new UserFriendlyException("创建工序预警规则失败", ex);
            }
        }

        /// <summary>
        /// 获取预警规则列表
        /// </summary>
        public async Task<List<OperationAlertRuleDto>> GetAlertRulesAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序预警规则列表，工序ID：{OperationId}", operationId);
                var rules = await _alertManager.GetAlertRulesAsync(operationId);
                return ObjectMapper.Map<List<OperationAlertRule>, List<OperationAlertRuleDto>>(rules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序预警规则列表失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序预警规则列表失败", ex);
            }
        }

        /// <summary>
        /// 处理预警
        /// </summary>
        public async Task<OperationAlertDto> HandleAlertAsync(HandleOperationAlertDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始处理工序预警，预警ID：{AlertId}", input.AlertId);
                var alert = await _alertManager.HandleAlertAsync(
                    input.AlertId,
                    input.HandlingResult,
                    input.Comments);
                _logger.LogInformation("工序预警处理完成，预警ID：{AlertId}", alert.Id);
                return ObjectMapper.Map<OperationAlert, OperationAlertDto>(alert);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理工序预警失败，预警ID：{AlertId}", input.AlertId);
                throw new UserFriendlyException("处理工序预警失败", ex);
            }
        }

        /// <summary>
        /// 获取活动预警列表
        /// </summary>
        public async Task<List<OperationAlertDto>> GetActiveAlertsAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序活动预警列表，工序ID：{OperationId}", operationId);
                var alerts = await _alertManager.GetActiveAlertsAsync(operationId);
                return ObjectMapper.Map<List<OperationAlert>, List<OperationAlertDto>>(alerts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序活动预警列表失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序活动预警列表失败", ex);
            }
        }
    }
}