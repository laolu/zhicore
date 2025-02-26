using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序变更管理应用服务
    /// </summary>
    public class OperationChangeAppService : ApplicationService, IOperationChangeAppService
    {
        private readonly OperationChangeManager _changeManager;
        private readonly ILogger<OperationChangeAppService> _logger;

        public OperationChangeAppService(
            OperationChangeManager changeManager,
            ILogger<OperationChangeAppService> logger)
        {
            _changeManager = changeManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建工序变更申请
        /// </summary>
        public async Task<OperationChangeRequestDto> CreateChangeRequestAsync(CreateOperationChangeRequestDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始创建工序变更申请，工序ID：{OperationId}", input.OperationId);
                var request = await _changeManager.CreateChangeRequestAsync(
                    input.OperationId,
                    input.Title,
                    input.Description,
                    input.ChangeType,
                    input.Priority);
                _logger.LogInformation("工序变更申请创建成功，申请ID：{RequestId}", request.Id);
                return ObjectMapper.Map<OperationChangeRequest, OperationChangeRequestDto>(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建工序变更申请失败，工序ID：{OperationId}", input.OperationId);
                throw new UserFriendlyException("创建工序变更申请失败", ex);
            }
        }

        /// <summary>
        /// 审批工序变更申请
        /// </summary>
        public async Task<OperationChangeRequestDto> ApproveChangeRequestAsync(ApproveOperationChangeRequestDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始审批工序变更申请，申请ID：{RequestId}", input.RequestId);
                var request = await _changeManager.ApproveChangeRequestAsync(
                    input.RequestId,
                    input.ApprovalResult,
                    input.Comments);
                _logger.LogInformation("工序变更申请审批完成，申请ID：{RequestId}", request.Id);
                return ObjectMapper.Map<OperationChangeRequest, OperationChangeRequestDto>(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "审批工序变更申请失败，申请ID：{RequestId}", input.RequestId);
                throw new UserFriendlyException("审批工序变更申请失败", ex);
            }
        }

        /// <summary>
        /// 获取工序变更历史记录
        /// </summary>
        public async Task<List<OperationChangeHistoryDto>> GetChangeHistoryAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序变更历史记录，工序ID：{OperationId}", operationId);
                var history = await _changeManager.GetChangeHistoryAsync(operationId);
                return ObjectMapper.Map<List<OperationChangeHistory>, List<OperationChangeHistoryDto>>(history);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序变更历史记录失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序变更历史记录失败", ex);
            }
        }
    }
}