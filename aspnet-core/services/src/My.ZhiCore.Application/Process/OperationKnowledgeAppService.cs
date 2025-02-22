using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序知识库应用服务
    /// </summary>
    public class OperationKnowledgeAppService : ApplicationService, IOperationKnowledgeAppService
    {
        private readonly OperationKnowledgeManager _knowledgeManager;
        private readonly ILogger<OperationKnowledgeAppService> _logger;

        public OperationKnowledgeAppService(
            OperationKnowledgeManager knowledgeManager,
            ILogger<OperationKnowledgeAppService> logger)
        {
            _knowledgeManager = knowledgeManager;
            _logger = logger;
        }

        /// <summary>
        /// 添加工序最佳实践
        /// </summary>
        public async Task<OperationBestPracticeDto> AddBestPracticeAsync(CreateOperationBestPracticeDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始添加工序最佳实践，工序ID：{OperationId}", input.OperationId);
                var bestPractice = await _knowledgeManager.AddBestPracticeAsync(
                    input.OperationId,
                    input.Title,
                    input.Description,
                    input.Category);
                _logger.LogInformation("工序最佳实践添加成功，ID：{Id}", bestPractice.Id);
                return ObjectMapper.Map<OperationBestPractice, OperationBestPracticeDto>(bestPractice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加工序最佳实践失败，工序ID：{OperationId}", input.OperationId);
                throw new UserFriendlyException("添加工序最佳实践失败", ex);
            }
        }

        /// <summary>
        /// 获取工序最佳实践列表
        /// </summary>
        public async Task<List<OperationBestPracticeDto>> GetBestPracticesAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序最佳实践列表，工序ID：{OperationId}", operationId);
                var bestPractices = await _knowledgeManager.GetBestPracticesAsync(operationId);
                return ObjectMapper.Map<List<OperationBestPractice>, List<OperationBestPracticeDto>>(bestPractices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序最佳实践列表失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序最佳实践列表失败", ex);
            }
        }

        /// <summary>
        /// 添加工序常见问题
        /// </summary>
        public async Task<OperationFaqDto> AddFaqAsync(CreateOperationFaqDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始添加工序常见问题，工序ID：{OperationId}", input.OperationId);
                var faq = await _knowledgeManager.AddFaqAsync(
                    input.OperationId,
                    input.Question,
                    input.Answer,
                    input.Category);
                _logger.LogInformation("工序常见问题添加成功，ID：{Id}", faq.Id);
                return ObjectMapper.Map<OperationFaq, OperationFaqDto>(faq);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加工序常见问题失败，工序ID：{OperationId}", input.OperationId);
                throw new UserFriendlyException("添加工序常见问题失败", ex);
            }
        }

        /// <summary>
        /// 获取工序常见问题列表
        /// </summary>
        public async Task<List<OperationFaqDto>> GetFaqsAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序常见问题列表，工序ID：{OperationId}", operationId);
                var faqs = await _knowledgeManager.GetFaqsAsync(operationId);
                return ObjectMapper.Map<List<OperationFaq>, List<OperationFaqDto>>(faqs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序常见问题列表失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序常见问题列表失败", ex);
            }
        }
    }
}