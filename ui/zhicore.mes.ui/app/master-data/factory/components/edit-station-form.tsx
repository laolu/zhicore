"use client"

import React from 'react';
import * as z from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Card } from "@/components/ui/card";

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

type StationFormValues = z.infer<typeof stationFormSchema>;

interface EditStationFormProps {
    isOpen: boolean;
    onClose: () => void;
    onSubmit: (values: StationFormValues) => void;
    initialData?: StationFormValues;
}

export function EditStationForm({ isOpen, onClose, onSubmit, initialData }: EditStationFormProps) {
    const form = useForm<StationFormValues>({
        resolver: zodResolver(stationFormSchema),
        defaultValues: initialData || {
            stationCode: "",
            name: "",
            productionLine: "",
            operator: "",
            equipmentStatus: "normal",
            productionStatus: "producing",
            description: "",
        },
    });

    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
            <Card className="w-[600px] p-6">
                <div className="flex justify-between items-center mb-6">
                    <h3 className="text-lg font-semibold">{initialData ? '编辑工位' : '新增工位'}</h3>
                    <Button variant="ghost" size="sm" className="!rounded-button" onClick={onClose}>
                        <i className="fas fa-times"></i>
                    </Button>
                </div>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
                        <div className="grid grid-cols-2 gap-4">
                            <FormField
                                control={form.control}
                                name="stationCode"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>工位编号</FormLabel>
                                        <FormControl>
                                            <Input placeholder="请输入工位编号" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="name"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>工位名称</FormLabel>
                                        <FormControl>
                                            <Input placeholder="请输入工位名称" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="productionLine"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>所属产线</FormLabel>
                                        <FormControl>
                                            <select
                                                className="w-full h-10 px-3 rounded-md border border-gray-200 focus:outline-none focus:ring-2 focus:ring-blue-500"
                                                {...field}
                                            >
                                                <option value="">请选择产线</option>
                                                <option value="手机组装线">手机组装线</option>
                                                <option value="平板组装线">平板组装线</option>
                                                <option value="笔记本组装线">笔记本组装线</option>
                                            </select>
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="operator"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>操作人员</FormLabel>
                                        <FormControl>
                                            <Input placeholder="请输入操作人员姓名" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="equipmentStatus"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>设备状态</FormLabel>
                                        <FormControl>
                                            <select
                                                className="w-full h-10 px-3 rounded-md border border-gray-200 focus:outline-none focus:ring-2 focus:ring-blue-500"
                                                {...field}
                                            >
                                                <option value="normal">正常</option>
                                                <option value="maintenance">维护中</option>
                                                <option value="fault">故障</option>
                                            </select>
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="productionStatus"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>生产状态</FormLabel>
                                        <FormControl>
                                            <select
                                                className="w-full h-10 px-3 rounded-md border border-gray-200 focus:outline-none focus:ring-2 focus:ring-blue-500"
                                                {...field}
                                            >
                                                <option value="producing">生产中</option>
                                                <option value="idle">空闲</option>
                                                <option value="stopped">停工</option>
                                            </select>
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="description"
                                render={({ field }) => (
                                    <FormItem className="col-span-2">
                                        <FormLabel>工位描述</FormLabel>
                                        <FormControl>
                                            <textarea
                                                className="w-full h-24 p-3 rounded-md border border-gray-200 focus:outline-none focus:ring-2 focus:ring-blue-500"
                                                placeholder="请输入工位描述"
                                                {...field}
                                            />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                        </div>
                        <div className="flex justify-end gap-4 mt-6">
                            <Button variant="outline" className="!rounded-button" onClick={onClose}>取消</Button>
                            <Button type="submit" className="!rounded-button bg-blue-600">确定</Button>
                        </div>
                    </form>
                </Form>
            </Card>
        </div>
    );
}