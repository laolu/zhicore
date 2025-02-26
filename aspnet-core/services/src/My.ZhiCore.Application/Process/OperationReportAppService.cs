using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序报表应用服务
    /// </summary>
    public class OperationReportAppService : ApplicationService, IOperationReportAppService
    {
        private readonly OperationReportManager _reportManager;
        private readonly ILogger<OperationReportAppService> _logger;

        public OperationReportAppService(
            OperationReportManager reportManager,
            ILogger<OperationReportAppService> logger)
        {
            _reportManager = reportManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取工序执行情况统计报表
        /// </summary>
        public async Task<OperationExecutionReportDto> GetExecutionReportAsync(GetOperationReportInput input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始生成工序执行情况统计报表，工序ID：{OperationId}", input.OperationId);
                var report = await _reportManager.GetExecutionReportAsync(
                    input.OperationId,
                    input.StartTime,
                    input.EndTime);
                _logger.LogInformation("工序执行情况统计报表生成成功");
                return ObjectMapper.Map<OperationExecutionReport, OperationExecutionReportDto>(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "生成工序执行情况统计报表失败，工序ID：{OperationId}", input.OperationId);
                throw new UserFriendlyException("生成工序执行情况统计报表失败", ex);
            }
        }

        /// <summary>
        /// 获取工序资源使用统计报表
        /// </summary>
        public async Task<OperationResourceUsageReportDto> GetResourceUsageReportAsync(GetOperationReportInput input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始生成工序资源使用统计报表，工序ID：{OperationId}", input.OperationId);
                var report = await _reportManager.GetResourceUsageReportAsync(
                    input.OperationId,
                    input.StartTime,
                    input.EndTime);
                _logger.LogInformation("工序资源使用统计报表生成成功");
                return ObjectMapper.Map<OperationResourceUsageReport, OperationResourceUsageReportDto>(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "生成工序资源使用统计报表失败，工序ID：{OperationId}", input.OperationId);
                throw new UserFriendlyException("生成工序资源使用统计报表失败", ex);
            }
        }

        /// <summary>
        /// 获取工序质量分析报表
        /// </summary>
        public async Task<OperationQualityReportDto> GetQualityReportAsync(GetOperationReportInput input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始生成工序质量分析报表，工序ID：{OperationId}", input.OperationId);
                var report = await _reportManager.GetQualityReportAsync(
                    input.OperationId,
                    input.StartTime,
                    input.EndTime);
                _logger.LogInformation("工序质量分析报表生成成功");
                return ObjectMapper.Map<OperationQualityReport, OperationQualityReportDto>(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "生成工序质量分析报表失败，工序ID：{OperationId}", input.OperationId);
                throw new UserFriendlyException("生成工序质量分析报表失败", ex);
            }
        }
    }
}