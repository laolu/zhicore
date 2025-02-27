"use client"

import { MainLayout } from "@/components/main-layout"
import Link from "next/link"

export default function InspectionPage() {
  return (
      <div className="p-6">
        <h1 className="text-2xl font-semibold mb-4">质量检验</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          <Link href="/quality/inspection/first" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">首检管理</h2>
              <p className="text-gray-500">生产首件产品质量检验</p>
            </div>
          </Link>
          <Link href="/quality/inspection/process" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">过程检验</h2>
              <p className="text-gray-500">生产过程质量检验</p>
            </div>
          </Link>
          <Link href="/quality/inspection/final" className="block">
            <div className="p-4 border rounded-lg shadow-sm hover:shadow-md transition-shadow">
              <h2 className="text-lg font-medium mb-2">完工检验</h2>
              <p className="text-gray-500">产品完工质量检验</p>
            </div>
          </Link>
        </div>
      </div>
  )
}