import { RegisterOptions, UseFormGetValues } from 'react-hook-form'
import * as yup from 'yup'
type Rules = {
  [key in 'email' | 'password' | 'confirm_password']?: RegisterOptions
}
//C1 sử dụng validate hook form cơ bản
//getRules không xài nữa
export const getRules = (getValues?: UseFormGetValues<any>): Rules => ({
  email: {
    required: {
      value: true,
      message: 'Email không được để trống'
    },
    pattern: {
      value: /^\S+@\S+\.\S+$/,
      message: 'Email không đúng định dạng'
    },
    maxLength: {
      value: 160,
      message: 'Độ dài từ 5 -160 ký tự'
    },
    minLength: {
      value: 5,
      message: 'Độ dài từ 5 -160 ký tự'
    }
  },
  password: {
    required: {
      value: true,
      message: 'Mk không được để trống'
    },

    maxLength: {
      value: 160,
      message: 'Độ dài từ 6 -160 ký tự'
    },
    minLength: {
      value: 6,
      message: 'Độ dài từ 6 -160 ký tự'
    }
  },
  confirm_password: {
    required: {
      value: true,
      message: 'Mk không được để trống'
    },

    maxLength: {
      value: 160,
      message: 'Độ dài từ 6 -160 ký tự'
    },
    minLength: {
      value: 6,
      message: 'Độ dài từ 6 -160 ký tự'
    },
    //C2 confirm_password
    validate:
      typeof getValues === 'function'
        ? (value) => value === getValues('password') || 'Nhập lại password không khớp'
        : undefined
  }
})
//---------------------------------------------
//C2  sử dụng validate hook form Yup
export const schema = yup.object({
  email: yup
    .string()
    .required('Email là bắt buộc')
    .email('Email không đúng định dạn')
    .min(5, 'Độ dài từ 6 -160 ký tự')
    .max(160, 'Độ dài từ 6 -160 ký tự'),
  password: yup
    .string()
    .required('Nhập lại Password là bắt buộc')
    .min(6, 'Độ dài từ 6 -160 ký tự')
    .max(160, 'Độ dài từ 6 -160 ký tự'),
  confirm_password: yup
    .string()
    .required('Nhập lại Password là bắt buộc')
    .min(6, 'Độ dài từ 6 -160 ký tự')
    .max(160, 'Độ dài từ 6 -160 ký tự')
    //Dùng để so sánh password và cofim-password
    .oneOf([yup.ref('password')], 'Nhập lại password không khớp')
})
//Cách khai báo interface kế thừa yub thay vì phải khai báo interface Register FromData
export type Schema = yup.InferType<typeof schema>

//Khi muốn chỉ lấy email,password không lấy confirm_password dùng omit
export const loginSchema = schema.omit(['confirm_password'])
//Cách khai báo interface kế thừa yub thay vì phải khai báo interface Login FromData
export type LoginSchema = yup.InferType<typeof loginSchema>
