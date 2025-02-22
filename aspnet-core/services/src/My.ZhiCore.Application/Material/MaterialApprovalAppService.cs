using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料审批应用服务，用于管理物料的审核和审批流程
    /// </summary>
    public class MaterialApprovalAppService : ApplicationService
    {
        private readonly IRepository<Material, Guid> _materialRepository;
        private readonly MaterialManager _materialManager;
        private readonly ILogger<MaterialApprovalAppService> _logger;

        public MaterialApprovalAppService(
            IRepository<Material, Guid> materialRepository,
            MaterialManager materialManager,
            ILogger<MaterialApprovalAppService> logger)
        {
            _materialRepository = materialRepository;
            _materialManager = materialManager;
            _logger = logger;
        }

        /// <summary>
        /// 提交物料审批
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="approvalType">审批类型</param>
        /// <param name="remarks">备注</param>
        public async Task SubmitApprovalAsync(
            Guid materialId,
            MaterialApprovalType approvalType,
            string remarks)
        {
            var material = await _materialRepository.GetAsync(materialId);
            await _materialManager.SubmitApprovalAsync(material, approvalType, remarks);
            _logger.LogInformation($"物料 {material.Name} 已提交{approvalType}审批");
        }

        /// <summary>
        /// 审批物料
        /// </summary>
        /// <param name="approvalId">审批ID</param>
        /// <param name="approved">是否通过</param>
        /// <param name="comments">审批意见</param>
        public async Task ApproveAsync(
            Guid approvalId,
            bool approved,
            string comments)
        {
            var approval = await Repository.GetAsync<MaterialApproval>(approvalId);
            var material = await _materialRepository.GetAsync(approval.MaterialId);

            if (approved)
            {
                await _materialManager.ApproveAsync(approval, comments);
                _logger.LogInformation($"物料 {material.Name} 的{approval.ApprovalType}审批已通过");
            }
            else
            {
                await _materialManager.RejectAsync(approval, comments);
                _logger.LogInformation($"物料 {material.Name} 的{approval.ApprovalType}审批已拒绝");
            }
        }

        /// <summary>
        /// 获取物料的审批历史
        /// </summary>
        /// <param name="materialId">物料ID</param>
        public async Task<List<MaterialApproval>> GetApprovalHistoryAsync(Guid materialId)
        {
            return await Repository.GetListAsync<MaterialApproval>(
                a => a.MaterialId == materialId,
                orderBy: q => q.OrderByDescending(a => a.CreationTime));
        }

        /// <summary>
        /// 获取待审批的物料列表
        /// </summary>
        /// <param name="approvalType">审批类型</param>
        public async Task<List<MaterialApproval>> GetPendingApprovalsAsync(MaterialApprovalType? approvalType = null)
        {
            return await Repository.GetListAsync<MaterialApproval>(
                a => !a.IsCompleted && (approvalType == null || a.ApprovalType == approvalType),
                orderBy: q => q.OrderByDescending(a => a.CreationTime));
        }
    }
}