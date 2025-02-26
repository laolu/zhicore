using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序审批应用服务
    /// </summary>
    public class OperationApprovalAppService : ApplicationService, IOperationApprovalAppService
    {
        private readonly OperationApprovalManager _approvalManager;
        private readonly ILogger<OperationApprovalAppService> _logger;

        public OperationApprovalAppService(
            OperationApprovalManager approvalManager,
            ILogger<OperationApprovalAppService> logger)
        {
            _approvalManager = approvalManager;
            _logger = logger;
        }

        /// <summary>
        /// 提交工序审批
        /// </summary>
        public async Task<OperationApprovalDto> SubmitApprovalAsync(CreateOperationApprovalDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始提交工序审批，工序ID：{OperationId}", input.OperationId);
                var approval = await _approvalManager.SubmitApprovalAsync(
                    input.OperationId,
                    input.ApprovalType,
                    input.Description);
                _logger.LogInformation("工序审批提交成功，审批ID：{Id}", approval.Id);
                return ObjectMapper.Map<OperationApproval, OperationApprovalDto>(approval);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "工序审批提交失败，工序ID：{OperationId}", input.OperationId);
                throw new UserFriendlyException("工序审批提交失败", ex);
            }
        }

        /// <summary>
        /// 审批工序
        /// </summary>
        public async Task<OperationApprovalDto> ApproveOperationAsync(ApproveOperationDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始审批工序，审批ID：{ApprovalId}", input.ApprovalId);
                var approval = await _approvalManager.ApproveOperationAsync(
                    input.ApprovalId,
                    input.IsApproved,
                    input.Comments);
                _logger.LogInformation("工序审批完成，审批ID：{Id}", approval.Id);
                return ObjectMapper.Map<OperationApproval, OperationApprovalDto>(approval);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "工序审批失败，审批ID：{ApprovalId}", input.ApprovalId);
                throw new UserFriendlyException("工序审批失败", ex);
            }
        }

        /// <summary>
        /// 获取工序审批历史
        /// </summary>
        public async Task<List<OperationApprovalDto>> GetApprovalHistoryAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序审批历史，工序ID：{OperationId}", operationId);
                var approvals = await _approvalManager.GetApprovalHistoryAsync(operationId);
                return ObjectMapper.Map<List<OperationApproval>, List<OperationApprovalDto>>(approvals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序审批历史失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序审批历史失败", ex);
            }
        }
    }
}