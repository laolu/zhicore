import React from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";

interface MainLayoutProps {
  children: React.ReactNode;
}

const MainLayout: React.FC<MainLayoutProps> = ({ children }) => {
  const [searchValue, setSearchValue] = React.useState("");

  return (
    <div className="min-h-screen bg-gray-50">
      {/* 顶部导航 */}
      <div className="fixed top-0 left-0 right-0 h-16 bg-white shadow-sm z-50 flex items-center justify-between px-6">
        <div className="flex items-center gap-8">
          <Button variant="ghost" className="text-xl font-bold text-blue-600 hover:bg-transparent">
            MES 生产管理系统
          </Button>
          <div className="relative">
            <Input
              type="text"
              placeholder="全局搜索..."
              className="w-96 pl-10 border-gray-200"
              value={searchValue}
              onChange={(e) => setSearchValue(e.target.value)}
            />
          </div>
        </div>
      </div>
      {/* 主要内容区域 */}
      <div className="pt-20 px-6">
        {children}
      </div>
    </div>
  );
};

export default MainLayout;