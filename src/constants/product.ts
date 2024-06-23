//Khai báo đảm bảo dữ liệu này có để đọc không được sửa
export const sortBy = {
  createdAt: 'createdAt',
  view: 'view',
  sold: 'sold',
  price: 'price'
} as const

export const order = {
  asc: 'asc',
  desc: 'desc'
} as const
