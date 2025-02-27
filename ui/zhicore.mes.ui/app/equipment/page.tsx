"use client"

import { MainLayout } from "@/components/main-layout"
import Link from "next/link"

export default function EquipmentPage() {
  return (

      <div className="p-6">
        <h1 className="text-2xl font-semibold mb-4">设备管理</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          <Link href="/equipment/list" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">设备台账</h2>
              <p className="text-gray-500">设备基础信息管理</p>
            </div>
          </Link>
          <Link href="/equipment/maintenance" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">点检保养</h2>
              <p className="text-gray-500">设备日常点检与保养</p>
            </div>
          </Link>
          <Link href="/equipment/repair" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">故障维修</h2>
              <p className="text-gray-500">设备故障处理与维修</p>
            </div>
          </Link>
          <Link href="/equipment/trace" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">设备追溯</h2>
              <p className="text-gray-500">设备全生命周期追溯</p>
            </div>
          </Link>
        </div>
      </div>
  )
}