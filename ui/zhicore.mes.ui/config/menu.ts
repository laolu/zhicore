import {
  BookOpen,
  Bot,
  Box,
  ClipboardList,
  Command,
  Factory,
  FileSpreadsheet,
  Frame,
  LayoutDashboard,
  LifeBuoy,
  Map,
  PieChart,
  Send,
  Settings,
  Settings2,
  SquareTerminal,
  Warehouse,
  Wrench,
} from "lucide-react"

export const menuConfig = {
  user: {
    name: "shadcn",
    email: "m@example.com",
    avatar: "/avatars/shadcn.jpg",
  },
  navMain: [
    {
      title: "系统总览",
      url: "/dashboard",
      icon: LayoutDashboard,
      items: [
        {
          title: "生产看板",
          url: "/dashboard/production",
        },
        {
          title: "质量看板",
          url: "/dashboard/quality",
        },
        {
          title: "设备状态",
          url: "/dashboard/equipment",
        },
        {
          title: "异常监控",
          url: "/dashboard/exception",
        }
      ],
    },
    {
      title: "生产管理",
      url: "/production",
      icon: Factory,
      items: [
        {
          title: "生产计划",
          url: "/production/plan",
        },
        {
          title: "工艺管理",
          url: "/production/process",
          items: [
            {
              title: "工艺路线",
              url: "/production/process/routing",
            },
            {
              title: "工序管理",
              url: "/production/process/operation",
            },
            {
              title: "BOM管理",
              url: "/production/process/bom",
            }
          ]
        },
        {
          title: "生产执行",
          url: "/production/execution",
          items: [
            {
              title: "生产订单",
              url: "/production/execution/order",
            },
            {
              title: "工单管理",
              url: "/production/execution/work-order",
            },
            {
              title: "生产调度",
              url: "/production/execution/scheduling",
            },
            {
              title: "生产报工",
              url: "/production/execution/report",
            }
          ]
        },
        {
          title: "现场管理",
          url: "/production/shop-floor",
          items: [
            {
              title: "工位管理",
              url: "/production/shop-floor/station",
            },
            {
              title: "人员排班",
              url: "/production/shop-floor/scheduling",
            },
            {
              title: "异常处理",
              url: "/production/shop-floor/exception",
            }
          ]
        },
        {
          title: "生产追溯",
          url: "/production/trace",
        },
      ],
    },
    {
      title: "质量管理",
      url: "/quality",
      icon: ClipboardList,
      items: [
        {
          title: "质量标准",
          url: "/quality/standard",
        },
        {
          title: "质量检验",
          url: "/quality/inspection",
          items: [
            {
              title: "首检管理",
              url: "/quality/inspection/first",
            },
            {
              title: "过程检验",
              url: "/quality/inspection/process",
            },
            {
              title: "完工检验",
              url: "/quality/inspection/final",
            }
          ]
        },
        {
          title: "SPC控制",
          url: "/quality/spc",
        },
        {
          title: "计量管理",
          url: "/quality/measuring",
        },
        {
          title: "不合格处理",
          url: "/quality/ncr",
        },
        {
          title: "质量追溯",
          url: "/quality/trace",
        },
      ],
    },
    {
      title: "生产物料",
      url: "/production-material",
      icon: Box,
      items: [
        {
          title: "线边库管理",
          url: "/production-material/lineside",
        },
        {
          title: "上料管理",
          url: "/production-material/feeding",
        },
        {
          title: "物料配送",
          url: "/production-material/delivery",
        },
        {
          title: "物料消耗",
          url: "/production-material/consumption",
        },
        {
          title: "物料追溯",
          url: "/production-material/trace",
        },
      ],
    },
    {
      title: "设备管理",
      url: "/equipment",
      icon: Wrench,
      items: [
        {
          title: "设备台账",
          url: "/equipment/list",
        },
        {
          title: "点检保养",
          url: "/equipment/maintenance",
        },
        {
          title: "故障维修",
          url: "/equipment/repair",
        },
        {
          title: "设备追溯",
          url: "/equipment/trace",
        },
      ],
    },
    {
      title: "报表分析",
      url: "/reports",
      icon: FileSpreadsheet,
      items: [
        {
          title: "生产报表",
          url: "/reports/production",
        },
        {
          title: "质量报表",
          url: "/reports/quality",
        },
        {
          title: "设备报表",
          url: "/reports/equipment",
        },
        {
          title: "生产物料报表",
          url: "/reports/production-material",
        },
      ],
    },
    {
      title: "系统管理",
      url: "/system",
      icon: Settings,
      items: [
        {
          title: "用户权限",
          url: "/system/auth",
          items: [
            {
              title: "用户管理",
              url: "/system/auth/users",
            },
            {
              title: "角色权限",
              url: "/system/auth/roles",
            },
            {
              title: "组织架构",
              url: "/system/auth/organization",
            }
          ]
        },
        {
          title: "系统配置",
          url: "/system/settings",
          items: [
            {
              title: "基础参数",
              url: "/system/settings/parameters",
            },
            {
              title: "数据字典",
              url: "/system/settings/dictionary",
            },
            {
              title: "接口配置",
              url: "/system/settings/interface",
            }
          ]
        },
      ],
    },
  ],
  navSecondary: [
    {
      title: "Support",
      url: "#",
      icon: LifeBuoy,
    },
    {
      title: "Feedback",
      url: "#",
      icon: Send,
    },
  ],
  projects: [
    {
      name: "Design Engineering",
      url: "#",
      icon: Frame,
    },
    {
      name: "Sales & Marketing",
      url: "#",
      icon: PieChart,
    },
    {
      name: "Travel",
      url: "#",
      icon: Map,
    },
  ],
}