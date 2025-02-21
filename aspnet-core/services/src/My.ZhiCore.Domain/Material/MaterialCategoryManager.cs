using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料分类管理器，负责处理物料分类的业务规则和领域事件
    /// </summary>
    public class MaterialCategoryManager : DomainService
    {
        private readonly IRepository<MaterialCategory, Guid> _materialCategoryRepository;

        public MaterialCategoryManager(
            IRepository<MaterialCategory, Guid> materialCategoryRepository)
        {
            _materialCategoryRepository = materialCategoryRepository;
        }

        /// <summary>
        /// 创建物料分类
        /// </summary>
        public async Task<MaterialCategory> CreateAsync(
            string name,
            string code,
            Guid? parentId,
            string description,
            int sortOrder,
            Guid? attributeTemplateId)
        {
            await ValidateCodeAsync(code);
            var path = await CalculatePathAsync(parentId);

            var category = new MaterialCategory(
                GuidGenerator.Create(),
                name,
                code,
                parentId,
                path,
                description,
                sortOrder,
                attributeTemplateId
            );

            return await _materialCategoryRepository.InsertAsync(category);
        }

        /// <summary>
        /// 更新物料分类
        /// </summary>
        public async Task<MaterialCategory> UpdateAsync(
            Guid id,
            string name,
            string code,
            string description,
            int sortOrder,
            Guid? attributeTemplateId)
        {
            var category = await _materialCategoryRepository.GetAsync(id);
            
            if (category.Code != code)
            {
                await ValidateCodeAsync(code);
            }

            category.Update(name, code, description, sortOrder, attributeTemplateId);
            return await _materialCategoryRepository.UpdateAsync(category);
        }

        /// <summary>
        /// 更新物料分类状态
        /// </summary>
        public async Task<MaterialCategory> UpdateStatusAsync(Guid id, bool isActive)
        {
            var category = await _materialCategoryRepository.GetAsync(id);
            category.UpdateStatus(isActive);
            return await _materialCategoryRepository.UpdateAsync(category);
        }

        /// <summary>
        /// 移动物料分类
        /// </summary>
        public async Task<MaterialCategory> MoveAsync(Guid id, Guid? newParentId)
        {
            var category = await _materialCategoryRepository.GetAsync(id);
            var newPath = await CalculatePathAsync(newParentId);

            // 验证是否形成循环引用
            if (newParentId.HasValue)
            {
                var parent = await _materialCategoryRepository.GetAsync(newParentId.Value);
                if (parent.Path.StartsWith(category.Path))
                {
                    throw new BusinessException("ZhiCore:CannotMoveToSubCategory")
                        .WithData("CategoryId", id)
                        .WithData("NewParentId", newParentId);
                }
            }

            // 更新当前分类的路径
            category.UpdatePath(newPath);

            // 更新所有子分类的路径
            var children = await _materialCategoryRepository.GetListAsync(c => c.Path.StartsWith(category.Path));
            foreach (var child in children)
            {
                var childNewPath = child.Path.Replace(category.Path, newPath);
                child.UpdatePath(childNewPath);
                await _materialCategoryRepository.UpdateAsync(child);
            }

            return await _materialCategoryRepository.UpdateAsync(category);
        }

        /// <summary>
        /// 验证分类编码唯一性
        /// </summary>
        private async Task ValidateCodeAsync(string code)
        {
            var exists = await _materialCategoryRepository.AnyAsync(c => c.Code == code);
            if (exists)
            {
                throw new BusinessException("ZhiCore:DuplicateCategoryCode")
                    .WithData("Code", code);
            }
        }

        /// <summary>
        /// 计算分类路径
        /// </summary>
        private async Task<string> CalculatePathAsync(Guid? parentId)
        {
            if (!parentId.HasValue)
            {
                return "/";
            }

            var parent = await _materialCategoryRepository.GetAsync(parentId.Value);
            return parent.Path.TrimEnd('/') + "/" + parentId.Value + "/";
        }
    }
}