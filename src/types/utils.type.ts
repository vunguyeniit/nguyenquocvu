// export interface ResponseApi<Data> {
//   message: string
//   data?: Data
// }

export interface SuccessResponse<Data> {
  message: string
  data: Data
}

export interface ErrorResponse<Data> {
  message: string
  data?: Data
}
