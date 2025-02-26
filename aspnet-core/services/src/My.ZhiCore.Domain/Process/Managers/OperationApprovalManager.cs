using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序审核管理器 - 负责处理工序的审核和审批流程
    /// </summary>
    public class OperationApprovalManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationApproval, Guid> _operationApprovalRepository;

        public OperationApprovalManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationApproval, Guid> operationApprovalRepository)
        {
            _operationRepository = operationRepository;
            _operationApprovalRepository = operationApprovalRepository;
        }

        /// <summary>
        /// 提交工序审核
        /// </summary>
        public async Task<OperationApproval> SubmitForApprovalAsync(
            Guid operationId,
            string comments)
        {
            var operation = await _operationRepository.GetAsync(operationId);

            var approval = new OperationApproval
            {
                OperationId = operationId,
                Status = OperationApprovalStatus.Pending,
                SubmissionTime = Clock.Now,
                Comments = comments
            };

            await _operationApprovalRepository.InsertAsync(approval);

            await LocalEventBus.PublishAsync(
                new OperationApprovalSubmittedEto
                {
                    Id = approval.Id,
                    OperationId = approval.OperationId,
                    Status = approval.Status
                });

            return approval;
        }

        /// <summary>
        /// 审批工序
        /// </summary>
        public async Task<OperationApproval> ApproveAsync(
            Guid approvalId,
            bool isApproved,
            string comments)
        {
            var approval = await _operationApprovalRepository.GetAsync(approvalId);
            
            approval.Status = isApproved ? OperationApprovalStatus.Approved : OperationApprovalStatus.Rejected;
            approval.ApprovalTime = Clock.Now;
            approval.Comments = comments;

            await _operationApprovalRepository.UpdateAsync(approval);

            await LocalEventBus.PublishAsync(
                new OperationApprovalCompletedEto
                {
                    Id = approval.Id,
                    OperationId = approval.OperationId,
                    Status = approval.Status,
                    IsApproved = isApproved
                });

            return approval;
        }

        /// <summary>
        /// 撤回审核申请
        /// </summary>
        public async Task WithdrawApprovalAsync(Guid approvalId)
        {
            var approval = await _operationApprovalRepository.GetAsync(approvalId);
            
            if (approval.Status != OperationApprovalStatus.Pending)
            {
                throw new InvalidOperationException("只能撤回待审核状态的申请");
            }

            approval.Status = OperationApprovalStatus.Withdrawn;
            approval.WithdrawalTime = Clock.Now;

            await _operationApprovalRepository.UpdateAsync(approval);

            await LocalEventBus.PublishAsync(
                new OperationApprovalWithdrawnEto
                {
                    Id = approval.Id,
                    OperationId = approval.OperationId
                });
        }
    }
}