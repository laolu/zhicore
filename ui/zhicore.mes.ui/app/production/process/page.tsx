"use client"

import { MainLayout } from "@/components/main-layout"
import Link from "next/link"

export default function ProcessPage() {
  return (
      <div className="p-6">
        <h1 className="text-2xl font-semibold mb-4">工艺管理</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          <Link href="/production/process/routing" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">工艺路线</h2>
              <p className="text-gray-500">工艺路线定义与管理</p>
            </div>
          </Link>
          <Link href="/production/process/operation" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">工序管理</h2>
              <p className="text-gray-500">生产工序定义与管理</p>
            </div>
          </Link>
          <Link href="/production/process/bom" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">BOM管理</h2>
              <p className="text-gray-500">产品物料清单管理</p>
            </div>
          </Link>
        </div>
      </div>
  )
}