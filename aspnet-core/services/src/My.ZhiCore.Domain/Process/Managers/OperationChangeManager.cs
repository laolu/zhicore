using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序变更管理器 - 负责管理工序的变更记录和变更历史
    /// </summary>
    public class OperationChangeManager : ZhiCoreDomainService
    {   
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationChange, Guid> _operationChangeRepository;

        public OperationChangeManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationChange, Guid> operationChangeRepository)
        {
            _operationRepository = operationRepository;
            _operationChangeRepository = operationChangeRepository;
        }

        /// <summary>
        /// 记录工序变更
        /// </summary>
        public async Task<OperationChange> RecordChangeAsync(
            Guid operationId,
            string changeType,
            string description,
            string oldValue,
            string newValue)
        {
            var operation = await _operationRepository.GetAsync(operationId);

            var change = new OperationChange
            {
                OperationId = operationId,
                ChangeType = changeType,
                Description = description,
                OldValue = oldValue,
                NewValue = newValue,
                ChangeTime = Clock.Now
            };

            await _operationChangeRepository.InsertAsync(change);

            await LocalEventBus.PublishAsync(
                new OperationChangeRecordedEto
                {
                    Id = change.Id,
                    OperationId = change.OperationId,
                    ChangeType = change.ChangeType,
                    Description = change.Description
                });

            return change;
        }

        /// <summary>
        /// 获取工序变更历史
        /// </summary>
        public async Task<List<OperationChange>> GetChangeHistoryAsync(Guid operationId)
        {
            return await _operationChangeRepository
                .GetListAsync(x => x.OperationId == operationId);
        }
    }
}