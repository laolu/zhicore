using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;
using System.Linq;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备备件管理器 - 用于管理设备备件的生命周期和业务规则
    /// </summary>
    public class EquipmentSparepartManager : DomainService
    {
        private readonly IRepository<EquipmentSparepart, Guid> _sparepartRepository;
        private readonly ILocalEventBus _localEventBus;

        public EquipmentSparepartManager(
            IRepository<EquipmentSparepart, Guid> sparepartRepository,
            ILocalEventBus localEventBus)
        {
            _sparepartRepository = sparepartRepository;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建新的备件
        /// </summary>
        public async Task<EquipmentSparepart> CreateAsync(
            string code,
            string name,
            string specification,
            string manufacturer,
            int stockQuantity,
            int minStockWarning,
            string unit,
            string remark = null)
        {
            await ValidateCodeAsync(code);

            var sparepart = new EquipmentSparepart(
                GuidGenerator.Create(),
                code,
                name,
                specification,
                manufacturer,
                stockQuantity,
                minStockWarning,
                unit,
                remark
            );

            await _sparepartRepository.InsertAsync(sparepart);

            // 检查是否需要触发库存预警
            if (sparepart.IsLowStock())
            {
                await _localEventBus.PublishAsync(new SparepartLowStockEvent
                {
                    SparepartId = sparepart.Id,
                    CurrentStock = sparepart.StockQuantity,
                    MinStockWarning = sparepart.MinStockWarning
                });
            }

            return sparepart;
        }

        /// <summary>
        /// 更新备件信息
        /// </summary>
        public async Task UpdateAsync(
            EquipmentSparepart sparepart,
            string name,
            string specification,
            string manufacturer,
            int minStockWarning,
            string unit,
            string remark = null)
        {
            sparepart.Update(
                name,
                specification,
                manufacturer,
                minStockWarning,
                unit,
                remark
            );

            await _sparepartRepository.UpdateAsync(sparepart);

            // 检查是否需要触发库存预警
            if (sparepart.IsLowStock())
            {
                await _localEventBus.PublishAsync(new SparepartLowStockEvent
                {
                    SparepartId = sparepart.Id,
                    CurrentStock = sparepart.StockQuantity,
                    MinStockWarning = sparepart.MinStockWarning
                });
            }
        }

        /// <summary>
        /// 入库操作
        /// </summary>
        public async Task StockInAsync(EquipmentSparepart sparepart, int quantity)
        {
            var oldStock = sparepart.StockQuantity;
            sparepart.StockIn(quantity);

            await _sparepartRepository.UpdateAsync(sparepart);

            await _localEventBus.PublishAsync(new SparepartStockChangedEvent
            {
                SparepartId = sparepart.Id,
                OldStock = oldStock,
                NewStock = sparepart.StockQuantity,
                ChangeType = StockChangeType.StockIn,
                ChangeQuantity = quantity
            });
        }

        /// <summary>
        /// 出库操作
        /// </summary>
        public async Task StockOutAsync(EquipmentSparepart sparepart, int quantity)
        {
            var oldStock = sparepart.StockQuantity;
            sparepart.StockOut(quantity);

            await _sparepartRepository.UpdateAsync(sparepart);

            await _localEventBus.PublishAsync(new SparepartStockChangedEvent
            {
                SparepartId = sparepart.Id,
                OldStock = oldStock,
                NewStock = sparepart.StockQuantity,
                ChangeType = StockChangeType.StockOut,
                ChangeQuantity = quantity
            });

            // 检查是否需要触发库存预警
            if (sparepart.IsLowStock())
            {
                await _localEventBus.PublishAsync(new SparepartLowStockEvent
                {
                    SparepartId = sparepart.Id,
                    CurrentStock = sparepart.StockQuantity,
                    MinStockWarning = sparepart.MinStockWarning
                });
            }
        }

        /// <summary>
        /// 验证备件编码唯一性
        /// </summary>
        private async Task ValidateCodeAsync(string code)
        {
            var exists = await _sparepartRepository.AnyAsync(x => x.Code == code);
            if (exists)
            {
                throw new SparepartCodeAlreadyExistsException(code);
            }
        }
    }

    /// <summary>
    /// 备件库存变更事件
    /// </summary>
    public class SparepartStockChangedEvent
    {
        public Guid SparepartId { get; set; }
        public int OldStock { get; set; }
        public int NewStock { get; set; }
        public StockChangeType ChangeType { get; set; }
        public int ChangeQuantity { get; set; }
    }

    /// <summary>
    /// 库存变更类型
    /// </summary>
    public enum StockChangeType
    {
        StockIn,
        StockOut
    }

    /// <summary>
    /// 备件库存预警事件
    /// </summary>
    public class SparepartLowStockEvent
    {
        public Guid SparepartId { get; set; }
        public int CurrentStock { get; set; }
        public int MinStockWarning { get; set; }
    }

    /// <summary>
    /// 备件编码已存在异常
    /// </summary>
    public class SparepartCodeAlreadyExistsException : Exception
    {
        public string Code { get; }

        public SparepartCodeAlreadyExistsException(string code)
            : base($"备件编码 '{code}' 已存在")
        {
            Code = code;
        }
    }
}