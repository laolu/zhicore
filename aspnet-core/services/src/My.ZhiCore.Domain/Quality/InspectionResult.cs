using System;
using Volo.Abp.Domain.Entities;

namespace MesNet.Quality
{
    /// <summary>
    /// 检验结果实体
    /// </summary>
    public class InspectionResult : Entity<Guid>
    {
        /// <summary>
        /// 所属检验项目ID
        /// </summary>
        public Guid InspectionItemId { get; private set; }

        /// <summary>
        /// 检验结果值
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// 检验人员
        /// </summary>
        public string Inspector { get; private set; }

        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime InspectionTime { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; private set; }

        protected InspectionResult()
        {
        }

        public InspectionResult(
            Guid id,
            Guid inspectionItemId,
            object value,
            string inspector,
            string remark = null)
        {
            Id = id;
            InspectionItemId = inspectionItemId;
            Value = value;
            Inspector = inspector;
            InspectionTime = DateTime.Now;
            Remark = remark;
        }
    }
}