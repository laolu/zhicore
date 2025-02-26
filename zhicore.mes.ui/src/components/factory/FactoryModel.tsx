import React from "react";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { Card } from "@/components/ui/card";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogDescription, DialogFooter, DialogTrigger } from "@/components/ui/dialog";

const FactoryModel: React.FC = () => {
  return (
    <div className="p-6">
      <div className="flex justify-between items-center mb-6">
        <div>
          <h1 className="text-2xl font-bold">工厂建模</h1>
          <p className="text-gray-500 mt-1">管理工厂的组织结构和生产资源</p>
        </div>
        <div className="flex gap-3">
          <Dialog>
            <DialogTrigger asChild>
              <Button className="!rounded-button whitespace-nowrap">
                <i className="fas fa-plus mr-2"></i>
                新增模型
              </Button>
            </DialogTrigger>
            <DialogContent className="sm:max-w-[800px]">
              <DialogHeader>
                <DialogTitle>新增模型</DialogTitle>
                <DialogDescription>填写模型基本信息</DialogDescription>
              </DialogHeader>
              {/* 模型表单内容 */}
            </DialogContent>
          </Dialog>
        </div>
      </div>

      <Card className="mb-6">
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>模型编号</TableHead>
              <TableHead>模型名称</TableHead>
              <TableHead>类型</TableHead>
              <TableHead>上级模型</TableHead>
              <TableHead>负责人</TableHead>
              <TableHead>状态</TableHead>
              <TableHead>操作</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {[
              {
                id: "FM20231201",
                name: "总装车间",
                type: "车间",
                parent: "-",
                owner: "张经理",
                status: "正常"
              },
              {
                id: "FM20231202",
                name: "装配线-A",
                type: "生产线",
                parent: "总装车间",
                owner: "李主管",
                status: "正常"
              },
              {
                id: "FM20231203",
                name: "机加工线-B",
                type: "生产线",
                parent: "机加工车间",
                owner: "王主管",
                status: "正常"
              },
              {
                id: "FM20231204",
                name: "钳工工位-01",
                type: "工位",
                parent: "装配线-A",
                owner: "赵组长",
                status: "维护中"
              },
              {
                id: "FM20231205",
                name: "CNC加工中心",
                type: "设备",
                parent: "机加工线-B",
                owner: "刘工程师",
                status: "正常"
              }
            ].map((model) => (
              <TableRow key={model.id}>
                <TableCell className="font-medium">{model.id}</TableCell>
                <TableCell>{model.name}</TableCell>
                <TableCell>{model.type}</TableCell>
                <TableCell>{model.parent}</TableCell>
                <TableCell>{model.owner}</TableCell>
                <TableCell>
                  <Badge variant={model.status === "正常" ? "success" : "warning"}>
                    {model.status}
                  </Badge>
                </TableCell>
                <TableCell>
                  <Dialog>
                    <DialogTrigger asChild>
                      <Button variant="outline" size="sm" className="!rounded-button whitespace-nowrap">
                        详情
                      </Button>
                    </DialogTrigger>
                    <DialogContent className="sm:max-w-[800px]">
                      <DialogHeader>
                        <DialogTitle>模型详情</DialogTitle>
                        <DialogDescription>模型编号：{model.id}</DialogDescription>
                      </DialogHeader>
                      {/* 模型详情内容 */}
                    </DialogContent>
                  </Dialog>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </Card>

      <div className="space-y-6">
        <Card className="p-6">
          <h2 className="text-lg font-semibold mb-4">模型统计</h2>
          <div className="space-y-4">
            {[
              {
                type: "主工厂",
                count: 1,
                subModels: 5,
                devices: 25
              },
              {
                type: "装配车间",
                count: 2,
                subModels: 8,
                devices: 42
              },
              {
                type: "机加工车间",
                count: 2,
                subModels: 10,
                devices: 68
              },
              {
                type: "物流仓储",
                count: 1,
                subModels: 4,
                devices: 21
              }
            ].map((item, index) => (
              <div key={index} className="p-4 bg-gray-50 rounded-lg">
                <div className="flex justify-between items-center">
                  <div>
                    <h3 className="font-medium">{item.type}</h3>
                    <p className="text-sm text-gray-500 mt-1">
                      {item.subModels} 个子模型 · {item.devices} 台设备
                    </p>
                  </div>
                  <Badge variant="secondary">{item.count}个</Badge>
                </div>
              </div>
            ))}
          </div>
        </Card>
      </div>
    </div>
  );
};

export default FactoryModel;