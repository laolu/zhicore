using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备模板管理器 - 负责设备模板的业务逻辑和领域规则
    /// </summary>
    public class EquipmentTemplateManager : DomainService
    {
        private readonly IRepository<EquipmentTemplate, Guid> _equipmentTemplateRepository;

        public EquipmentTemplateManager(
            IRepository<EquipmentTemplate, Guid> equipmentTemplateRepository)
        {
            _equipmentTemplateRepository = equipmentTemplateRepository;
        }

        /// <summary>
        /// 创建设备模板
        /// </summary>
        public async Task<EquipmentTemplate> CreateAsync(
            string code,
            string name,
            string equipmentType,
            string specification,
            string manufacturer,
            int maintenancePeriod,
            string remark = null)
        {
            await ValidateTemplateCodeAsync(code);

            var template = new EquipmentTemplate(
                GuidGenerator.Create(),
                code,
                name,
                equipmentType,
                specification,
                manufacturer,
                maintenancePeriod,
                remark
            );

            return await _equipmentTemplateRepository.InsertAsync(template);
        }

        /// <summary>
        /// 更新设备模板
        /// </summary>
        public async Task<EquipmentTemplate> UpdateAsync(
            Guid id,
            string name,
            string equipmentType,
            string specification,
            string manufacturer,
            int maintenancePeriod,
            string remark = null)
        {
            var template = await _equipmentTemplateRepository.GetAsync(id);
            
            template.Update(
                name,
                equipmentType,
                specification,
                manufacturer,
                maintenancePeriod,
                remark
            );

            return await _equipmentTemplateRepository.UpdateAsync(template);
        }

        /// <summary>
        /// 验证模板编码唯一性
        /// </summary>
        private async Task ValidateTemplateCodeAsync(string code)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));

            var exists = await _equipmentTemplateRepository
                .AnyAsync(t => t.Code == code);

            if (exists)
            {
                throw new BusinessException(ZhiCoreDomainErrorCodes.EquipmentTemplateCodeAlreadyExists)
                    .WithData("code", code);
            }
        }

        /// <summary>
        /// 添加技术参数
        /// </summary>
        public async Task AddTechnicalParameterAsync(
            Guid id,
            string key,
            string value)
        {
            var template = await _equipmentTemplateRepository.GetAsync(id);
            template.AddTechnicalParameter(key, value);
            await _equipmentTemplateRepository.UpdateAsync(template);
        }

        /// <summary>
        /// 移除技术参数
        /// </summary>
        public async Task RemoveTechnicalParameterAsync(
            Guid id,
            string key)
        {
            var template = await _equipmentTemplateRepository.GetAsync(id);
            template.RemoveTechnicalParameter(key);
            await _equipmentTemplateRepository.UpdateAsync(template);
        }

        /// <summary>
        /// 清空技术参数
        /// </summary>
        public async Task ClearTechnicalParametersAsync(Guid id)
        {
            var template = await _equipmentTemplateRepository.GetAsync(id);
            template.ClearTechnicalParameters();
            await _equipmentTemplateRepository.UpdateAsync(template);
        }
    }
}