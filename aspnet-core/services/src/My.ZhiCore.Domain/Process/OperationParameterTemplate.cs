using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Production.Process
{
    /// <summary>
    /// 参数模板实体
    /// </summary>
    public class ParameterTemplate : Entity<Guid>
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; private set; }

        /// <summary>
        /// 模板代码
        /// </summary>
        public string TemplateCode { get; private set; }

        /// <summary>
        /// 适用工序类型
        /// </summary>
        public string ProcessType { get; private set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; private set; }

        /// <summary>
        /// 模板描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 参数项列表
        /// </summary>
        public ICollection<ProcessStepParameter> Parameters { get; private set; }

        protected ParameterTemplate()
        {
        }

        public ParameterTemplate(
            Guid id,
            string templateName,
            string templateCode,
            string processType,
            bool isEnabled,
            string description)
        {
            Id = id;
            SetTemplateName(templateName);
            SetTemplateCode(templateCode);
            SetProcessType(processType);
            IsEnabled = isEnabled;
            SetDescription(description);
            Parameters = new List<ProcessStepParameter>();
        }

        private void SetTemplateName(string templateName)
        {
            if (string.IsNullOrWhiteSpace(templateName))
            {
                throw new ArgumentException("模板名称不能为空", nameof(templateName));
            }

            if (templateName.Length > 100)
            {
                throw new ArgumentException("模板名称长度不能超过100个字符", nameof(templateName));
            }

            TemplateName = templateName;
        }

        private void SetTemplateCode(string templateCode)
        {
            if (string.IsNullOrWhiteSpace(templateCode))
            {
                throw new ArgumentException("模板代码不能为空", nameof(templateCode));
            }

            if (templateCode.Length > 50)
            {
                throw new ArgumentException("模板代码长度不能超过50个字符", nameof(templateCode));
            }

            TemplateCode = templateCode;
        }

        private void SetProcessType(string processType)
        {
            if (string.IsNullOrWhiteSpace(processType))
            {
                throw new ArgumentException("适用工序类型不能为空", nameof(processType));
            }

            if (processType.Length > 50)
            {
                throw new ArgumentException("适用工序类型长度不能超过50个字符", nameof(processType));
            }

            ProcessType = processType;
        }

        private void SetDescription(string description)
        {
            if (!string.IsNullOrEmpty(description) && description.Length > 500)
            {
                throw new ArgumentException("模板描述长度不能超过500个字符", nameof(description));
            }

            Description = description;
        }

        /// <summary>
        /// 更新模板信息
        /// </summary>
        public void Update(
            string templateName,
            string templateCode,
            string processType,
            bool isEnabled,
            string description)
        {
            SetTemplateName(templateName);
            SetTemplateCode(templateCode);
            SetProcessType(processType);
            IsEnabled = isEnabled;
            SetDescription(description);
            Parameters = new List<ProcessStepParameter>();
        }
    }
}