"use client"

import { MainLayout } from "@/components/main-layout"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { Plus, Search } from "lucide-react"

export default function EquipmentRepairPage() {
  return (
      <div className="p-6">
        <Card>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-4">
            <CardTitle>设备故障维修</CardTitle>
            <Button>
              <Plus className="mr-2 h-4 w-4" />
              新建维修单
            </Button>
          </CardHeader>
          <CardContent>
            <div className="flex items-center space-x-2 mb-4">
              <div className="flex-1">
                <div className="relative">
                  <Search className="absolute left-2 top-2.5 h-4 w-4 text-muted-foreground" />
                  <Input placeholder="搜索维修工单..." className="pl-8" />
                </div>
              </div>
              <Button variant="outline">筛选</Button>
            </div>
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>工单编号</TableHead>
                  <TableHead>设备名称</TableHead>
                  <TableHead>故障类型</TableHead>
                  <TableHead>故障描述</TableHead>
                  <TableHead>维修人员</TableHead>
                  <TableHead>状态</TableHead>
                  <TableHead>操作</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                <TableRow>
                  <TableCell>RP001</TableCell>
                  <TableCell>数控车床</TableCell>
                  <TableCell>机械故障</TableCell>
                  <TableCell>主轴异响</TableCell>
                  <TableCell>李工</TableCell>
                  <TableCell>处理中</TableCell>
                  <TableCell>
                    <Button variant="link" size="sm">查看</Button>
                    <Button variant="link" size="sm">编辑</Button>
                  </TableCell>
                </TableRow>
              </TableBody>
            </Table>
          </CardContent>
        </Card>
      </div>
  )
}