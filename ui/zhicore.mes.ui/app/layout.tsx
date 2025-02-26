import "./globals.css";
import { AppSidebar } from "@/components/app-sidebar"
import { SidebarProvider } from "@/components/ui/sidebar"
import { MainLayout } from "@/components/main-layout"

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en" className="h-full">
      <body className="h-full overflow-hidden">
        <SidebarProvider>
          <AppSidebar />
          <MainLayout>
            {children}
          </MainLayout>
        </SidebarProvider>
      </body>
    </html>
  )
}
