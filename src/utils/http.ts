import axios, { AxiosError, AxiosInstance } from 'axios'
import HttpStatusCode from '../constants/http.status.enum'
import { toast } from 'react-toastify'
import { AuthResponse } from '../types/auth.type'
import { clearLS, getAccesTokenToLS, setAccessTokenToLS, setProfileToLS } from './auth'
import path from '../constants/path'
class Http {
  instance: AxiosInstance
  //Khi khai báo 1 biến trong class cần khởi tạo nó trong constructor(ví dụ accessToken)
  private accessToken: string
  constructor() {
    //Dùng lưu trên Ram mỗi lần truy xuất nhanh
    this.accessToken = getAccesTokenToLS()
    this.instance = axios.create({
      baseURL: 'https://api-ecom.duthanhduoc.com/',
      timeout: 10000,
      headers: {
        'Content-Type': 'application/json'
      }
    })
    //Xử lí những request api cần header  token  gửi lên server xác thực(ví dụ route)
    this.instance.interceptors.request.use(
      (config) => {
        if (this.accessToken) {
          //Truyền access token lên server bằng header
          config.headers.authorization = this.accessToken
          return config
        }
        return config
      },
      (error) => {
        return Promise.reject(error)
      }
    )
    //Xử lí những response api trả về
    this.instance.interceptors.response.use(
      (response) => {
        //lấy url ra check
        const { url } = response.config
        if (url === path.login || url === path.register) {
          //Gán accsesstoken vào localStoge
          const data = response.data as AuthResponse
          this.accessToken = data.data.access_token
          setAccessTokenToLS(this.accessToken)
          setProfileToLS(data.data.user)
        } else if (url === path.logout) {
          //Xóa accsessToken
          this.accessToken = ''
          clearLS()
        }
        return response
      },
      //Lỗi trả về từ axios mặt định
      function (error: AxiosError) {
        console.log(error)
        if (error.response?.status !== HttpStatusCode.UnprocessableEntity) {
          const data: any | undefined = error.response?.data
          const message = data.message || error.message
          toast.error(message)
        }
        return Promise.reject(error)
      }
    )
  }
}
const http = new Http().instance
export default http
