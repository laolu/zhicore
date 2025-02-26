import React from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Badge } from "@/components/ui/badge";
import { Card } from "@/components/ui/card";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import * as echarts from "echarts";

const ProductionMonitoring: React.FC = () => {
  const [selectedTab, setSelectedTab] = React.useState("realtime");

  React.useEffect(() => {
    // 生产进度图表
    const progressChart = echarts.init(document.getElementById("progressChart"));
    const progressOption = {
      animation: false,
      title: {
        text: "生产进度监控",
        left: "center",
      },
      tooltip: {
        trigger: "axis",
      },
      xAxis: {
        type: "category",
        data: ["周一", "周二", "周三", "周四", "周五", "周六", "周日"],
      },
      yAxis: {
        type: "value",
      },
      series: [
        {
          name: "计划完成量",
          type: "line",
          data: [120, 132, 101, 134, 90, 230, 210],
        },
        {
          name: "实际完成量",
          type: "line",
          data: [110, 128, 98, 135, 88, 225, 208],
        },
      ],
    };
    progressChart.setOption(progressOption);

    return () => {
      progressChart.dispose();
    };
  }, []);

  return (
    <div className="p-6">
      <div className="mb-6">
        <h1 className="text-2xl font-bold mb-2">生产进度监控</h1>
        <p className="text-gray-500">实时监控生产线运行状态和进度情况</p>
      </div>
      <Tabs value={selectedTab} onValueChange={setSelectedTab} className="mb-6">
        <TabsList>
          <TabsTrigger value="realtime">实时监控</TabsTrigger>
          <TabsTrigger value="analysis">进度分析</TabsTrigger>
          <TabsTrigger value="history">历史记录</TabsTrigger>
        </TabsList>
        <TabsContent value="realtime">
          <div className="grid grid-cols-3 gap-6 mb-6">
            {[
              {
                title: "A 线生产状态",
                status: "运行中",
                progress: 85,
                speed: "32 件/小时",
                operator: "陈志明",
                color: "bg-green-500"
              },
              {
                title: "B 线生产状态",
                status: "计划中",
                progress: 0,
                speed: "待启动",
                operator: "李雪梅",
                color: "bg-gray-500"
              },
              {
                title: "C 线生产状态",
                status: "暂停维护",
                progress: 45,
                speed: "0 件/小时",
                operator: "王建国",
                color: "bg-yellow-500"
              }
            ].map((line) => (
              <Card key={line.title} className="p-6">
                <div className="flex justify-between items-center mb-4">
                  <h3 className="font-semibold">{line.title}</h3>
                  <Badge className={line.status === "运行中" ? "bg-green-500" : line.status === "计划中" ? "bg-gray-500" : "bg-yellow-500"}>
                    {line.status}
                  </Badge>
                </div>
                <div className="space-y-4">
                  <div className="h-2 bg-gray-100 rounded-full overflow-hidden">
                    <div
                      className={`h-full ${line.color} transition-all duration-500`}
                      style={{ width: `${line.progress}%` }}
                    ></div>
                  </div>
                  <div className="grid grid-cols-2 gap-4">
                    <div>
                      <p className="text-sm text-gray-500">生产速度</p>
                      <p className="font-medium">{line.speed}</p>
                    </div>
                    <div>
                      <p className="text-sm text-gray-500">操作员</p>
                      <p className="font-medium">{line.operator}</p>
                    </div>
                  </div>
                </div>
              </Card>
            ))}
          </div>
          {/* 其他实时监控内容 */}
        </TabsContent>
        {/* 其他标签页内容 */}
      </Tabs>
    </div>
  );
};

export default ProductionMonitoring;