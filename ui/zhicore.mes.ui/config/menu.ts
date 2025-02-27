import {
  BookOpen,
  Bot,
  Box,
  Building2,
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
  Users,
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
      isActive: true,
      items: [
        {
          title: "数据看板",
          url: "/dashboard/overview",
        },
        {
          title: "生产监控",
          url: "/dashboard/monitor",
        },
        {
          title: "设备状态",
          url: "/dashboard/equipment",
        },
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
          title: "生产订单",
          url: "/production/order",
        },
        {
          title: "工艺路线",
          url: "/production/routing",
        },
        {
          title: "生产报工",
          url: "/production/report",
        },
        {
          title: "生产追溯",
          url: "/production/trace",
        },
      ],
    },
    {
      title: "物料管理",
      url: "/inventory",
      icon: Box,
      items: [
        {
          title: "物料主数据",
          url: "/inventory/material",
        },
        {
          title: "库存查询",
          url: "/inventory/stock",
        },
        {
          title: "入库管理",
          url: "/inventory/inbound",
        },
        {
          title: "出库管理",
          url: "/inventory/outbound",
        },
        {
          title: "库存盘点",
          url: "/inventory/check",
        },
      ],
    },
    {
      title: "质量管理",
      url: "/quality",
      icon: ClipboardList,
      items: [
        {
          title: "质检标准",
          url: "/quality/standard",
        },
        {
          title: "来料检验",
          url: "/quality/incoming",
        },
        {
          title: "过程检验",
          url: "/quality/process",
        },
        {
          title: "成品检验",
          url: "/quality/final",
        },
        {
          title: "不合格处理",
          url: "/quality/ncr",
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
          title: "设备维护",
          url: "/equipment/maintenance",
        },
        {
          title: "维修管理",
          url: "/equipment/repair",
        },
        {
          title: "备件管理",
          url: "/equipment/spare-parts",
        },
      ],
    },
    {
      title: "仓储管理",
      url: "/warehouse",
      icon: Warehouse,
      items: [
        {
          title: "仓库设置",
          url: "/warehouse/setup",
        },
        {
          title: "库位管理",
          url: "/warehouse/location",
        },
        {
          title: "库存移动",
          url: "/warehouse/transfer",
        },
        {
          title: "库存预警",
          url: "/warehouse/alert",
        },
      ],
    },
    {
      title: "报表中心",
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
          title: "库存报表",
          url: "/reports/inventory",
        },
      ],
    },
    {
      title: "基础数据",
      url: "/master-data",
      icon: Building2,
      items: [
        {
          title: "工厂建模",
          url: "/master-data/factory",
        },
        {
          title: "工艺管理",
          url: "/master-data/process",
          items: [
            {
              title: "工艺路线",
              url: "/master-data/process/routing",
            },
            {
              title: "工序管理",
              url: "/master-data/process/operation",
            },
            {
              title: "工艺参数",
              url: "/master-data/process/parameter",
            }
          ]
        },
        {
          title: "物料管理",
          url: "/master-data/material",
          items: [
            {
              title: "物料主数据",
              url: "/master-data/material/info",
            },
            {
              title: "BOM管理",
              url: "/master-data/material/bom",
            },
            {
              title: "物料分类",
              url: "/master-data/material/category",
            }
          ]
        },
        {
          title: "人员管理",
          url: "/master-data/personnel",
          items: [
            {
              title: "员工信息",
              url: "/master-data/personnel/employee",
            },
            {
              title: "班组管理",
              url: "/master-data/personnel/team",
            },
            {
              title: "技能管理",
              url: "/master-data/personnel/skill",
            }
          ]
        }
      ],
    },
    {
      title: "系统管理",
      url: "/system",
      icon: Settings,
      items: [
        {
          title: "用户管理",
          url: "/system/users",
        },
        {
          title: "角色权限",
          url: "/system/roles",
        },
        {
          title: "组织架构",
          url: "/system/organization",
        },
        {
          title: "系统参数",
          url: "/system/parameters",
        },
        {
          title: "数据字典",
          url: "/system/dictionary",
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