using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序反馈管理器 - 负责收集和处理工序执行的反馈信息
    /// </summary>
    public class OperationFeedbackManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationFeedback, Guid> _feedbackRepository;

        public OperationFeedbackManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationFeedback, Guid> feedbackRepository)
        {
            _operationRepository = operationRepository;
            _feedbackRepository = feedbackRepository;
        }

        /// <summary>
        /// 提交工序反馈
        /// </summary>
        public async Task<OperationFeedback> SubmitFeedbackAsync(
            Guid operationId,
            string content,
            FeedbackType type,
            FeedbackPriority priority,
            string submitterName)
        {
            var operation = await _operationRepository.GetAsync(operationId);

            var feedback = new OperationFeedback
            {
                OperationId = operationId,
                Content = content,
                Type = type,
                Priority = priority,
                SubmitterName = submitterName,
                Status = FeedbackStatus.Submitted,
                SubmissionTime = Clock.Now
            };

            await _feedbackRepository.InsertAsync(feedback);

            await LocalEventBus.PublishAsync(
                new OperationFeedbackSubmittedEto
                {
                    Id = feedback.Id,
                    OperationId = feedback.OperationId,
                    Type = feedback.Type,
                    Priority = feedback.Priority
                });

            return feedback;
        }

        /// <summary>
        /// 更新工序反馈
        /// </summary>
        public async Task<OperationFeedback> UpdateFeedbackAsync(
            Guid feedbackId,
            string content,
            FeedbackType type,
            FeedbackPriority priority)
        {
            var feedback = await _feedbackRepository.GetAsync(feedbackId);

            feedback.Content = content;
            feedback.Type = type;
            feedback.Priority = priority;
            feedback.LastModificationTime = Clock.Now;

            await _feedbackRepository.UpdateAsync(feedback);

            await LocalEventBus.PublishAsync(
                new OperationFeedbackUpdatedEto
                {
                    Id = feedback.Id,
                    OperationId = feedback.OperationId,
                    Type = feedback.Type,
                    Priority = feedback.Priority
                });

            return feedback;
        }

        /// <summary>
        /// 处理工序反馈
        /// </summary>
        public async Task ProcessFeedbackAsync(
            Guid feedbackId,
            string response,
            string processorName)
        {
            var feedback = await _feedbackRepository.GetAsync(feedbackId);

            feedback.Status = FeedbackStatus.Processed;
            feedback.Response = response;
            feedback.ProcessorName = processorName;
            feedback.ProcessingTime = Clock.Now;

            await _feedbackRepository.UpdateAsync(feedback);

            await LocalEventBus.PublishAsync(
                new OperationFeedbackProcessedEto
                {
                    Id = feedback.Id,
                    OperationId = feedback.OperationId,
                    ProcessorName = feedback.ProcessorName
                });
        }

        /// <summary>
        /// 关闭工序反馈
        /// </summary>
        public async Task CloseFeedbackAsync(
            Guid feedbackId,
            string resolution,
            string closedByName)
        {
            var feedback = await _feedbackRepository.GetAsync(feedbackId);

            feedback.Status = FeedbackStatus.Closed;
            feedback.Resolution = resolution;
            feedback.ClosedByName = closedByName;
            feedback.ClosureTime = Clock.Now;

            await _feedbackRepository.UpdateAsync(feedback);

            await LocalEventBus.PublishAsync(
                new OperationFeedbackClosedEto
                {
                    Id = feedback.Id,
                    OperationId = feedback.OperationId,
                    Resolution = feedback.Resolution
                });
        }

        /// <summary>
        /// 重新打开工序反馈
        /// </summary>
        public async Task ReopenFeedbackAsync(
            Guid feedbackId,
            string reopenReason)
        {
            var feedback = await _feedbackRepository.GetAsync(feedbackId);

            feedback.Status = FeedbackStatus.Reopened;
            feedback.ReopenReason = reopenReason;
            feedback.ReopenTime = Clock.Now;

            await _feedbackRepository.UpdateAsync(feedback);

            await LocalEventBus.PublishAsync(
                new OperationFeedbackReopenedEto
                {
                    Id = feedback.Id,
                    OperationId = feedback.OperationId,
                    ReopenReason = feedback.ReopenReason
                });
        }
    }
}