"use client"

import { MainLayout } from "@/components/main-layout"
import Link from "next/link"

export default function ProductionMaterialPage() {
  return (
      <div className="p-6">
        <h1 className="text-2xl font-semibold mb-4">生产物料</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          <Link href="/production-material/lineside" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">线边库管理</h2>
              <p className="text-gray-500">生产线边物料库存管理</p>
            </div>
          </Link>
          <Link href="/production-material/feeding" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">上料管理</h2>
              <p className="text-gray-500">生产线物料补给管理</p>
            </div>
          </Link>
          <Link href="/production-material/delivery" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">物料配送</h2>
              <p className="text-gray-500">物料配送计划与执行</p>
            </div>
          </Link>
          <Link href="/production-material/consumption" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">物料消耗</h2>
              <p className="text-gray-500">物料消耗统计与分析</p>
            </div>
          </Link>
          <Link href="/production-material/trace" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">物料追溯</h2>
              <p className="text-gray-500">物料使用全过程追溯</p>
            </div>
          </Link>
        </div>
      </div>
  )
}