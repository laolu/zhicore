using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序资源需求实体
    /// </summary>
    public class ProcessStepResource : Entity<Guid>
    {
        /// <summary>
        /// 所属工序Id
        /// </summary>
        public Guid ProcessStepId { get; private set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public ResourceType ResourceType { get; private set; }

        /// <summary>
        /// 资源编号
        /// </summary>
        public string ResourceCode { get; private set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName { get; private set; }

        /// <summary>
        /// 需求数量
        /// </summary>
        public int RequiredQuantity { get; private set; }

        /// <summary>
        /// 资源规格
        /// </summary>
        public string Specification { get; private set; }

        /// <summary>
        /// 是否必需
        /// </summary>
        public bool IsMandatory { get; private set; }

        /// <summary>
        /// 资源描述
        /// </summary>
        public string Description { get; private set; }

        protected ProcessStepResource()
        {
        }

        public ProcessStepResource(
            Guid id,
            Guid processStepId,
            ResourceType resourceType,
            string resourceCode,
            string resourceName,
            int requiredQuantity,
            string specification,
            bool isMandatory,
            string description)
        {
            Id = id;
            ProcessStepId = processStepId;
            SetResourceType(resourceType);
            SetResourceCode(resourceCode);
            SetResourceName(resourceName);
            SetRequiredQuantity(requiredQuantity);
            SetSpecification(specification);
            IsMandatory = isMandatory;
            SetDescription(description);
        }

        private void SetResourceType(ResourceType resourceType)
        {
            if (!Enum.IsDefined(typeof(ResourceType), resourceType))
            {
                throw new ArgumentException("无效的资源类型", nameof(resourceType));
            }

            ResourceType = resourceType;
        }

        private void SetResourceCode(string resourceCode)
        {
            if (string.IsNullOrWhiteSpace(resourceCode))
            {
                throw new ArgumentException("资源编号不能为空", nameof(resourceCode));
            }

            if (resourceCode.Length > 50)
            {
                throw new ArgumentException("资源编号长度不能超过50个字符", nameof(resourceCode));
            }

            ResourceCode = resourceCode;
        }

        private void SetResourceName(string resourceName)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentException("资源名称不能为空", nameof(resourceName));
            }

            if (resourceName.Length > 100)
            {
                throw new ArgumentException("资源名称长度不能超过100个字符", nameof(resourceName));
            }

            ResourceName = resourceName;
        }

        private void SetRequiredQuantity(int requiredQuantity)
        {
            if (requiredQuantity <= 0)
            {
                throw new ArgumentException("需求数量必须大于0", nameof(requiredQuantity));
            }

            RequiredQuantity = requiredQuantity;
        }

        private void SetSpecification(string specification)
        {
            if (!string.IsNullOrEmpty(specification) && specification.Length > 200)
            {
                throw new ArgumentException("资源规格长度不能超过200个字符", nameof(specification));
            }

            Specification = specification;
        }

        private void SetDescription(string description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length > 500)
            {
                throw new ArgumentException("资源描述长度不能超过500个字符", nameof(description));
            }

            Description = description;
        }

        /// <summary>
        /// 更新资源需求信息
        /// </summary>
        public void Update(
            int requiredQuantity,
            string specification,
            bool isMandatory,
            string description)
        {
            SetRequiredQuantity(requiredQuantity);
            SetSpecification(specification);
            IsMandatory = isMandatory;
            SetDescription(description);
        }
    }
}