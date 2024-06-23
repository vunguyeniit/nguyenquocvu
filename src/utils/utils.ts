import axios, { AxiosError } from 'axios'
import HttpStatusCode from '../constants/http.status.enum'
//Kiểm tra nó phải stack AxiosError
export function isAxiosErrors<T>(error: unknown): error is AxiosError<T> {
  return axios.isAxiosError(error)
}
//Kiểm tra nó phải status == 422 không
export function isAxiosUnprocessableEntityError<T>(error: unknown): error is AxiosError<T> {
  return isAxiosErrors(error) && error.response?.status === HttpStatusCode.UnprocessableEntity
}

//Format lại Gía
export function formatCurrency(currency: number) {
  return new Intl.NumberFormat('de-DE').format(currency)
}
//Format lại Đã bán
export function formatNumberToSocialStyle(value: number) {
  return new Intl.NumberFormat('en', { notation: 'compact', maximumFractionDigits: 1 })
    .format(value)
    .replace('.', ',')
    .toLowerCase()
}
