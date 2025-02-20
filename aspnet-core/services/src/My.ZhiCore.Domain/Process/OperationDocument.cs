using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序文档实体
    /// </summary>
    public class ProcessStepDocument : Entity<Guid>
    {
        /// <summary>
        /// 所属工序Id
        /// </summary>
        public Guid ProcessStepId { get; private set; }

        /// <summary>
        /// 文档类型
        /// </summary>
        public DocumentType DocumentType { get; private set; }

        /// <summary>
        /// 文档编号
        /// </summary>
        public string DocumentCode { get; private set; }

        /// <summary>
        /// 文档标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 文档版本
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// 上传人
        /// </summary>
        public string Uploader { get; private set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; private set; }

        /// <summary>
        /// 文档状态
        /// </summary>
        public DocumentStatus Status { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; private set; }

        protected ProcessStepDocument()
        {
        }

        public ProcessStepDocument(
            Guid id,
            Guid processStepId,
            DocumentType documentType,
            string documentCode,
            string title,
            string version,
            string filePath,
            string uploader,
            string remarks)
        {
            Id = id;
            ProcessStepId = processStepId;
            SetDocumentType(documentType);
            SetDocumentCode(documentCode);
            SetTitle(title);
            SetVersion(version);
            SetFilePath(filePath);
            SetUploader(uploader);
            UploadTime = DateTime.Now;
            Status = DocumentStatus.Active;
            SetRemarks(remarks);
        }

        private void SetDocumentType(DocumentType documentType)
        {
            if (!Enum.IsDefined(typeof(DocumentType), documentType))
            {
                throw new ArgumentException("无效的文档类型", nameof(documentType));
            }

            DocumentType = documentType;
        }

        private void SetDocumentCode(string documentCode)
        {
            if (string.IsNullOrWhiteSpace(documentCode))
            {
                throw new ArgumentException("文档编号不能为空", nameof(documentCode));
            }

            if (documentCode.Length > 50)
            {
                throw new ArgumentException("文档编号长度不能超过50个字符", nameof(documentCode));
            }

            DocumentCode = documentCode;
        }

        private void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("文档标题不能为空", nameof(title));
            }

            if (title.Length > 200)
            {
                throw new ArgumentException("文档标题长度不能超过200个字符", nameof(title));
            }

            Title = title;
        }

        private void SetVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
            {
                throw new ArgumentException("文档版本不能为空", nameof(version));
            }

            if (version.Length > 20)
            {
                throw new ArgumentException("文档版本长度不能超过20个字符", nameof(version));
            }

            Version = version;
        }

        private void SetFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("文件路径不能为空", nameof(filePath));
            }

            if (filePath.Length > 500)
            {
                throw new ArgumentException("文件路径长度不能超过500个字符", nameof(filePath));
            }

            FilePath = filePath;
        }

        private void SetUploader(string uploader)
        {
            if (string.IsNullOrWhiteSpace(uploader))
            {
                throw new ArgumentException("上传人不能为空", nameof(uploader));
            }

            if (uploader.Length > 50)
            {
                throw new ArgumentException("上传人长度不能超过50个字符", nameof(uploader));
            }

            Uploader = uploader;
        }

        private void SetRemarks(string remarks)
        {
            if (!string.IsNullOrEmpty(remarks) && remarks.Length > 500)
            {
                throw new ArgumentException("备注长度不能超过500个字符", nameof(remarks));
            }

            Remarks = remarks;
        }

        /// <summary>
        /// 更新文档信息
        /// </summary>
        public void Update(
            DocumentType documentType,
            string documentCode,
            string title,
            string version,
            string filePath,
            string remarks)
        {
            SetDocumentType(documentType);
            SetDocumentCode(documentCode);
            SetTitle(title);
            SetVersion(version);
            SetFilePath(filePath);
            SetRemarks(remarks);
        }

        /// <summary>
        /// 作废文档
        /// </summary>
        public void Invalidate()
        {
            Status = DocumentStatus.Invalid;
        }

        /// <summary>
        /// 激活文档
        /// </summary>
        public void Activate()
        {
            Status = DocumentStatus.Active;
        }
    }
}