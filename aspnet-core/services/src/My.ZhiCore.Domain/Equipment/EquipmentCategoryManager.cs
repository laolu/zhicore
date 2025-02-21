using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备分类管理器，负责设备分类的生命周期管理和业务规则验证
    /// </summary>
    public class EquipmentCategoryManager : DomainService
    {
        private readonly IRepository<EquipmentCategory, Guid> _equipmentCategoryRepository;
        private readonly ILocalEventBus _localEventBus;

        public EquipmentCategoryManager(
            IRepository<EquipmentCategory, Guid> equipmentCategoryRepository,
            ILocalEventBus localEventBus)
        {
            _equipmentCategoryRepository = equipmentCategoryRepository;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建新的设备分类
        /// </summary>
        /// <param name="name">分类名称</param>
        /// <param name="code">分类编码</param>
        /// <param name="description">分类描述</param>
        /// <param name="parentId">父级分类ID</param>
        /// <returns>创建的设备分类实体</returns>
        public async Task<EquipmentCategory> CreateAsync(
            string name,
            string code,
            string description,
            Guid? parentId = null)
        {
            await ValidateCodeAsync(code);
            if (parentId.HasValue)
            {
                await ValidateParentExistsAsync(parentId.Value);
            }

            var category = new EquipmentCategory(
                GuidGenerator.Create(),
                name,
                code,
                description,
                parentId);

            await _equipmentCategoryRepository.InsertAsync(category);

            await _localEventBus.PublishAsync(
                new EquipmentCategoryCreatedEvent
                {
                    CategoryId = category.Id,
                    Name = category.Name,
                    Code = category.Code,
                    ParentId = category.ParentId
                });

            return category;
        }

        /// <summary>
        /// 更新设备分类
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <param name="name">新的分类名称</param>
        /// <param name="code">新的分类编码</param>
        /// <param name="description">新的分类描述</param>
        /// <param name="parentId">新的父级分类ID</param>
        /// <returns>更新后的设备分类实体</returns>
        public async Task<EquipmentCategory> UpdateAsync(
            Guid id,
            string name,
            string code,
            string description,
            Guid? parentId = null)
        {
            var category = await _equipmentCategoryRepository.GetAsync(id);

            if (category.Code != code)
            {
                await ValidateCodeAsync(code);
            }

            if (parentId.HasValue && parentId.Value != category.ParentId)
            {
                await ValidateParentExistsAsync(parentId.Value);
                await ValidateNotCircularReferenceAsync(id, parentId.Value);
            }

            category.Update(name, code, description, parentId);

            await _equipmentCategoryRepository.UpdateAsync(category);

            await _localEventBus.PublishAsync(
                new EquipmentCategoryUpdatedEvent
                {
                    CategoryId = category.Id,
                    Name = category.Name,
                    Code = category.Code,
                    ParentId = category.ParentId
                });

            return category;
        }

        /// <summary>
        /// 删除设备分类
        /// </summary>
        /// <param name="id">要删除的分类ID</param>
        public async Task DeleteAsync(Guid id)
        {
            var category = await _equipmentCategoryRepository.GetAsync(id);
            await ValidateNoChildrenAsync(id);

            await _equipmentCategoryRepository.DeleteAsync(id);

            await _localEventBus.PublishAsync(
                new EquipmentCategoryDeletedEvent
                {
                    CategoryId = id
                });
        }

        /// <summary>
        /// 验证分类编码的唯一性
        /// </summary>
        private async Task ValidateCodeAsync(string code)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            
            var exists = await _equipmentCategoryRepository.AnyAsync(x => x.Code == code);
            if (exists)
            {
                throw new BusinessException(ZhiCoreDomainErrorCodes.EquipmentCategoryCodeAlreadyExists)
                    .WithData("code", code);
            }
        }

        /// <summary>
        /// 验证父级分类是否存在
        /// </summary>
        private async Task ValidateParentExistsAsync(Guid parentId)
        {
            var exists = await _equipmentCategoryRepository.AnyAsync(x => x.Id == parentId);
            if (!exists)
            {
                throw new BusinessException(ZhiCoreDomainErrorCodes.EquipmentCategoryNotFound)
                    .WithData("id", parentId);
            }
        }

        /// <summary>
        /// 验证是否存在循环引用
        /// </summary>
        private async Task ValidateNotCircularReferenceAsync(Guid categoryId, Guid newParentId)
        {
            var parent = await _equipmentCategoryRepository.GetAsync(newParentId);
            while (parent.ParentId.HasValue)
            {
                if (parent.ParentId.Value == categoryId)
                {
                    throw new BusinessException(ZhiCoreDomainErrorCodes.EquipmentCategoryCircularReference);
                }
                parent = await _equipmentCategoryRepository.GetAsync(parent.ParentId.Value);
            }
        }

        /// <summary>
        /// 验证分类是否有子分类
        /// </summary>
        private async Task ValidateNoChildrenAsync(Guid categoryId)
        {
            var hasChildren = await _equipmentCategoryRepository.AnyAsync(x => x.ParentId == categoryId);
            if (hasChildren)
            {
                throw new BusinessException(ZhiCoreDomainErrorCodes.EquipmentCategoryHasChildren)
                    .WithData("id", categoryId);
            }
        }
    }
}