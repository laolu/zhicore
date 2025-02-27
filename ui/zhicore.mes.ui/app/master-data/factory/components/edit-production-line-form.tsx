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

// 产线表单验证Schema
const productionLineFormSchema = z.object({
    lineCode: z.string().min(2, {
        message: "产线编号至少需要2个字符",
    }),
    name: z.string().min(2, {
        message: "产线名称至少需要2个字符",
    }),
    workshop: z.string().min(2, {
        message: "请选择所属车间",
    }),
    stations: z.string().min(1, {
        message: "请输入工位数量",
    }),
    capacity: z.string().min(2, {
        message: "请输入生产能力",
    }),
    status: z.string(),
    description: z.string(),
});

type ProductionLineFormValues = z.infer<typeof productionLineFormSchema>;

interface EditProductionLineFormProps {
    isOpen: boolean;
    onClose: () => void;
    onSubmit: (values: ProductionLineFormValues) => void;
    initialData?: ProductionLineFormValues;
}

export function EditProductionLineForm({ isOpen, onClose, onSubmit, initialData }: EditProductionLineFormProps) {
    const form = useForm<ProductionLineFormValues>({
        resolver: zodResolver(productionLineFormSchema),
        defaultValues: initialData || {
            lineCode: "",
            name: "",
            workshop: "",
            stations: "",
            capacity: "",
            status: "running",
            description: "",
        },
    });

    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
            <Card className="w-[600px] p-6">
                <div className="flex justify-between items-center mb-6">
                    <h3 className="text-lg font-semibold">{initialData ? '编辑产线' : '新增产线'}</h3>
                    <Button variant="ghost" size="sm" className="!rounded-button" onClick={onClose}>
                        <i className="fas fa-times"></i>
                    </Button>
                </div>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
                        <div className="grid grid-cols-2 gap-4">
                            <FormField
                                control={form.control}
                                name="lineCode"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>产线编号</FormLabel>
                                        <FormControl>
                                            <Input placeholder="请输入产线编号" {...field} />
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
                                        <FormLabel>产线名称</FormLabel>
                                        <FormControl>
                                            <Input placeholder="请输入产线名称" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="workshop"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>所属车间</FormLabel>
                                        <FormControl>
                                            <select
                                                className="w-full h-10 px-3 rounded-md border border-gray-200 focus:outline-none focus:ring-2 focus:ring-blue-500"
                                                {...field}
                                            >
                                                <option value="">请选择车间</option>
                                                <option value="总装车间">总装车间</option>
                                                <option value="电子车间">电子车间</option>
                                                <option value="机加工车间">机加工车间</option>
                                            </select>
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="stations"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>工位数量</FormLabel>
                                        <FormControl>
                                            <Input type="number" placeholder="请输入工位数量" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="capacity"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>生产能力</FormLabel>
                                        <FormControl>
                                            <Input placeholder="请输入生产能力" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="status"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>状态</FormLabel>
                                        <FormControl>
                                            <select
                                                className="w-full h-10 px-3 rounded-md border border-gray-200 focus:outline-none focus:ring-2 focus:ring-blue-500"
                                                {...field}
                                            >
                                                <option value="running">运行中</option>
                                                <option value="maintenance">维护中</option>
                                                <option value="stop">停用</option>
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
                                        <FormLabel>产线描述</FormLabel>
                                        <FormControl>
                                            <textarea
                                                className="w-full h-24 p-3 rounded-md border border-gray-200 focus:outline-none focus:ring-2 focus:ring-blue-500"
                                                placeholder="请输入产线描述"
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