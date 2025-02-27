"use client"

import { MainLayout } from "@/components/main-layout"
import Link from "next/link"

export default function QualityPage() {
  return (
      <div className="p-6">
        <h1 className="text-2xl font-semibold mb-4">质量管理</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          <Link href="/quality/standard" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">质量标准</h2>
              <p className="text-gray-500">质量标准制定与管理</p>
            </div>
          </Link>
          <Link href="/quality/inspection" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">质量检验</h2>
              <p className="text-gray-500">首检、过程检验和完工检验</p>
            </div>
          </Link>
          <Link href="/quality/spc" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">SPC控制</h2>
              <p className="text-gray-500">统计过程控制管理</p>
            </div>
          </Link>
          <Link href="/quality/measuring" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">计量管理</h2>
              <p className="text-gray-500">计量设备与标准管理</p>
            </div>
          </Link>
          <Link href="/quality/ncr" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">不合格处理</h2>
              <p className="text-gray-500">不合格品管理与处置</p>
            </div>
          </Link>
          <Link href="/quality/trace" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">质量追溯</h2>
              <p className="text-gray-500">质量数据全程追溯</p>
            </div>
          </Link>
        </div>
      </div>
  )
}