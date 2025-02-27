"use client"

import { MainLayout } from "@/components/main-layout"
import Link from "next/link"

export default function ProductionPage() {
  return (
      <div className="p-6">
        <h1 className="text-2xl font-semibold mb-4">生产管理</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          <Link href="/production/plan" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">生产计划</h2>
              <p className="text-gray-500">生产计划制定与管理</p>
            </div>
          </Link>
          <Link href="/production/process" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">工艺管理</h2>
              <p className="text-gray-500">工艺路线、工序和BOM管理</p>
            </div>
          </Link>
          <Link href="/production/execution" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">生产执行</h2>
              <p className="text-gray-500">生产订单和工单管理</p>
            </div>
          </Link>
          <Link href="/production/shop-floor" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">现场管理</h2>
              <p className="text-gray-500">工位管理和人员排班</p>
            </div>
          </Link>
          <Link href="/production/trace" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">生产追溯</h2>
              <p className="text-gray-500">生产全过程追溯</p>
            </div>
          </Link>
        </div>
      </div>
  )
}