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

const workshopFormSchema = z.object({
    name: z.string().min(2, {
        message: "车间名称至少需要2个字符",
    }),
    factory: z.string().min(2, {
        message: "请选择所属工厂",
    }),
    supervisor: z.string().min(2, {
        message: "主管名称至少需要2个字符",
    }),
    employees: z.string().min(1, {
        message: "请输入员工人数",
    }),
    status: z.string(),
    description: z.string(),
});

type WorkshopFormValues = z.infer<typeof workshopFormSchema>;

interface EditWorkshopFormProps {
    isOpen: boolean;
    onClose: () => void;
    onSubmit: (values: WorkshopFormValues) => void;
    initialData?: WorkshopFormValues;
}

export function EditWorkshopForm({ isOpen, onClose, onSubmit, initialData }: EditWorkshopFormProps) {
    const form = useForm<WorkshopFormValues>({
        resolver: zodResolver(workshopFormSchema),
        defaultValues: initialData || {
            name: "",
            factory: "",
            supervisor: "",
            employees: "",
            status: "running",
            description: "",
        },
    });

    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
            <Card className="w-[600px] p-6">
                <div className="flex justify-between items-center mb-6">
                    <h3 className="text-lg font-semibold">{initialData ? '编辑车间' : '新增车间'}</h3>
                    <Button variant="ghost" size="sm" className="!rounded-button" onClick={onClose}>
                        <i className="fas fa-times"></i>
                    </Button>
                </div>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
                        <div className="grid grid-cols-2 gap-4">
                            <FormField
                                control={form.control}
                                name="name"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>车间名称</FormLabel>
                                        <FormControl>
                                            <Input placeholder="请输入车间名称" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="factory"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>所属工厂</FormLabel>
                                        <FormControl>
                                            <select
                                                className="w-full h-10 px-3 rounded-md border border-gray-200 focus:outline-none focus:ring-2 focus:ring-blue-500"
                                                {...field}
                                            >
                                                <option value="">请选择工厂</option>
                                                <option value="华东智能制造中心">华东智能制造中心</option>
                                                <option value="华南生产基地">华南生产基地</option>
                                                <option value="西部制造园区">西部制造园区</option>
                                            </select>
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="supervisor"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>车间主管</FormLabel>
                                        <FormControl>
                                            <Input placeholder="请输入车间主管姓名" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="employees"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>员工人数</FormLabel>
                                        <FormControl>
                                            <Input type="number" placeholder="请输入员工人数" {...field} />
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
                                        <FormLabel>车间描述</FormLabel>
                                        <FormControl>
                                            <textarea
                                                className="w-full h-24 p-3 rounded-md border border-gray-200 focus:outline-none focus:ring-2 focus:ring-blue-500"
                                                placeholder="请输入车间描述"
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