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

const factoryFormSchema = z.object({
    name: z.string().min(2, {
        message: "工厂名称至少需要2个字符",
    }),
    location: z.string().min(2, {
        message: "所在地至少需要2个字符",
    }),
    buildDate: z.string(),
    manager: z.string().min(2, {
        message: "负责人名称至少需要2个字符",
    }),
    phone: z.string().min(11, {
        message: "请输入有效的手机号码",
    }),
    status: z.string(),
    description: z.string(),
});

type FactoryFormValues = z.infer<typeof factoryFormSchema>;

interface EditFactoryFormProps {
    isOpen: boolean;
    onClose: () => void;
    onSubmit: (values: FactoryFormValues) => void;
    initialData?: FactoryFormValues;
}

export function EditFactoryForm({ isOpen, onClose, onSubmit, initialData }: EditFactoryFormProps) {
    const form = useForm<FactoryFormValues>({
        resolver: zodResolver(factoryFormSchema),
        defaultValues: initialData || {
            name: "",
            location: "",
            buildDate: "",
            manager: "",
            phone: "",
            status: "running",
            description: "",
        },
    });

    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
            <Card className="w-[600px] p-6">
                <div className="flex justify-between items-center mb-6">
                    <h3 className="text-lg font-semibold">{initialData ? '编辑工厂' : '新增工厂'}</h3>
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
                                        <FormLabel>工厂名称</FormLabel>
                                        <FormControl>
                                            <Input placeholder="请输入工厂名称" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="location"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>所在地</FormLabel>
                                        <FormControl>
                                            <Input placeholder="请输入工厂所在地" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="buildDate"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>建成时间</FormLabel>
                                        <FormControl>
                                            <Input type="date" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="manager"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>负责人</FormLabel>
                                        <FormControl>
                                            <Input placeholder="请输入负责人姓名" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="phone"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>联系电话</FormLabel>
                                        <FormControl>
                                            <Input placeholder="请输入联系电话" {...field} />
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
                                                <option value="building">建设中</option>
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
                                        <FormLabel>工厂简介</FormLabel>
                                        <FormControl>
                                            <textarea
                                                className="w-full h-24 p-3 rounded-md border border-gray-200 focus:outline-none focus:ring-2 focus:ring-blue-500"
                                                placeholder="请输入工厂简介"
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