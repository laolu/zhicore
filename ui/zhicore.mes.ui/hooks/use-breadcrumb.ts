"use client"

import { usePathname } from "next/navigation"
import { menuConfig } from "@/config/menu"

interface Breadcrumb {
  title: string
  href: string
  active?: boolean
}

export function useBreadcrumb() {
  const pathname = usePathname()

  const getBreadcrumbs = (): Breadcrumb[] => {
    const breadcrumbs: Breadcrumb[] = []
    
    // 查找匹配的主菜单和子菜单
    for (const mainItem of menuConfig.navMain) {
      if (pathname.startsWith(mainItem.url)) {
        breadcrumbs.push({
          title: mainItem.title,
          href: mainItem.url,
        })
        
        // 查找匹配的子菜单
        const subItem = mainItem.items?.find(item => item.url === pathname)
        if (subItem) {
          breadcrumbs.push({
            title: subItem.title,
            href: subItem.url,
            active: true,
          })
        }
        break
      }
    }
    
    return breadcrumbs
  }

  return getBreadcrumbs()
}