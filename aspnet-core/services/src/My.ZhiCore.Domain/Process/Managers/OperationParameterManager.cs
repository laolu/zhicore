using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序参数管理器 - 负责处理工序参数相关的领域逻辑和事件
    /// </summary>
    public class OperationParameterManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationParameter, Guid> _operationParameterRepository;
        private readonly IRepository<OperationParameterGroup, Guid> _parameterGroupRepository;

        public OperationParameterManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationParameter, Guid> operationParameterRepository,
            IRepository<OperationParameterGroup, Guid> parameterGroupRepository)
        {
            _operationRepository = operationRepository;
            _operationParameterRepository = operationParameterRepository;
            _parameterGroupRepository = parameterGroupRepository;
        }

        /// <summary>
        /// 设置工序参数
        /// </summary>
        public async Task SetParametersAsync(Guid operationId, List<OperationParameter> parameters)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            
            foreach (var parameter in parameters)
            {
                parameter.OperationId = operationId;
                await _operationParameterRepository.InsertAsync(parameter);
            }

            await LocalEventBus.PublishAsync(
                new OperationParametersSetEto
                {
                    OperationId = operationId,
                    ParameterIds = parameters.Select(p => p.Id).ToList()
                });
        }

        /// <summary>
        /// 创建参数组
        /// </summary>
        public async Task<OperationParameterGroup> CreateParameterGroupAsync(
            string name,
            string code,
            Guid? parentGroupId = null)
        {
            var group = new OperationParameterGroup
            {
                Name = name,
                Code = code,
                ParentGroupId = parentGroupId
            };

            await _parameterGroupRepository.InsertAsync(group);

            await LocalEventBus.PublishAsync(
                new OperationParameterGroupCreatedEto
                {
                    Id = group.Id,
                    Name = group.Name,
                    Code = group.Code,
                    ParentGroupId = group.ParentGroupId
                });

            return group;
        }

        /// <summary>
        /// 获取工序参数列表
        /// </summary>
        public async Task<List<OperationParameter>> GetOperationParametersAsync(Guid operationId)
        {
            return await _operationParameterRepository.GetListAsync(p => p.OperationId == operationId);
        }

        /// <summary>
        /// 更新参数值
        /// </summary>
        public async Task UpdateParameterValueAsync(
            Guid parameterId,
            string value)
        {
            var parameter = await _operationParameterRepository.GetAsync(parameterId);
            parameter.Value = value;

            await _operationParameterRepository.UpdateAsync(parameter);

            await LocalEventBus.PublishAsync(
                new OperationParameterValueUpdatedEto
                {
                    ParameterId = parameterId,
                    OperationId = parameter.OperationId,
                    Value = value
                });
        }

        /// <summary>
        /// 删除参数
        /// </summary>
        public async Task DeleteParameterAsync(Guid parameterId)
        {
            var parameter = await _operationParameterRepository.GetAsync(parameterId);
            await _operationParameterRepository.DeleteAsync(parameter);

            await LocalEventBus.PublishAsync(
                new OperationParameterDeletedEto
                {
                    ParameterId = parameterId,
                    OperationId = parameter.OperationId
                });
        }
    }
}