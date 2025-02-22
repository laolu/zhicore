using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备审批应用服务，用于管理设备的审核和审批流程
    /// </summary>
    public class EquipmentApprovalAppService : ApplicationService
    {
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly ILogger<EquipmentApprovalAppService> _logger;

        public EquipmentApprovalAppService(
            IRepository<Equipment, Guid> equipmentRepository,
            ILogger<EquipmentApprovalAppService> logger)
        {
            _equipmentRepository = equipmentRepository;
            _logger = logger;
        }

        /// <summary>
        /// 提交设备审批
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="approvalType">审批类型</param>
        /// <param name="description">审批说明</param>
        public async Task SubmitApprovalAsync(
            Guid equipmentId,
            string approvalType,
            string description)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            equipment.UpdateApprovalStatus(ApprovalStatus.Pending);
            await _equipmentRepository.UpdateAsync(equipment);

            _logger.LogInformation($"设备 {equipment.Name} 已提交{approvalType}审批，说明：{description}");
        }

        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="remarks">备注</param>
        public async Task ApproveAsync(Guid equipmentId, string remarks)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            equipment.UpdateApprovalStatus(ApprovalStatus.Approved);
            await _equipmentRepository.UpdateAsync(equipment);

            _logger.LogInformation($"设备 {equipment.Name} 审批已通过，备注：{remarks}");
        }

        /// <summary>
        /// 审批驳回
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="reason">驳回原因</param>
        /// <param name="remarks">备注</param>
        public async Task RejectAsync(Guid equipmentId, string reason, string remarks)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            equipment.UpdateApprovalStatus(ApprovalStatus.Rejected);
            await _equipmentRepository.UpdateAsync(equipment);

            _logger.LogInformation($"设备 {equipment.Name} 审批已驳回，原因：{reason}，备注：{remarks}");
        }

        /// <summary>
        /// 撤销审批
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="reason">撤销原因</param>
        public async Task CancelAsync(Guid equipmentId, string reason)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            equipment.UpdateApprovalStatus(ApprovalStatus.Cancelled);
            await _equipmentRepository.UpdateAsync(equipment);

            _logger.LogInformation($"设备 {equipment.Name} 审批已撤销，原因：{reason}");
        }
    }
}