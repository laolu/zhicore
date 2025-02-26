"use client"

import * as React from "react"

import { NavMain } from "@/components/nav-main"
import { NavProjects } from "@/components/nav-projects"
import { NavUser } from "@/components/nav-user"
import { TeamSwitcher } from "@/components/team-switcher"
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarHeader,
  SidebarRail,
} from "@/components/ui/sidebar"

import {menuConfig} from "@/config/menu"

export function AppSidebar({ ...props }: React.ComponentProps<typeof Sidebar>) {
  return (
    <Sidebar collapsible="icon" {...props}>
      <SidebarHeader>
        <TeamSwitcher teams={menuConfig.teams} />
      </SidebarHeader>
      <SidebarContent>
        <NavMain items={menuConfig.navMain} />
        <NavProjects projects={menuConfig.projects} />
      </SidebarContent>
      <SidebarFooter>
        <NavUser user={menuConfig.user} />
      </SidebarFooter>
      <SidebarRail />
    </Sidebar>
  )
}
