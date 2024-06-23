import { User } from '../types/user.type'
//Câu hình Token
export const setAccessTokenToLS = (access_token: string) => {
  localStorage.setItem('access_token', access_token)
}
export const clearLS = () => {
  localStorage.removeItem('access_token')
  localStorage.removeItem('profile')
}
export const getAccesTokenToLS = () => localStorage.getItem('access_token') || ''

//Lấy token và chuyển json thành object
export const getProfileFromLS = () => {
  const result = localStorage.getItem('profile')
  return result ? JSON.parse(result) : null
}
//Lưu token chuyển nó thành json
export const setProfileToLS = (profile: User) => {
  localStorage.setItem('profile', JSON.stringify(profile))
}
