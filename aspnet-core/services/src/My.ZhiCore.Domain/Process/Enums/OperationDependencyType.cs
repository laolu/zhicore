namespace My.ZhiCore.Production.Process
{
    /// <summary>
    /// 工序依赖关系类型
    /// </summary>
    public enum OperationDependencyType
    {
        /// <summary>
        /// 开始-开始：前置工序开始后，后续工序才能开始
        /// </summary>
        StartToStart = 1,

        /// <summary>
        /// 开始-完成：前置工序开始后，后续工序才能完成
        /// </summary>
        StartToFinish = 2,

        /// <summary>
        /// 完成-开始：前置工序完成后，后续工序才能开始
        /// </summary>
        FinishToStart = 3,

        /// <summary>
        /// 完成-完成：前置工序完成后，后续工序才能完成
        /// </summary>
        FinishToFinish = 4,

        /// <summary>
        /// 并行：工序可以同时进行
        /// </summary>
        Parallel = 5
    }
}