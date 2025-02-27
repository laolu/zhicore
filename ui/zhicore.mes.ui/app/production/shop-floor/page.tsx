"use client"

import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { ScrollArea } from "@/components/ui/scroll-area"
import { LayoutDashboard, Monitor, Settings2, Factory } from "lucide-react"

export default function ProductionShopFloorPage() {
  const shopFloorModules = [
    {
      title: "车间看板",
      description: "实时监控车间生产状态和关键指标",
      icon: Monitor,
      href: "/production/shop-floor/dashboard"
    },
    {
      title: "工位管理",
      description: "管理车间工位配置和状态",
      icon: Settings2,
      href: "/production/shop-floor/workstation"
    },
    {
      title: "生产线管理",
      description: "配置和管理生产线信息",
      icon: Factory,
      href: "/production/shop-floor/production-line"
    },
    {
      title: "车间布局",
      description: "查看和编辑车间布局规划",
      icon: LayoutDashboard,
      href: "/production/shop-floor/layout"
    }
  ]

  return (
    <div className="container py-6 p-5">
      <div className="flex flex-col gap-4">
        <div>
          <h1 className="text-2xl font-semibold tracking-tight">生产车间</h1>
          <p className="text-sm text-muted-foreground">管理车间生产设施和监控生产状态</p>
        </div>
        <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
          {shopFloorModules.map((module) => (
            <Card key={module.title} className="hover:bg-muted/50">
              <a href={module.href}>
                <CardHeader>
                  <div className="flex items-center gap-2">
                    <module.icon className="h-5 w-5" />
                    <CardTitle className="text-base">{module.title}</CardTitle>
                  </div>
                </CardHeader>
                <CardContent>
                  <CardDescription>{module.description}</CardDescription>
                </CardContent>
              </a>
            </Card>
          ))}
        </div>
      </div>
    </div>
  )
}