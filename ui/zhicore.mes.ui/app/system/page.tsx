"use client"

import { MainLayout } from "@/components/main-layout"
import Link from "next/link"

export default function SystemPage() {
  return (
      <div className="p-6">
        <h1 className="text-2xl font-semibold mb-4">系统管理</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          <Link href="/system/auth" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">用户权限</h2>
              <p className="text-gray-500">用户、角色和组织架构管理</p>
            </div>
          </Link>
          <Link href="/system/settings" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">系统配置</h2>
              <p className="text-gray-500">基础参数和数据字典配置</p>
            </div>
          </Link>
        </div>
      </div>
  )
}