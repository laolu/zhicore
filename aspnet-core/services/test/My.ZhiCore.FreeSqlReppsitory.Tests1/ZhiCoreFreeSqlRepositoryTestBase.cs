using My.ZhiCore.FreeSqlReppsitory.Tests;
using My.ZhiCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.ZhiCore
{
    public abstract class ZhiCoreFreeSqlRepositoryTestBase: ZhiCoreTestBase<ZhiCoreFreeSqlRepositoryTestModule>
    {
        public ZhiCoreFreeSqlRepositoryTestBase()
        {
            ServiceProvider.InitializeLocalization();
        }
    }
}
