"use client"

import { MainLayout } from "@/components/main-layout"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { Search } from "lucide-react"

export default function EquipmentTracePage() {
  return (
      <div className="p-6">
        <Card>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-4">
            <CardTitle>设备追溯</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="flex items-center space-x-2 mb-4">
              <div className="flex-1">
                <div className="relative">
                  <Search className="absolute left-2 top-2.5 h-4 w-4 text-muted-foreground" />
                  <Input placeholder="输入设备编号或名称搜索..." className="pl-8" />
                </div>
              </div>
              <Button variant="outline">筛选</Button>
            </div>
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>设备编号</TableHead>
                  <TableHead>设备名称</TableHead>
                  <TableHead>操作类型</TableHead>
                  <TableHead>操作内容</TableHead>
                  <TableHead>操作人</TableHead>
                  <TableHead>操作时间</TableHead>
                  <TableHead>操作</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                <TableRow>
                  <TableCell>EQ001</TableCell>
                  <TableCell>数控车床</TableCell>
                  <TableCell>点检</TableCell>
                  <TableCell>日常点检</TableCell>
                  <TableCell>张工</TableCell>
                  <TableCell>2024-01-20 09:00:00</TableCell>
                  <TableCell>
                    <Button variant="link" size="sm">详情</Button>
                  </TableCell>
                </TableRow>
                <TableRow>
                  <TableCell>EQ001</TableCell>
                  <TableCell>数控车床</TableCell>
                  <TableCell>维修</TableCell>
                  <TableCell>主轴维修</TableCell>
                  <TableCell>李工</TableCell>
                  <TableCell>2024-01-19 14:30:00</TableCell>
                  <TableCell>
                    <Button variant="link" size="sm">详情</Button>
                  </TableCell>
                </TableRow>
              </TableBody>
            </Table>
          </CardContent>
        </Card>
      </div>
  )
}