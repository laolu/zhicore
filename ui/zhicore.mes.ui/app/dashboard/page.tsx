"use client"

import { MainLayout } from "@/components/main-layout"

export default function DashboardPage() {
  return (
      <div className="p-6">
        <h1 className="text-2xl font-semibold mb-4">系统总览</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
          <div className="p-4 border rounded-lg shadow-sm">
            <h2 className="text-lg font-medium mb-2">生产看板</h2>
            <p className="text-gray-500">实时生产数据监控</p>
          </div>
          <div className="p-4 border rounded-lg shadow-sm">
            <h2 className="text-lg font-medium mb-2">质量看板</h2>
            <p className="text-gray-500">质量指标实时监控</p>
          </div>
          <div className="p-4 border rounded-lg shadow-sm">
            <h2 className="text-lg font-medium mb-2">设备状态</h2>
            <p className="text-gray-500">设备运行状态监控</p>
          </div>
          <div className="p-4 border rounded-lg shadow-sm">
            <h2 className="text-lg font-medium mb-2">异常监控</h2>
            <p className="text-gray-500">系统异常实时监控</p>
          </div>
        </div>
      </div>
  )
}