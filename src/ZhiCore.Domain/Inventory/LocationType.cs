namespace ZhiCore.Domain.Inventory;

public enum LocationType
{
    /// <summary>
    /// 原料区
    /// </summary>
    RawMaterial = 1,

    /// <summary>
    /// 半成品区
    /// </summary>
    SemiFinished = 2,

    /// <summary>
    /// 成品区
    /// </summary>
    FinishedProduct = 3,

    /// <summary>
    /// 不良品区
    /// </summary>
    Defective = 4,

    /// <summary>
    /// 待检区
    /// </summary>
    Inspection = 5,

    /// <summary>
    /// 退货区
    /// </summary>
    Return = 6,

    /// <summary>
    /// 其他
    /// </summary>
    Other = 99
}