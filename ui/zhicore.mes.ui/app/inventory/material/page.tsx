"use client"

import React, { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Badge } from "@/components/ui/badge";
import { Checkbox } from "@/components/ui/checkbox";
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";

export default function Page(): React.ReactElement {
  const [selectedItems, setSelectedItems] = useState<string[]>([]);
  const [searchQuery, setSearchQuery] = useState("");
  const [categoryFilter, setCategoryFilter] = useState("all");
  const [statusFilter, setStatusFilter] = useState("all");
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 5;

  const inventoryData = [
    {
      id: "WL001",
      code: "M2023112501",
      name: "高强度碳钢螺栓",
      spec: "M8×60mm",
      unit: "个",
      warehouse: "A2-012-01",
      currentStock: 2850,
      safetyStock: 3000,
      maxStock: 10000,
      status: "warning",
    },
    {
      id: "WL002",
      code: "M2023112502",
      name: "不锈钢法兰",
      spec: "DN50 PN16",
      unit: "件",
      warehouse: "A3-025-02",
      currentStock: 568,
      safetyStock: 300,
      maxStock: 1000,
      status: "normal",
    },
    {
      id: "WL003",
      code: "M2023112503",
      name: "铝合金型材",
      spec: "100×100×6000mm",
      unit: "根",
      warehouse: "B1-008-03",
      currentStock: 125,
      safetyStock: 100,
      maxStock: 500,
      status: "normal",
    },
    {
      id: "WL004",
      code: "M2023112504",
      name: "聚四氟乙烯密封圈",
      spec: "φ50×φ40×5mm",
      unit: "个",
      warehouse: "A1-015-04",
      currentStock: 1200,
      safetyStock: 2000,
      maxStock: 5000,
      status: "warning",
    },
    {
      id: "WL005",
      code: "M2023112505",
      name: "碳钢焊接弯头",
      spec: "DN80 90°",
      unit: "个",
      warehouse: "C2-032-05",
      currentStock: 320,
      safetyStock: 200,
      maxStock: 800,
      status: "normal",
    },
    {
      id: "WL006",
      code: "M2023112506",
      name: "液压油",
      spec: "46#",
      unit: "桶",
      warehouse: "D1-001-01",
      currentStock: 28,
      safetyStock: 20,
      maxStock: 100,
      status: "normal",
    },
    {
      id: "WL007",
      code: "M2023112507",
      name: "不锈钢管",
      spec: "DN25 SCH40",
      unit: "米",
      warehouse: "B2-015-03",
      currentStock: 180,
      safetyStock: 500,
      maxStock: 2000,
      status: "warning",
    },
    {
      id: "WL008",
      code: "M2023112508",
      name: "轴承",
      spec: "6205-2RS",
      unit: "个",
      warehouse: "A4-023-02",
      currentStock: 456,
      safetyStock: 200,
      maxStock: 1000,
      status: "normal",
    },
    {
      id: "WL009",
      code: "M2023112509",
      name: "电机",
      spec: "Y90L-4 2.2kW",
      unit: "台",
      warehouse: "C1-008-01",
      currentStock: 15,
      safetyStock: 10,
      maxStock: 50,
      status: "normal",
    },
    {
      id: "WL010",
      code: "M2023112510",
      name: "橡胶密封圈",
      spec: "φ30×φ20×5mm",
      unit: "个",
      warehouse: "A1-016-04",
      currentStock: 2100,
      safetyStock: 3000,
      maxStock: 8000,
      status: "warning",
    }
  ];
  const handleSelectAll = (checked: boolean) => {
    if (checked) {
      setSelectedItems(inventoryData.map(item => item.id));
    } else {
      setSelectedItems([]);
    }
  };
  const handleSelectItem = (id: string) => {
    if (selectedItems.includes(id)) {
      setSelectedItems(selectedItems.filter(item => item !== id));
    } else {
      setSelectedItems([...selectedItems, id]);
    }
  };

  const handleAddMaterial = () => {
    setIsAddDialogOpen(true);
  };

  // 过滤和搜索逻辑
  const filteredData = inventoryData.filter(item => {
    const matchesSearch = item.code.toLowerCase().includes(searchQuery.toLowerCase()) ||
      item.name.toLowerCase().includes(searchQuery.toLowerCase());
    const matchesCategory = categoryFilter === "all" ? true : categoryFilter === "raw" ? item.name.includes("原材料") :
      categoryFilter === "parts" ? item.name.includes("零部件") :
        categoryFilter === "consumables" ? item.name.includes("耗材") : false;
    const matchesStatus = statusFilter === "all" ? true : item.status === statusFilter;
    return matchesSearch && matchesCategory && matchesStatus;
  });

  // 分页逻辑
  const totalPages = Math.ceil(filteredData.length / itemsPerPage);
  const paginatedData = filteredData.slice(
    (currentPage - 1) * itemsPerPage,
    currentPage * itemsPerPage
  );

  // 处理页码变化
  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  const [isAddDialogOpen, setIsAddDialogOpen] = useState(false);
  const [isEditDialogOpen, setIsEditDialogOpen] = useState(false);
  const [isDeleteDialogOpen, setIsDeleteDialogOpen] = useState(false);
  const [currentItem, setCurrentItem] = useState<typeof inventoryData[0] | null>(null);

  return (
    <div className="w-full bg-white shadow-sm p-6">
      {/* 顶部操作区 */}
      <div className="flex items-center justify-between mb-6">
        <div className="flex items-center gap-4 flex-1">
          <div className="relative w-[320px]">
            <Input
              placeholder="搜索物料编码/名称"
              className="pl-10 text-sm"
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
            />
            <i className="fas fa-search absolute left-3 top-1/2 -translate-y-1/2 text-gray-400 text-sm"></i>
          </div>
          <Select value={categoryFilter} onValueChange={setCategoryFilter}>
            <SelectTrigger className="w-[160px]">
              <SelectValue placeholder="物料分类" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="all">全部分类</SelectItem>
              <SelectItem value="raw">原材料</SelectItem>
              <SelectItem value="parts">零部件</SelectItem>
              <SelectItem value="consumables">耗材</SelectItem>
            </SelectContent>
          </Select>
          <Select value={statusFilter} onValueChange={setStatusFilter}>
            <SelectTrigger className="w-[160px]">
              <SelectValue placeholder="库存状态" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="all">全部状态</SelectItem>
              <SelectItem value="normal">库存正常</SelectItem>
              <SelectItem value="warning">库存不足</SelectItem>
            </SelectContent>
          </Select>
        </div>
        <div className="flex items-center gap-3">
          <Button variant="outline" className="!rounded-button whitespace-nowrap">
            <i className="fas fa-file-import mr-2"></i>
            导入
          </Button>
          <Button variant="outline" className="!rounded-button whitespace-nowrap">
            <i className="fas fa-file-export mr-2"></i>
            导出
          </Button>
          <Button
            className="!rounded-button whitespace-nowrap bg-blue-600"
            onClick={handleAddMaterial}
          >
            <i className="fas fa-plus mr-2"></i>
            新增物料
          </Button>
        </div>
      </div>
      {/* 表格区域 */}
      <div className="border rounded-lg">
        <Table>
          <TableHeader>
            <TableRow className="bg-gray-50">
              <TableHead className="w-[50px]">
                <Checkbox
                  checked={selectedItems.length === inventoryData.length}
                  onCheckedChange={(checked: boolean) => handleSelectAll(checked)}
                />
              </TableHead>
              <TableHead>物料编码</TableHead>
              <TableHead>物料名称</TableHead>
              <TableHead>规格型号</TableHead>
              <TableHead>单位</TableHead>
              <TableHead>仓位</TableHead>
              <TableHead className="text-right">当前库存</TableHead>
              <TableHead className="text-right">安全库存</TableHead>
              <TableHead className="text-right">最大库存</TableHead>
              <TableHead className="text-right">状态</TableHead>
              <TableHead className="text-right">操作</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {paginatedData.map((item) => (
              <TableRow key={item.id}>
                <TableCell>
                  <Checkbox
                    checked={selectedItems.includes(item.id)}
                    onCheckedChange={() => handleSelectItem(item.id)}
                  />
                </TableCell>
                <TableCell>{item.code}</TableCell>
                <TableCell>{item.name}</TableCell>
                <TableCell>{item.spec}</TableCell>
                <TableCell>{item.unit}</TableCell>
                <TableCell>{item.warehouse}</TableCell>
                <TableCell className="text-right">
                  <span className={item.status === "warning" ? "text-red-500 font-medium" : ""}>
                    {item.currentStock}
                  </span>
                </TableCell>
                <TableCell className="text-right">{item.safetyStock}</TableCell>
                <TableCell className="text-right">{item.maxStock}</TableCell>
                <TableCell className="text-right">
                  <Badge variant={item.status === "warning" ? "destructive" : "secondary"}>
                    {item.status === "warning" ? "库存不足" : "正常"}
                  </Badge>
                </TableCell>
                <TableCell>
                  <div className="flex items-center justify-end gap-2">
                    <Button
                      variant="ghost"
                      size="icon"
                      className="h-8 w-8 hover:bg-gray-100 hover:text-blue-600 transition-colors"
                      onClick={() => {
                        setCurrentItem(item);
                        setIsEditDialogOpen(true);
                      }}
                    >
                      <i className="fas fa-edit text-gray-500 hover:text-blue-600"></i>
                    </Button>
                    <Button
                      variant="ghost"
                      size="icon"
                      className="h-8 w-8 hover:bg-red-50 hover:text-red-600 transition-colors"
                      onClick={() => {
                        setCurrentItem(item);
                        setIsDeleteDialogOpen(true);
                      }}
                    >
                      <i className="fas fa-trash text-gray-500 hover:text-red-600"></i>
                    </Button>
                  </div>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>
      {/* 分页 */}
      <div className="flex items-center justify-between mt-4 px-4 py-2 space-x-4">
        <div className="text-sm text-gray-500 flex-shrink-0">
          共 {filteredData.length} 条数据
        </div>
        <Pagination className="flex-grow flex justify-end">
          <PaginationContent>
            <PaginationItem>
              <PaginationPrevious
                href="#"
                onClick={() => handlePageChange(currentPage - 1)}
                className={currentPage === 1 ? "pointer-events-none opacity-50" : ""}
              />
            </PaginationItem>
            {Array.from({ length: totalPages }, (_, i) => i + 1).map((page) => (
              <PaginationItem key={page}>
                <PaginationLink
                  href="#"
                  isActive={currentPage === page}
                  onClick={() => handlePageChange(page)}
                >
                  {page}
                </PaginationLink>
              </PaginationItem>
            ))}
            <PaginationItem>
              <PaginationNext
                href="#"
                onClick={() => handlePageChange(currentPage + 1)}
                className={currentPage === totalPages ? "pointer-events-none opacity-50" : ""}
              />
            </PaginationItem>
          </PaginationContent>
        </Pagination>
      </div>
    </div>
  );
};
