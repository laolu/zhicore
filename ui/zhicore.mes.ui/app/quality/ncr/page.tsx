"use client"

import { MainLayout } from "@/components/main-layout"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"

export default function QualityNCRPage() {
  return (
      <div className="p-6">
        <h1 className="text-2xl font-semibold mb-4">不合格处理</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 mb-6">
          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm font-medium">待处理</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="text-2xl font-bold">5</div>
              <p className="text-xs text-muted-foreground">件</p>
            </CardContent>
          </Card>
          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm font-medium">处理中</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="text-2xl font-bold">3</div>
              <p className="text-xs text-muted-foreground">件</p>
            </CardContent>
          </Card>
          <Card>
            <CardHeader className="pb-2">
              <CardTitle className="text-sm font-medium">已完成</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="text-2xl font-bold">12</div>
              <p className="text-xs text-muted-foreground">本周累计</p>
            </CardContent>
          </Card>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <Card>
            <CardHeader>
              <CardTitle>不合格品清单</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="rounded-lg border p-4">
                <div className="space-y-4">
                  <div className="text-sm text-gray-500">暂无数据</div>
                </div>
              </div>
            </CardContent>
          </Card>
          <Card>
            <CardHeader>
              <CardTitle>处理记录</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="rounded-lg border p-4">
                <div className="space-y-4">
                  <div className="text-sm text-gray-500">暂无数据</div>
                </div>
              </div>
            </CardContent>
          </Card>
        </div>
      </div>
  )
}