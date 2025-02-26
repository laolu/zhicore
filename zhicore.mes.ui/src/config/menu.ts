import {
  AudioWaveform,
  BarChart2,
  Binary,
  BookOpen,
  Bot,
  Boxes,
  ClipboardList,
  Command,
  Database,
  Factory,
  Frame,
  GalleryVerticalEnd,
  Map,
  PieChart,
  Settings2,
  SquareTerminal,
  Workflow,
  Wrench,
} from "lucide-react"

export const menuConfig = {
  user: {
    name: "shadcn",
    email: "m@example.com",
    avatar: "/avatars/shadcn.jpg",
  },
  teams: [
    {
      name: "生产一部",
      logo: Factory,
      plan: "制造部",
    },
    {
      name: "生产二部",
      logo: Factory,
      plan: "制造部",
    },
    {
      name: "质检部",
      logo: ClipboardList,
      plan: "质量部",
    },
  ],
  navMain: [
    {
      title: "生产执行",
      url: "/production",
      icon: Factory,
      items: [
        {
          title: "工单管理",
          url: "/production/workorder",
        },
        {
          title: "生产计划",
          url: "/production/plan",
        },
        {
          title: "生产调度",
          url: "/production/scheduling",
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
      title: "质量管理",
      url: "/quality",
      icon: ClipboardList,
      items: [
        {
          title: "IQC检验",
          url: "/quality/iqc",
        },
        {
          title: "IPQC检验",
          url: "/quality/ipqc",
        },
        {
          title: "FQC检验",
          url: "/quality/fqc",
        },
        {
          title: "不合格品管理",
          url: "/quality/ncr",
        },
        {
          title: "质量追溯",
          url: "/quality/trace",
        },
      ],
    },
    {
      title: "设备管理",
      url: "/equipment",
      icon: Wrench,
      items: [
        {
          title: "设备档案",
          url: "/equipment/profile",
        },
        {
          title: "设备监控",
          url: "/equipment/monitor",
        },
        {
          title: "维修管理",
          url: "/equipment/maintenance",
        },
        {
          title: "点检保养",
          url: "/equipment/inspection",
        },
        {
          title: "备件管理",
          url: "/equipment/spare-parts",
        },
      ],
    },
    {
      title: "物料管理",
      url: "/material",
      icon: Boxes,
      items: [
        {
          title: "物料收发",
          url: "/material/transaction",
        },
        {
          title: "库存管理",
          url: "/material/inventory",
        },
        {
          title: "物料配送",
          url: "/material/delivery",
        },
        {
          title: "库存盘点",
          url: "/material/check",
        },
      ],
    },
    {
      title: "工艺管理",
      url: "/process",
      icon: Workflow,
      items: [
        {
          title: "工艺路线",
          url: "/process/routing",
        },
        {
          title: "工艺文件",
          url: "/process/document",
        },
        {
          title: "工艺参数",
          url: "/process/parameter",
        },
        {
          title: "作业指导",
          url: "/process/instruction",
        },
      ],
    },
    {
      title: "数据采集",
      url: "/datacollect",
      icon: Binary,
      items: [
        {
          title: "人工采集",
          url: "/datacollect/manual",
        },
        {
          title: "自动采集",
          url: "/datacollect/auto",
        },
        {
          title: "采集配置",
          url: "/datacollect/config",
        },
      ],
    },
    {
      title: "主数据",
      url: "/master",
      icon: Database,
      items: [
        {
          title: "物料主数据",
          url: "/master/material",
        },
        {
          title: "BOM管理",
          url: "/master/bom",
        },
        {
          title: "工序管理",
          url: "/master/operation",
        },
        {
          title: "工位管理",
          url: "/master/workstation",
        },
        {
          title: "人员管理",
          url: "/master/employee",
        },
      ],
    },
    {
      title: "报表中心",
      url: "/report",
      icon: BarChart2,
      items: [
        {
          title: "生产报表",
          url: "/report/production",
        },
        {
          title: "质量报表",
          url: "/report/quality",
        },
        {
          title: "设备报表",
          url: "/report/equipment",
        },
        {
          title: "物料报表",
          url: "/report/material",
        },
      ],
    },
    {
      title: "系统管理",
      url: "/system",
      icon: Settings2,
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
          title: "数据字典",
          url: "/system/dict",
        },
        {
          title: "参数设置",
          url: "/system/params",
        },
      ],
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