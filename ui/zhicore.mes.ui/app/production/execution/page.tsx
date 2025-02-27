"use client"

import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { ScrollArea } from "@/components/ui/scroll-area"
import { ClipboardList, FileSpreadsheet, ListChecks, Timer } from "lucide-react"

export default function ProductionExecutionPage() {
  const executionModules = [
    {
      title: "生产订单执行",
      description: "查看和管理生产订单的执行状态",
      icon: ClipboardList,
      href: "/production/execution/order"
    },
    {
      title: "工序报工",
      description: "记录和管理生产工序的完成情况",
      icon: FileSpreadsheet,
      href: "/production/execution/report"
    },
    {
      title: "生产排程",
      description: "管理生产订单的排程和进度",
      icon: Timer,
      href: "/production/execution/scheduling"
    },
    {
      title: "工单管理",
      description: "创建和管理生产工单",
      icon: ListChecks,
      href: "/production/execution/work-order"
    }
  ]

  return (
    <div className="container py-6 p-5">
      <div className="flex flex-col gap-4">
        <div>
          <h1 className="text-2xl font-semibold tracking-tight">生产执行</h1>
          <p className="text-sm text-muted-foreground">管理和跟踪生产订单的执行过程</p>
        </div>
        <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
          {executionModules.map((module) => (
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