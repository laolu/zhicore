'use client'
import React from 'react';
import * as z from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Card } from "@/components/ui/card";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
// 工位表单验证Schema
const stationFormSchema = z.object({
    stationCode: z.string().min(2, {
        message: "工位编号至少需要2个字符",
    }),
    name: z.string().min(2, {
        message: "工位名称至少需要2个字符",
    }),
    productionLine: z.string().min(2, {
        message: "请选择所属产线",
    }),
    operator: z.string().min(2, {
        message: "操作人员至少需要2个字符",
    }),
    equipmentStatus: z.string(),
    productionStatus: z.string(),
    description: z.string(),
});
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
export default function Page() {
    const [isModalOpen, setIsModalOpen] = React.useState(false);
    const [isEditModalOpen, setIsEditModalOpen] = React.useState(false);
    const [isWorkshopModalOpen, setIsWorkshopModalOpen] = React.useState(false);
    const [isEditWorkshopModalOpen, setIsEditWorkshopModalOpen] = React.useState(false);
    const [isProductionLineModalOpen, setIsProductionLineModalOpen] = React.useState(false);
    const [isEditProductionLineModalOpen, setIsEditProductionLineModalOpen] = React.useState(false);
    const [isStationModalOpen, setIsStationModalOpen] = React.useState(false);
    const [isEditStationModalOpen, setIsEditStationModalOpen] = React.useState(false);
    const [editingFactory, setEditingFactory] = React.useState<any>(null);
    const [editingWorkshop, setEditingWorkshop] = React.useState<any>(null);
    const [editingProductionLine, setEditingProductionLine] = React.useState<any>(null);
    const [editingStation, setEditingStation] = React.useState<any>({
        stationCode: "",
        name: "",
        productionLine: "",
        operator: "",
        equipmentStatus: "normal",
        productionStatus: "producing",
        description: "",
    });
    // 工位表单
    const stationForm = useForm<z.infer<typeof stationFormSchema>>({
        resolver: zodResolver(stationFormSchema),
        defaultValues: {
            stationCode: "",
            name: "",
            productionLine: "",
            operator: "",
            equipmentStatus: "normal",
            productionStatus: "producing",
            description: "",
        },
    });
    const handleEdit = (factory: any) => {
        setEditingFactory(factory);
        form.reset({
            name: factory.name,
            location: factory.location,
            buildDate: factory.buildDate,
            manager: factory.manager,
            phone: factory.phone,
            status: factory.status,
            description: factory.description || "",
        });
        setIsEditModalOpen(true);
    };
    const onSubmit = (values: z.infer<typeof factoryFormSchema>) => {
        console.log(values);
        setIsEditModalOpen(false);
    };
    return (
            <div className="p-6">
                {/* 工厂概览 */}
                <div className="grid grid-cols-4 gap-4 mb-6">
                    <Card className="p-4">
                        <div className="flex items-center gap-4">
                            <div className="w-12 h-12 rounded-lg bg-blue-100 flex items-center justify-center">
                                <i className="fas fa-building text-blue-600 text-xl"></i>
                            </div>
                            <div>
                                <div className="text-sm text-gray-500">工厂总数</div>
                                <div className="text-2xl font-semibold">3</div>
                            </div>
                        </div>
                    </Card>
                    <Card className="p-4">
                        <div className="flex items-center gap-4">
                            <div className="w-12 h-12 rounded-lg bg-green-100 flex items-center justify-center">
                                <i className="fas fa-warehouse text-green-600 text-xl"></i>
                            </div>
                            <div>
                                <div className="text-sm text-gray-500">车间数量</div>
                                <div className="text-2xl font-semibold">12</div>
                            </div>
                        </div>
                    </Card>
                    <Card className="p-4">
                        <div className="flex items-center gap-4">
                            <div className="w-12 h-12 rounded-lg bg-purple-100 flex items-center justify-center">
                                <i className="fas fa-conveyor-belt text-purple-600 text-xl"></i>
                            </div>
                            <div>
                                <div className="text-sm text-gray-500">生产线</div>
                                <div className="text-2xl font-semibold">36</div>
                            </div>
                        </div>
                    </Card>
                    <Card className="p-4">
                        <div className="flex items-center gap-4">
                            <div className="w-12 h-12 rounded-lg bg-orange-100 flex items-center justify-center">
                                <i className="fas fa-tools text-orange-600 text-xl"></i>
                            </div>
                            <div>
                                <div className="text-sm text-gray-500">工位总数</div>
                                <div className="text-2xl font-semibold">248</div>
                            </div>
                        </div>
                    </Card>
                </div>
                {/* 主要管理内容 */}
                <Tabs defaultValue="factory" className="w-full">
                    <TabsList className="mb-4">
                        <TabsTrigger value="factory" className="!rounded-button whitespace-nowrap">工厂信息</TabsTrigger>
                        <TabsTrigger value="workshop" className="!rounded-button whitespace-nowrap">车间管理</TabsTrigger>
                        <TabsTrigger value="line" className="!rounded-button whitespace-nowrap">产线管理</TabsTrigger>
                        <TabsTrigger value="station" className="!rounded-button whitespace-nowrap">工位管理</TabsTrigger>
                    </TabsList>
                    <TabsContent value="factory">
                        <Card className="p-6">
                            <div className="flex justify-between items-center mb-4">
                                <div className="flex gap-4">
                                    <Input
                                        placeholder="搜索工厂名称"
                                        className="w-64 border-gray-200"
                                        prefix={<i className="fas fa-search text-gray-400"></i>}
                                    />
                                    <Button className="!rounded-button whitespace-nowrap">
                                        <i className="fas fa-filter mr-2"></i>
                                        筛选
                                    </Button>
                                </div>
                                <div className="flex gap-2">
                                    <Button className="!rounded-button whitespace-nowrap bg-blue-600" onClick={() => setIsModalOpen(true)}>
                                        <i className="fas fa-plus mr-2"></i>
                                        新增工厂
                                    </Button>
                                </div>
                            </div>
                            <ScrollArea className="h-[600px]">
                                <Table>
                                    <TableHeader>
                                        <TableRow>
                                            <TableHead>工厂名称</TableHead>
                                            <TableHead>所在地</TableHead>
                                            <TableHead>建成时间</TableHead>
                                            <TableHead>负责人</TableHead>
                                            <TableHead>联系电话</TableHead>
                                            <TableHead>状态</TableHead>
                                            <TableHead>操作</TableHead>
                                        </TableRow>
                                    </TableHeader>
                                    <TableBody>
                                        <TableRow>
                                            <TableCell>华东智能制造中心</TableCell>
                                            <TableCell>上海市浦东新区</TableCell>
                                            <TableCell>2020-06-15</TableCell>
                                            <TableCell>陈志远</TableCell>
                                            <TableCell>13812345678</TableCell>
                                            <TableCell>
                                                <span className="px-2 py-1 bg-green-100 text-green-600 rounded-full text-sm">运行中</span>
                                            </TableCell>
                                            <TableCell>
                                                <div className="flex gap-2">
                                                    <Button
                                                        variant="ghost"
                                                        size="sm"
                                                        className="!rounded-button whitespace-nowrap text-blue-600"
                                                        title="编辑"
                                                        onClick={() => {
                                                            setEditingStation({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            stationForm.reset({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            setIsEditStationModalOpen(true);
                                                        }}
                                                    >
                                                        <i className="fas fa-edit"></i>
                                                    </Button>
                                                    <Button variant="ghost" size="sm" className="!rounded-button whitespace-nowrap text-red-600" title="删除">
                                                        <i className="fas fa-trash"></i>
                                                    </Button>
                                                </div>
                                            </TableCell>
                                        </TableRow>
                                        <TableRow>
                                            <TableCell>华南生产基地</TableCell>
                                            <TableCell>广东省深圳市</TableCell>
                                            <TableCell>2019-03-28</TableCell>
                                            <TableCell>林明华</TableCell>
                                            <TableCell>13987654321</TableCell>
                                            <TableCell>
                                                <span className="px-2 py-1 bg-green-100 text-green-600 rounded-full text-sm">运行中</span>
                                            </TableCell>
                                            <TableCell>
                                                <div className="flex gap-2">
                                                    <Button
                                                        variant="ghost"
                                                        size="sm"
                                                        className="!rounded-button whitespace-nowrap text-blue-600"
                                                        title="编辑"
                                                        onClick={() => {
                                                            setEditingStation({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            stationForm.reset({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            setIsEditStationModalOpen(true);
                                                        }}
                                                    >
                                                        <i className="fas fa-edit"></i>
                                                    </Button>
                                                    <Button variant="ghost" size="sm" className="!rounded-button whitespace-nowrap text-red-600" title="删除">
                                                        <i className="fas fa-trash"></i>
                                                    </Button>
                                                </div>
                                            </TableCell>
                                        </TableRow>
                                        <TableRow>
                                            <TableCell>西部制造园区</TableCell>
                                            <TableCell>四川省成都市</TableCell>
                                            <TableCell>2021-09-01</TableCell>
                                            <TableCell>王建国</TableCell>
                                            <TableCell>13567891234</TableCell>
                                            <TableCell>
                                                <span className="px-2 py-1 bg-yellow-100 text-yellow-600 rounded-full text-sm">建设中</span>
                                            </TableCell>
                                            <TableCell>
                                                <div className="flex gap-2">
                                                    <Button
                                                        variant="ghost"
                                                        size="sm"
                                                        className="!rounded-button whitespace-nowrap text-blue-600"
                                                        title="编辑"
                                                        onClick={() => {
                                                            setEditingStation({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            stationForm.reset({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            setIsEditStationModalOpen(true);
                                                        }}
                                                    >
                                                        <i className="fas fa-edit"></i>
                                                    </Button>
                                                    <Button variant="ghost" size="sm" className="!rounded-button whitespace-nowrap text-red-600" title="删除">
                                                        <i className="fas fa-trash"></i>
                                                    </Button>
                                                </div>
                                            </TableCell>
                                        </TableRow>
                                    </TableBody>
                                </Table>
                            </ScrollArea>
                        </Card>
                    </TabsContent>
                    <TabsContent value="workshop">
                        <Card className="p-6">
                            <div className="flex justify-between items-center mb-4">
                                <div className="flex gap-4">
                                    <Input
                                        placeholder="搜索车间名称"
                                        className="w-64 border-gray-200"
                                        prefix={<i className="fas fa-search text-gray-400"></i>}
                                    />
                                    <Button className="!rounded-button whitespace-nowrap">
                                        <i className="fas fa-filter mr-2"></i>
                                        筛选
                                    </Button>
                                </div>
                                <Button className="!rounded-button whitespace-nowrap bg-blue-600" onClick={() => setIsWorkshopModalOpen(true)}>
                                    <i className="fas fa-plus mr-2"></i>
                                    新增车间
                                </Button>
                            </div>
                            <ScrollArea className="h-[600px]">
                                <Table>
                                    <TableHeader>
                                        <TableRow>
                                            <TableHead>车间名称</TableHead>
                                            <TableHead>所属工厂</TableHead>
                                            <TableHead>车间主管</TableHead>
                                            <TableHead>产线数量</TableHead>
                                            <TableHead>员工人数</TableHead>
                                            <TableHead>状态</TableHead>
                                            <TableHead>操作</TableHead>
                                        </TableRow>
                                    </TableHeader>
                                    <TableBody>
                                        <TableRow>
                                            <TableCell>总装车间A</TableCell>
                                            <TableCell>华东智能制造中心</TableCell>
                                            <TableCell>赵工</TableCell>
                                            <TableCell>6</TableCell>
                                            <TableCell>120</TableCell>
                                            <TableCell>
                                                <span className="px-2 py-1 bg-green-100 text-green-600 rounded-full text-sm">运行中</span>
                                            </TableCell>
                                            <TableCell>
                                                <div className="flex gap-2">
                                                    <Button
                                                        variant="ghost"
                                                        size="sm"
                                                        className="!rounded-button whitespace-nowrap text-blue-600"
                                                        title="编辑"
                                                        onClick={() => {
                                                            setEditingStation({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            stationForm.reset({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            setIsEditStationModalOpen(true);
                                                        }}
                                                    >
                                                        <i className="fas fa-edit"></i>
                                                    </Button>
                                                    <Button variant="ghost" size="sm" className="!rounded-button whitespace-nowrap text-red-600" title="删除">
                                                        <i className="fas fa-trash"></i>
                                                    </Button>
                                                </div>
                                            </TableCell>
                                        </TableRow>
                                        <TableRow>
                                            <TableCell>电子装配车间</TableCell>
                                            <TableCell>华南生产基地</TableCell>
                                            <TableCell>李工</TableCell>
                                            <TableCell>8</TableCell>
                                            <TableCell>160</TableCell>
                                            <TableCell>
                                                <span className="px-2 py-1 bg-green-100 text-green-600 rounded-full text-sm">运行中</span>
                                            </TableCell>
                                            <TableCell>
                                                <div className="flex gap-2">
                                                    <Button
                                                        variant="ghost"
                                                        size="sm"
                                                        className="!rounded-button whitespace-nowrap text-blue-600"
                                                        title="编辑"
                                                        onClick={() => {
                                                            setEditingStation({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            stationForm.reset({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            setIsEditStationModalOpen(true);
                                                        }}
                                                    >
                                                        <i className="fas fa-edit"></i>
                                                    </Button>
                                                    <Button variant="ghost" size="sm" className="!rounded-button whitespace-nowrap text-red-600" title="删除">
                                                        <i className="fas fa-trash"></i>
                                                    </Button>
                                                </div>
                                            </TableCell>
                                        </TableRow>
                                    </TableBody>
                                </Table>
                            </ScrollArea>
                        </Card>
                    </TabsContent>
                    <TabsContent value="line">
                        <Card className="p-6">
                            <div className="flex justify-between items-center mb-4">
                                <div className="flex gap-4">
                                    <Input
                                        placeholder="搜索产线名称"
                                        className="w-64 border-gray-200"
                                        prefix={<i className="fas fa-search text-gray-400"></i>}
                                    />
                                    <Button className="!rounded-button whitespace-nowrap">
                                        <i className="fas fa-filter mr-2"></i>
                                        筛选
                                    </Button>
                                </div>
                                <Button
                                    className="!rounded-button whitespace-nowrap bg-blue-600"
                                    onClick={() => setIsProductionLineModalOpen(true)}
                                >
                                    <i className="fas fa-plus mr-2"></i>
                                    新增产线
                                </Button>
                            </div>
                            <ScrollArea className="h-[600px]">
                                <Table>
                                    <TableHeader>
                                        <TableRow>
                                            <TableHead>产线编号</TableHead>
                                            <TableHead>产线名称</TableHead>
                                            <TableHead>所属车间</TableHead>
                                            <TableHead>工位数量</TableHead>
                                            <TableHead>生产能力</TableHead>
                                            <TableHead>状态</TableHead>
                                            <TableHead>操作</TableHead>
                                        </TableRow>
                                    </TableHeader>
                                    <TableBody>
                                        <TableRow>
                                            <TableCell>PL001</TableCell>
                                            <TableCell>手机组装线</TableCell>
                                            <TableCell>电子装配车间</TableCell>
                                            <TableCell>12</TableCell>
                                            <TableCell>1000台/天</TableCell>
                                            <TableCell>
                                                <span className="px-2 py-1 bg-green-100 text-green-600 rounded-full text-sm">运行中</span>
                                            </TableCell>
                                            <TableCell>
                                                <div className="flex gap-2">
                                                    <Button
                                                        variant="ghost"
                                                        size="sm"
                                                        className="!rounded-button whitespace-nowrap text-blue-600"
                                                        title="编辑"
                                                        onClick={() => {
                                                            setEditingStation({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            stationForm.reset({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            setIsEditStationModalOpen(true);
                                                        }}
                                                    >
                                                        <i className="fas fa-edit"></i>
                                                    </Button>
                                                    <Button variant="ghost" size="sm" className="!rounded-button whitespace-nowrap text-red-600" title="删除">
                                                        <i className="fas fa-trash"></i>
                                                    </Button>
                                                </div>
                                            </TableCell>
                                        </TableRow>
                                        <TableRow>
                                            <TableCell>PL002</TableCell>
                                            <TableCell>平板电脑生产线</TableCell>
                                            <TableCell>电子装配车间</TableCell>
                                            <TableCell>15</TableCell>
                                            <TableCell>500台/天</TableCell>
                                            <TableCell>
                                                <span className="px-2 py-1 bg-yellow-100 text-yellow-600 rounded-full text-sm">维护中</span>
                                            </TableCell>
                                            <TableCell>
                                                <div className="flex gap-2">
                                                    <Button
                                                        variant="ghost"
                                                        size="sm"
                                                        className="!rounded-button whitespace-nowrap text-blue-600"
                                                        title="编辑"
                                                        onClick={() => {
                                                            setEditingStation({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            stationForm.reset({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            setIsEditStationModalOpen(true);
                                                        }}
                                                    >
                                                        <i className="fas fa-edit"></i>
                                                    </Button>
                                                    <Button variant="ghost" size="sm" className="!rounded-button whitespace-nowrap text-red-600" title="删除">
                                                        <i className="fas fa-trash"></i>
                                                    </Button>
                                                </div>
                                            </TableCell>
                                        </TableRow>
                                    </TableBody>
                                </Table>
                            </ScrollArea>
                        </Card>
                    </TabsContent>
                    <TabsContent value="station">
                        <Card className="p-6">
                            <div className="flex justify-between items-center mb-4">
                                <div className="flex gap-4">
                                    <Input
                                        placeholder="搜索工位名称"
                                        className="w-64 border-gray-200"
                                        prefix={<i className="fas fa-search text-gray-400"></i>}
                                    />
                                    <Button className="!rounded-button whitespace-nowrap">
                                        <i className="fas fa-filter mr-2"></i>
                                        筛选
                                    </Button>
                                </div>
                                <Button
                                    className="!rounded-button whitespace-nowrap bg-blue-600"
                                    onClick={() => {
                                        stationForm.reset(); // 重置表单
                                        setIsStationModalOpen(true);
                                    }}
                                >
                                    <i className="fas fa-plus mr-2"></i>
                                    新增工位
                                </Button>
                            </div>
                            <ScrollArea className="h-[600px]">
                                <Table>
                                    <TableHeader>
                                        <TableRow>
                                            <TableHead>工位编号</TableHead>
                                            <TableHead>工位名称</TableHead>
                                            <TableHead>所属产线</TableHead>
                                            <TableHead>操作人员</TableHead>
                                            <TableHead>设备状态</TableHead>
                                            <TableHead>生产状态</TableHead>
                                            <TableHead>操作</TableHead>
                                        </TableRow>
                                    </TableHeader>
                                    <TableBody>
                                        <TableRow>
                                            <TableCell>WS001</TableCell>
                                            <TableCell>屏幕组装工位</TableCell>
                                            <TableCell>手机组装线</TableCell>
                                            <TableCell>周师傅</TableCell>
                                            <TableCell>
                                                <span className="px-2 py-1 bg-green-100 text-green-600 rounded-full text-sm">正常</span>
                                            </TableCell>
                                            <TableCell>
                                                <span className="px-2 py-1 bg-blue-100 text-blue-600 rounded-full text-sm">生产中</span>
                                            </TableCell>
                                            <TableCell>
                                                <div className="flex gap-2">
                                                    <Button
                                                        variant="ghost"
                                                        size="sm"
                                                        className="!rounded-button whitespace-nowrap text-blue-600"
                                                        title="编辑"
                                                        onClick={() => {
                                                            setEditingStation({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            stationForm.reset({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            setIsEditStationModalOpen(true);
                                                        }}
                                                    >
                                                        <i className="fas fa-edit"></i>
                                                    </Button>
                                                    <Button variant="ghost" size="sm" className="!rounded-button whitespace-nowrap text-red-600" title="删除">
                                                        <i className="fas fa-trash"></i>
                                                    </Button>
                                                </div>
                                            </TableCell>
                                        </TableRow>
                                        <TableRow>
                                            <TableCell>WS002</TableCell>
                                            <TableCell>电池安装工位</TableCell>
                                            <TableCell>手机组装线</TableCell>
                                            <TableCell>刘师傅</TableCell>
                                            <TableCell>
                                                <span className="px-2 py-1 bg-red-100 text-red-600 rounded-full text-sm">故障</span>
                                            </TableCell>
                                            <TableCell>
                                                <span className="px-2 py-1 bg-gray-100 text-gray-600 rounded-full text-sm">停止</span>
                                            </TableCell>
                                            <TableCell>
                                                <div className="flex gap-2">
                                                    <Button
                                                        variant="ghost"
                                                        size="sm"
                                                        className="!rounded-button whitespace-nowrap text-blue-600"
                                                        title="编辑"
                                                        onClick={() => {
                                                            setEditingStation({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            stationForm.reset({
                                                                stationCode: "WS001",
                                                                name: "屏幕组装工位",
                                                                productionLine: "手机组装线",
                                                                operator: "周师傅",
                                                                equipmentStatus: "normal",
                                                                productionStatus: "producing",
                                                                description: "负责手机屏幕组装工序"
                                                            });
                                                            setIsEditStationModalOpen(true);
                                                        }}
                                                    >
                                                        <i className="fas fa-edit"></i>
                                                    </Button>
                                                    <Button variant="ghost" size="sm" className="!rounded-button whitespace-nowrap text-red-600" title="删除">
                                                        <i className="fas fa-trash"></i>
                                                    </Button>
                                                </div>
                                            </TableCell>
                                        </TableRow>
                                    </TableBody>
                                </Table>
                            </ScrollArea>
                        </Card>
                    </TabsContent>
                </Tabs>
            </div>

    );
};
