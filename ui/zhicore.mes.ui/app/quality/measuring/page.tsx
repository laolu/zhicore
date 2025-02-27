"use client"

import { MainLayout } from "@/components/main-layout"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"

export default function QualityMeasuringPage() {
  return (
      <div className="p-6">
        <h1 className="text-2xl font-semibold mb-4">计量管理</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-6">
          <Card>
            <CardHeader>
              <CardTitle>计量设备</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="rounded-lg border p-4">
                <div className="space-y-4">
                  <div>
                    <h3 className="text-lg font-medium mb-2">设备列表</h3>
                    <div className="text-sm text-gray-500">暂无数据</div>
                  </div>
                </div>
              </div>
            </CardContent>
          </Card>
          <Card>
            <CardHeader>
              <CardTitle>计量标准</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="rounded-lg border p-4">
                <div className="space-y-4">
                  <div>
                    <h3 className="text-lg font-medium mb-2">标准列表</h3>
                    <div className="text-sm text-gray-500">暂无数据</div>
                  </div>
                </div>
              </div>
            </CardContent>
          </Card>
        </div>
        <Card>
          <CardHeader>
            <CardTitle>校准记录</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="rounded-lg border p-4">
              <div className="space-y-4">
                <div>
                  <h3 className="text-lg font-medium mb-2">记录列表</h3>
                  <div className="text-sm text-gray-500">暂无数据</div>
                </div>
              </div>
            </div>
          </CardContent>
        </Card>
      </div>
  )
}