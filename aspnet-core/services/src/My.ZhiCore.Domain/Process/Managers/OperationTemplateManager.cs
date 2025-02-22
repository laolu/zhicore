using System;
using System.Threading.Tasks;
using My.ZhiCore.Process.Events;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序模板管理器 - 负责管理工序模板和标准工序
    /// </summary>
    public class OperationTemplateManager : ZhiCoreDomainService
    {
        private readonly IRepository<OperationTemplate, Guid> _operationTemplateRepository;

        public OperationTemplateManager(
            IRepository<OperationTemplate, Guid> operationTemplateRepository)
        {
            _operationTemplateRepository = operationTemplateRepository;
        }

        /// <summary>
        /// 创建工序模板
        /// </summary>
        public async Task<OperationTemplate> CreateTemplateAsync(
            string name,
            string code,
            string description,
            bool isStandard)
        {
            var template = new OperationTemplate
            {
                Name = name,
                Code = code,
                Description = description,
                IsStandard = isStandard
            };

            await _operationTemplateRepository.InsertAsync(template);

            await LocalEventBus.PublishAsync(
                new OperationTemplateCreatedEto
                {
                    Id = template.Id,
                    Name = template.Name,
                    Code = template.Code,
                    IsStandard = template.IsStandard
                });

            return template;
        }

        /// <summary>
        /// 更新工序模板
        /// </summary>
        public async Task<OperationTemplate> UpdateTemplateAsync(
            Guid id,
            string name,
            string code,
            string description)
        {
            var template = await _operationTemplateRepository.GetAsync(id);

            template.Name = name;
            template.Code = code;
            template.Description = description;

            await _operationTemplateRepository.UpdateAsync(template);

            await LocalEventBus.PublishAsync(
                new OperationTemplateUpdatedEto
                {
                    Id = template.Id,
                    Name = template.Name,
                    Code = template.Code,
                    IsStandard = template.IsStandard
                });

            return template;
        }

        /// <summary>
        /// 设置为标准工序模板
        /// </summary>
        public async Task SetAsStandardAsync(Guid templateId)
        {
            var template = await _operationTemplateRepository.GetAsync(templateId);
            template.SetAsStandard();
            await _operationTemplateRepository.UpdateAsync(template);

            await LocalEventBus.PublishAsync(
                new OperationTemplateSetAsStandardEto
                {
                    Id = template.Id,
                    Name = template.Name,
                    Code = template.Code
                });
        }

        /// <summary>
        /// 取消标准工序模板
        /// </summary>
        public async Task UnsetAsStandardAsync(Guid templateId)
        {
            var template = await _operationTemplateRepository.GetAsync(templateId);
            template.UnsetAsStandard();
            await _operationTemplateRepository.UpdateAsync(template);

            await LocalEventBus.PublishAsync(
                new OperationTemplateUnsetAsStandardEto
                {
                    Id = template.Id,
                    Name = template.Name,
                    Code = template.Code
                });
        }
    }
}