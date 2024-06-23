import { useForm } from 'react-hook-form'
import { Link, useNavigate } from 'react-router-dom'
import { schema, Schema, getRules } from '../../utils/rules'
import Input from '../../components/Input/Input'
import { yupResolver } from '@hookform/resolvers/yup/src/yup.js'
import { useMutation } from '@tanstack/react-query'
import authApi from '../../apis/auth.api'
import { omit } from 'lodash'
import { isAxiosUnprocessableEntityError } from '../../utils/utils'
import { ErrorResponse } from '../../types/utils.type'
import { useContext } from 'react'
import { AppContext } from '../../contexts/app.context'
import { Button } from '../../components/Button/Button'
import path from '../../constants/path'

type FormData = Schema
export default function Register() {
  const { setIsAuthenticated } = useContext(AppContext)
  const navigate = useNavigate()
  //   interface FormData {
  //     email: string
  //     password: string
  //     confirm_password: string
  //   }
  const {
    register,
    handleSubmit,
    setError,
    // getValues,
    formState: { errors }
  } = useForm<FormData>({
    resolver: yupResolver(schema)
  })

  const registerAccountMutation = useMutation({
    mutationFn: (body: Omit<FormData, 'confirm_password'>) => authApi.registerAccount(body)
  })
  //C1 validate hook form cơ bản
  //   const rules = getRules(getValues)
  //C2 sử dụng validate hook form Yup
  const onSubmit = handleSubmit(
    //chạy vào đây khi from dữ liệu đúng
    (data) => {
      //Sử dụng thư viện losh để có omit lạo bỏ confirm_password gửi lên
      const body = omit(data, ['confirm_password'])
      registerAccountMutation.mutate(body, {
        onSuccess: (data) => {
          setIsAuthenticated(true)
          navigate('/')
        },
        onError: (error) => {
          console.log('422', error)
          //Xử lí trường hợp 422
          if (isAxiosUnprocessableEntityError<ErrorResponse<Omit<FormData, 'confirm_password'>>>(error)) {
            const formError = error.response?.data.data
            //Cách 1 Error
            if (formError) {
              Object.keys(formError).forEach((key) => {
                setError(key as keyof Omit<FormData, 'confirm_password'>, {
                  type: 'Server',
                  message: formError[key as keyof Omit<FormData, 'confirm_password'>]
                })
              })
            }
            //Cách 2 error
            // if (formError?.email) {
            //   setError('email', {
            //     message: formError.email,
            //     type: 'Server'
            //   })
            // }
            // if (formError?.password) {
            //   setError('password', {
            //     message: formError.password,
            //     type: 'Server'
            //   })
            // }
          }
        }
      })
    }
    //chạy vào đây khi from dữ liệu không đúng
    //  (data) => {
    //    const password = getValues('password')
    //    console.log('password', password)
    //  }
  )
  //  console.log('error', errors)
  return (
    <div className='bg-orange'>
      <div className='container'>
        <div className='grid grid-cols-1 lg:grid-cols-5 py-12 lg:py-32 lg:pr-10'>
          <div className='lg:col-span-2 lg:col-start-4'>
            <form className='p-10 rounded bg-white shadow-sm' onSubmit={onSubmit}>
              <div className='text-2xl'>Đăng ký</div>
              <Input
                name='email'
                register={register}
                type='email'
                className='mt-8'
                errorMessage={errors.email?.message}
                placeholder='Email'
                //  rules={rules.email}
              />
              {/* <div className='mt-8'>
                <input
                  type='text'
                  className='p-3 w-full outline-none border border-gray-300 focus:border-gray-500 rounded-sm focus:shadow-sm'
                  placeholder='Email'
                  {...register('email', rules.email)}
                />
                <div className='mt-1 text-red-600 min-h-[1rem] text-sm'>{errors.email?.message}</div>
              </div> */}
              <Input
                name='password'
                register={register}
                type='password'
                className='mt-3'
                errorMessage={errors.password?.message}
                placeholder='Password'
                //  rules={rules.password}
              />
              {/* <div className='mt-3'>
                <input
                  type='password'
                  className='p-3 w-full outline-none border border-gray-300 focus:border-gray-500 rounded-sm focus:shadow-sm'
                  placeholder='Password'
                  {...register('password', rules.password)}
                />
                <div className='mt-1 text-red-600 min-h-[1rem] text-sm'>{errors.password?.message}</div>
              </div> */}
              <Input
                name='confirm_password'
                register={register}
                type='password'
                className='mt-3'
                errorMessage={errors.confirm_password?.message}
                placeholder='confirm_password'
                //  rules={rules.confirm_password}
              />
              {/* <div className='mt-3'>
                <input
                  type='password'
                  className='p-3 w-full outline-none border border-gray-300 focus:border-gray-500 rounded-sm focus:shadow-sm'
                  placeholder='Confirm Password'
                  {...register('confirm_password', {
                    ...rules.confirm_password
                    //C1 confirm_password
                    //validate: (value) => value === getValues('password') || 'Nhập lại password không khớp'
                  })}
                />
                <div className='mt-1 text-red-600 min-h-[1rem] text-sm'>{errors.confirm_password?.message}</div>
              </div> */}
              <div className='mt-3'>
                <Button
                  type='submit'
                  className='w-full flex justify-center items-center bg-red-500 py-4 px-2 uppercase text-white text-sm hover:bg-red-600'
                  isLoading={registerAccountMutation.isPending}
                  disabled={registerAccountMutation.isPending}
                >
                  Đăng Ký
                </Button>
              </div>

              <div className='flex items-center justify-center mt-5'>
                <span className='text-slate-400'>Bạn mới biết đến Shopee ?</span>
                <Link to={path.login} className='text-red-400 ml-1'>
                  Đăng nhập
                </Link>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  )
}
