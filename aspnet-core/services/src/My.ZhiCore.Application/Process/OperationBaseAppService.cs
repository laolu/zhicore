using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序应用服务
    /// 遵循DDD设计原则，通过领域服务（Manager）实现核心业务逻辑
    /// </summary>
    public class ProcessAppService : ApplicationService, IProcessAppService
    {
        private readonly OperationManager _operationManager;
        private readonly OperationProcessManager _processManager;
        private readonly OperationPlanManager _planManager;
        private readonly OperationQualityManager _qualityManager;
        private readonly OperationAnalysisManager _analysisManager;

        public ProcessAppService(
            OperationManager operationManager,
            OperationProcessManager processManager,
            OperationPlanManager planManager,
            OperationQualityManager qualityManager,
            OperationAnalysisManager analysisManager)
        {            
            _operationManager = operationManager;
            _processManager = processManager;
            _planManager = planManager;
            _qualityManager = qualityManager;
            _analysisManager = analysisManager;
        }

        /// <summary>
        /// 暂停工序
        /// </summary>
        public async Task PauseOperationAsync(Guid operationId)
        {
            await _operationManager.PauseOperationAsync(operationId);
        }

        /// <summary>
        /// 恢复工序
        /// </summary>
        public async Task ResumeOperationAsync(Guid operationId)
        {
            await _operationManager.ResumeOperationAsync(operationId);
        }

        /// <summary>
        /// 获取工序工艺列表
        /// </summary>
        public async Task<List<OperationProcessDto>> GetOperationProcessesAsync(Guid operationId)
        {
            var processes = await _processManager.GetOperationProcessesAsync(operationId);
            return ObjectMapper.Map<List<OperationProcess>, List<OperationProcessDto>>(processes);
        }

        /// <summary>
        /// 创建工序计划
        /// </summary>
        public async Task<OperationPlanDto> CreateOperationPlanAsync(CreateOperationPlanDto input)
        {
            var plan = await _planManager.CreateOperationPlanAsync(
                input.OperationId,
                input.PlanName,
                input.StartTime,
                input.EndTime,
                input.Quantity);
            return ObjectMapper.Map<OperationPlan, OperationPlanDto>(plan);
        }

        /// <summary>
        /// 调整工序计划
        /// </summary>
        public async Task<OperationPlanDto> AdjustOperationPlanAsync(AdjustOperationPlanDto input)
        {
            var plan = await _planManager.AdjustOperationPlanAsync(
                input.PlanId,
                input.StartTime,
                input.EndTime,
                input.Quantity);
            return ObjectMapper.Map<OperationPlan, OperationPlanDto>(plan);
        }

        /// <summary>
        /// 设置工艺参数
        /// </summary>
        public async Task<OperationProcessDto> SetProcessParametersAsync(SetProcessParametersDto input)
        {
            var process = await _processManager.SetProcessParametersAsync(
                input.ProcessId,
                input.Parameters);
            return ObjectMapper.Map<OperationProcess, OperationProcessDto>(process);
        }

        /// <summary>
        /// 工艺验证
        /// </summary>
        public async Task<bool> ValidateProcessAsync(Guid processId)
        {
            return await _processManager.ValidateProcessAsync(processId);
        }

        /// <summary>
        /// 质量检验
        /// </summary>
        public async Task<OperationQualityDto> InspectQualityAsync(InspectQualityDto input)
        {
            var quality = await _qualityManager.InspectQualityAsync(
                input.OperationId,
                input.InspectionItems);
            return ObjectMapper.Map<OperationQuality, OperationQualityDto>(quality);
        }

        /// <summary>
        /// 处理不合格品
        /// </summary>
        public async Task HandleDefectiveAsync(HandleDefectiveDto input)
        {
            await _qualityManager.HandleDefectiveAsync(
                input.OperationId,
                input.DefectiveId,
                input.HandleMethod,
                input.Remark);
        }

        /// <summary>
        /// 效率分析
        /// </summary>
        public async Task<OperationEfficiencyDto> AnalyzeEfficiencyAsync(Guid operationId)
        {
            var efficiency = await _analysisManager.AnalyzeEfficiencyAsync(operationId);
            return ObjectMapper.Map<OperationEfficiency, OperationEfficiencyDto>(efficiency);
        }

        /// <summary>
        /// 异常分析
        /// </summary>
        public async Task<List<OperationAnomalyDto>> AnalyzeAnomaliesAsync(AnalyzeAnomalyDto input)
        {
            var anomalies = await _analysisManager.AnalyzeAnomaliesAsync(
                input.OperationId,
                input.StartTime,
                input.EndTime);
            return ObjectMapper.Map<List<OperationAnomaly>, List<OperationAnomalyDto>>(anomalies);
        }
    }
}