"use client"

import { SidebarInset, SidebarTrigger } from "@/components/ui/sidebar"
import { Separator } from "@/components/ui/separator"
import { ScrollArea } from "@/components/ui/scroll-area"
import { BreadcrumbNav } from "@/components/breadcrumb-nav"

export function MainLayout({ children }: { children: React.ReactNode }) {
  return (
    <SidebarInset className="h-full flex flex-col">
      <header className="flex h-16 shrink-0 items-center gap-2">
        <div className="flex items-center gap-2 px-4">
          <SidebarTrigger className="-ml-1" />
          <Separator orientation="vertical" className="mr-2 h-4" />
          <BreadcrumbNav />
        </div>
      </header>
      <ScrollArea className="absolute inset-0">
        {children}
      </ScrollArea>
    </SidebarInset>
  )
}