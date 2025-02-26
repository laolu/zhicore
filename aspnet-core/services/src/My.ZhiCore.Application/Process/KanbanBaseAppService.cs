using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 看板基础应用服务
    /// </summary>
    public class KanbanBaseAppService : ApplicationService
    {
        private readonly KanbanManager _kanbanManager;

        public KanbanBaseAppService(KanbanManager kanbanManager)
        {
            _kanbanManager = kanbanManager;
        }

        /// <summary>
        /// 创建看板
        /// </summary>
        public async Task<Kanban> CreateKanbanAsync(Kanban kanban)
        {
            return await _kanbanManager.CreateKanbanAsync(kanban);
        }

        /// <summary>
        /// 更新看板
        /// </summary>
        public async Task<Kanban> UpdateKanbanAsync(Guid id, Kanban kanban)
        {
            return await _kanbanManager.UpdateKanbanAsync(id, kanban);
        }

        /// <summary>
        /// 删除看板
        /// </summary>
        public async Task DeleteKanbanAsync(Guid id)
        {
            await _kanbanManager.DeleteKanbanAsync(id);
        }

        /// <summary>
        /// 获取看板列表
        /// </summary>
        public async Task<List<Kanban>> GetKanbansAsync()
        {
            return await _kanbanManager.GetKanbansAsync();
        }

        /// <summary>
        /// 获取看板详情
        /// </summary>
        public async Task<Kanban> GetKanbanAsync(Guid id)
        {
            return await _kanbanManager.GetKanbanAsync(id);
        }

        /// <summary>
        /// 设置看板布局
        /// </summary>
        public async Task<bool> SetKanbanLayoutAsync(Guid id, KanbanLayout layout)
        {
            return await _kanbanManager.SetKanbanLayoutAsync(id, layout);
        }

        /// <summary>
        /// 获取看板布局
        /// </summary>
        public async Task<KanbanLayout> GetKanbanLayoutAsync(Guid id)
        {
            return await _kanbanManager.GetKanbanLayoutAsync(id);
        }
    }
}