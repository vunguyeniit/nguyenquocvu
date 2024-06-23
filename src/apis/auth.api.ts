import { AuthResponse } from '../types/auth.type'
import http from '../utils/http'
//AuthResponse quy định data kiểu trả về được khai báo types
//C1
//Gom Api
const authApi = {
  registerAccount: (body: { email: string; password: string }) => http.post<AuthResponse>('/register', body),

  login: (body: { email: string; password: string }) => http.post<AuthResponse>('/login', body),

  logout: () => http.post('/logout')
}

export default authApi

//C2
// export const registerAccount = (body: { email: string; password: string }) => http.post<AuthResponse>('/register', body)

// export const login = (body: { email: string; password: string }) => http.post<AuthResponse>('/login', body)

// export const logout = () => http.post('/logout')
