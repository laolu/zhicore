'use client'

import { usePathname } from 'next/navigation'
import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from "@/components/ui/breadcrumb"
import { menuConfig } from "@/config/menu"

export function DynamicBreadcrumb() {
  const pathname = usePathname()
  const segments = pathname.split('/').filter(Boolean)

  // 根据路径获取标题
  const getTitle = (path: string) => {
    const findMenuItem = (items: any[]): string | null => {
      for (const item of items) {
        if (item.url === `/${path}`) return item.title || item.name
        if (item.items) {
          const found = findMenuItem(item.items)
          if (found) return found
        }
      }
      return null
    }

    // 在主导航中查找
    const mainTitle = findMenuItem(menuConfig.navMain)
    if (mainTitle) return mainTitle

    // 在项目列表中查找
    const projectTitle = findMenuItem(menuConfig.projects)
    if (projectTitle) return projectTitle

    return path
  }

  if (segments.length === 0) return null

  return (
    <Breadcrumb>
      <BreadcrumbList>
        <BreadcrumbItem>
          <BreadcrumbLink href="/">仪表板</BreadcrumbLink>
        </BreadcrumbItem>
        {segments.map((segment, index) => {
          const href = `/${segments.slice(0, index + 1).join('/')}`
          const isLast = index === segments.length - 1
          const title = getTitle(segment)

          return (
            <BreadcrumbItem key={href}>
              <BreadcrumbSeparator />
              {isLast ? (
                <BreadcrumbPage>{title}</BreadcrumbPage>
              ) : (
                <BreadcrumbLink href={href}>{title}</BreadcrumbLink>
              )}
            </BreadcrumbItem>
          )
        })}
      </BreadcrumbList>
    </Breadcrumb>
  )
}