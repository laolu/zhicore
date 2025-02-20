using System;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Production.Workshop.Events
{
    [EventName("My.ZhiCore.Production.Workshop.WorkshopStatusChanged")]
    public class WorkshopStatusChangedEto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public WorkshopType Type { get; set; }
        public bool IsActive { get; set; }
        public DateTime StatusChangedTime { get; set; }
    }
}