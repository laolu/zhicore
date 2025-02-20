using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工艺参数附件实体 - 用于管理工艺参数相关的图片和文件
    /// </summary>
    public class ProcessParameterAttachment : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 所属工艺参数ID
        /// </summary>
        public Guid ProcessParameterId { get; private set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; private set; }

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; private set; }

        /// <summary>
        /// 存储路径
        /// </summary>
        public string StoragePath { get; private set; }

        /// <summary>
        /// 附件类型（如：参考图片、结果照片等）
        /// </summary>
        public string AttachmentType { get; private set; }

        /// <summary>
        /// 附件描述
        /// </summary>
        public string Description { get; private set; }

        protected ProcessParameterAttachment() { }

        public ProcessParameterAttachment(
            Guid id,
            Guid processParameterId,
            string fileName,
            string fileType,
            long fileSize,
            string storagePath,
            string attachmentType,
            string description = null)
        {
            Id = id;
            ProcessParameterId = processParameterId;
            FileName = fileName;
            FileType = fileType;
            FileSize = fileSize;
            StoragePath = storagePath;
            AttachmentType = attachmentType;
            Description = description;
        }

        /// <summary>
        /// 更新附件描述
        /// </summary>
        public void UpdateDescription(string description)
        {
            Description = description;
        }

        /// <summary>
        /// 验证文件类型是否为图片
        /// </summary>
        public bool IsImage()
        {
            return FileType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);
        }
    }
}