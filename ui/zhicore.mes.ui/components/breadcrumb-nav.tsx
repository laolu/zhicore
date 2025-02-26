"use client"

import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from "@/components/ui/breadcrumb"
import { useBreadcrumb } from "@/hooks/use-breadcrumb"

export function BreadcrumbNav() {
  const breadcrumbs = useBreadcrumb()

  return (
    <Breadcrumb>
      <BreadcrumbList>
        {breadcrumbs.map((item, index) => (
          <BreadcrumbItem key={item.href}>
            {index === breadcrumbs.length - 1 ? (
              <BreadcrumbPage>{item.title}</BreadcrumbPage>
            ) : (
              <BreadcrumbLink href={item.href}>
                {item.title}
              </BreadcrumbLink>
            )}
            {index < breadcrumbs.length - 1 && (
              <BreadcrumbSeparator />
            )}
          </BreadcrumbItem>
        ))}
      </BreadcrumbList>
    </Breadcrumb>
  )
}