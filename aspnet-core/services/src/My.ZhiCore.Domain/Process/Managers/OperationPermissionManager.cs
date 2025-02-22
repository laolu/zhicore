using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序权限管理器 - 负责管理工序的访问权限和操作权限
    /// </summary>
    public class OperationPermissionManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationPermission, Guid> _permissionRepository;

        public OperationPermissionManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationPermission, Guid> permissionRepository)
        {
            _operationRepository = operationRepository;
            _permissionRepository = permissionRepository;
        }

        /// <summary>
        /// 授予工序权限
        /// </summary>
        public async Task GrantPermissionAsync(
            Guid operationId,
            Guid userId,
            OperationPermissionType permissionType)
        {
            var operation = await _operationRepository.GetAsync(operationId);

            var permission = new OperationPermission
            {
                OperationId = operationId,
                UserId = userId,
                PermissionType = permissionType,
                GrantTime = Clock.Now
            };

            await _permissionRepository.InsertAsync(permission);

            await LocalEventBus.PublishAsync(
                new OperationPermissionGrantedEto
                {
                    Id = permission.Id,
                    OperationId = permission.OperationId,
                    UserId = permission.UserId,
                    PermissionType = permission.PermissionType
                });
        }

        /// <summary>
        /// 撤销工序权限
        /// </summary>
        public async Task RevokePermissionAsync(
            Guid operationId,
            Guid userId,
            OperationPermissionType permissionType)
        {
            var permission = await _permissionRepository.FirstOrDefaultAsync(
                p => p.OperationId == operationId &&
                      p.UserId == userId &&
                      p.PermissionType == permissionType);

            if (permission != null)
            {
                await _permissionRepository.DeleteAsync(permission);

                await LocalEventBus.PublishAsync(
                    new OperationPermissionRevokedEto
                    {
                        Id = permission.Id,
                        OperationId = permission.OperationId,
                        UserId = permission.UserId,
                        PermissionType = permission.PermissionType
                    });
            }
        }

        /// <summary>
        /// 检查用户是否具有指定工序的权限
        /// </summary>
        public async Task<bool> HasPermissionAsync(
            Guid operationId,
            Guid userId,
            OperationPermissionType permissionType)
        {
            return await _permissionRepository.AnyAsync(
                p => p.OperationId == operationId &&
                      p.UserId == userId &&
                      p.PermissionType == permissionType);
        }
    }
}