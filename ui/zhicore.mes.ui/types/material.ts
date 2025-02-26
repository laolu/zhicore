export interface Material {
  id: string
  code: string
  name: string
  spec: string
  unit: string
  category: string
  stock: number
  safeStock: number
  maxStock: number
  standardPrice: number
  lastPurchasePrice: number
  avgPrice: number
  mainSupplier: string
  backupSupplier: string
}

export interface Warehouse {
  id: string
  name: string
  type: 'main' | 'sub' | 'temp'
  address: string
  manager: string
  contact: string
  area: number
  description?: string
}