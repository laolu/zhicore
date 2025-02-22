using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序模板应用服务
    /// </summary>
    public class OperationTemplateAppService : ApplicationService, IOperationTemplateAppService
    {
        private readonly OperationTemplateManager _templateManager;
        private readonly ILogger<OperationTemplateAppService> _logger;

        public OperationTemplateAppService(
            OperationTemplateManager templateManager,
            ILogger<OperationTemplateAppService> logger)
        {
            _templateManager = templateManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建工序模板
        /// </summary>
        public async Task<OperationTemplateDto> CreateTemplateAsync(CreateOperationTemplateDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始创建工序模板，名称：{Name}", input.Name);
                var template = await _templateManager.CreateTemplateAsync(
                    input.Name,
                    input.Code,
                    input.Description,
                    input.Category);
                _logger.LogInformation("工序模板创建成功，ID：{Id}", template.Id);
                return ObjectMapper.Map<OperationTemplate, OperationTemplateDto>(template);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建工序模板失败，名称：{Name}", input.Name);
                throw new UserFriendlyException("创建工序模板失败", ex);
            }
        }

        /// <summary>
        /// 获取工序模板列表
        /// </summary>
        public async Task<List<OperationTemplateDto>> GetTemplatesAsync(string category = null)
        {
            try
            {
                _logger.LogInformation("获取工序模板列表，类别：{Category}", category);
                var templates = await _templateManager.GetTemplatesAsync(category);
                return ObjectMapper.Map<List<OperationTemplate>, List<OperationTemplateDto>>(templates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序模板列表失败");
                throw new UserFriendlyException("获取工序模板列表失败", ex);
            }
        }

        /// <summary>
        /// 从模板创建工序
        /// </summary>
        public async Task<OperationDto> CreateOperationFromTemplateAsync(CreateOperationFromTemplateDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始从模板创建工序，模板ID：{TemplateId}", input.TemplateId);
                var operation = await _templateManager.CreateOperationFromTemplateAsync(
                    input.TemplateId,
                    input.Name,
                    input.Code);
                _logger.LogInformation("从模板创建工序成功，工序ID：{Id}", operation.Id);
                return ObjectMapper.Map<Operation, OperationDto>(operation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "从模板创建工序失败，模板ID：{TemplateId}", input.TemplateId);
                throw new UserFriendlyException("从模板创建工序失败", ex);
            }
        }
    }
}