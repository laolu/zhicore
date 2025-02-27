"use client"

import { MainLayout } from "@/components/main-layout"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"

export default function QualitySPCPage() {
  return (
      <div className="p-6">
        <h1 className="text-2xl font-semibold mb-4">SPC控制</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-6">
          <Card>
            <CardHeader>
              <CardTitle>控制图</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-[300px] flex items-center justify-center border-2 border-dashed rounded-lg">
                控制图表区域
              </div>
            </CardContent>
          </Card>
          <Card>
            <CardHeader>
              <CardTitle>过程能力分析</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="h-[300px] flex items-center justify-center border-2 border-dashed rounded-lg">
                过程能力分析图表区域
              </div>
            </CardContent>
          </Card>
        </div>
        <Card>
          <CardHeader>
            <CardTitle>SPC数据记录</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="rounded-lg border p-4">
              <div className="space-y-4">
                <div>
                  <h3 className="text-lg font-medium mb-2">数据列表</h3>
                  <div className="text-sm text-gray-500">暂无数据</div>
                </div>
              </div>
            </div>
          </CardContent>
        </Card>
      </div>
  )
}