using System;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料类型枚举，用于定义系统中不同种类的物料
    /// </summary>
    public enum MaterialType
    {
        /// <summary>
        /// 原材料：用于生产过程中的基础材料，如金属、塑料、木材等
        /// </summary>
        RawMaterial = 0,

        /// <summary>
        /// 半成品：生产过程中的中间产品，已经过部分加工但尚未完成的产品
        /// </summary>
        SemiFinishedProduct = 1,

        /// <summary>
        /// 成品：已完成全部生产过程，可以直接销售或使用的产品
        /// </summary>
        FinishedProduct = 2,

        /// <summary>
        /// 包装材料：用于产品包装的各类材料，如纸箱、塑料袋、标签等
        /// </summary>
        PackagingMaterial = 3,

        /// <summary>
        /// 消耗品：在生产过程中会被消耗掉的辅助材料，如清洁用品、润滑油等
        /// </summary>
        ConsumableMaterial = 4,

        /// <summary>
        /// 备件：用于设备维修和更换的零部件
        /// </summary>
        SparePart = 5
    }
}