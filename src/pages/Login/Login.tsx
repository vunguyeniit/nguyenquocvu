import { useForm } from 'react-hook-form'
import { Link, useNavigate } from 'react-router-dom'
import Input from '../../components/Input/Input'
import { yupResolver } from '@hookform/resolvers/yup/src/yup.js'
import { LoginSchema, loginSchema } from '../../utils/rules'
import { useMutation } from '@tanstack/react-query'
import authApi from '../../apis/auth.api'
import { ErrorResponse } from '../../types/utils.type'
import { isAxiosUnprocessableEntityError } from '../../utils/utils'
import { toast } from 'react-toastify'
import { useContext } from 'react'
import { AppContext } from '../../contexts/app.context'
import { Button } from '../../components/Button/Button'
import path from '../../constants/path'
type FormData = LoginSchema
export default function Login() {
  //Sử dụng useContext xét lại giá trị
  const { setIsAuthenticated, setProfile } = useContext(AppContext)
  const navigate = useNavigate()
  const {
    setError,
    register,
    handleSubmit,
    formState: { errors }
  } = useForm<FormData>({
    resolver: yupResolver(loginSchema)
  })

  const loginAccountMutation = useMutation({
    mutationFn: (body: FormData) => authApi.login(body)
  })
  //   console.log('bbbl', loginAccountMutation.isPending)
  const onSubmit = handleSubmit((data) => {
    loginAccountMutation.mutate(data, {
      onSuccess: (data) => {
        setIsAuthenticated(true)
        setProfile(data.data.data.user)
        navigate('/')
        toast.success('Login thành công')
      },
      onError: (error) => {
        //  console.log(error)
        if (isAxiosUnprocessableEntityError<ErrorResponse<FormData>>(error)) {
          const formError = error.response?.data.data
          //Cách 1 Error
          if (formError) {
            Object.keys(formError).forEach((key) => {
              setError(key as keyof FormData, {
                type: 'Server',
                message: formError[key as keyof FormData]
              })
              toast.error('Login thất bại')
            })
          }
        }
      }
    })
  })
  console.log(errors)
  return (
    <div className='bg-orange'>
      <div className='container'>
        <div className='grid grid-cols-1 lg:grid-cols-5 py-12 lg:py-32 lg:pr-10'>
          <div className='lg:col-span-2 lg:col-start-4'>
            <form className='p-10 rounded bg-white shadow-sm' onSubmit={onSubmit}>
              <div className='text-2xl'>Đăng Nhập</div>
              <div className='mt-8'>
                <Input
                  name='email'
                  register={register}
                  type='email'
                  className='mt-8'
                  errorMessage={errors.email?.message}
                  placeholder='Email'
                />
                {/* <input
                  type='email'
                  name='email'
                  className='p-3 w-full outline-none border border-gray-300 focus:border-gray-500 rounded-sm focus:shadow-sm'
                  placeholder='Email'
                /> */}
                <div className='mt-1 text-red-600 min-h-[1rem] text-sm'></div>
              </div>

              <div className='mt-3'>
                <Input
                  name='password'
                  register={register}
                  type='password'
                  className='mt-3'
                  errorMessage={errors.password?.message}
                  placeholder='Password'
                  //  rules={rules.password}
                />
                {/* <input
                  type='password'
                  name='password'
                  className='p-3 w-full outline-none border border-gray-300 focus:border-gray-500 rounded-sm focus:shadow-sm'
                  placeholder='Password'
                /> */}
                <div className='mt-1 text-red-600 min-h-[1rem] text-sm'></div>
              </div>
              <div className='mt-3'>
                <Button
                  className='w-full  bg-red-500 py-4  px-2 uppercase text-white text-sm hover:bg-red-600 flex justify-center items-center'
                  //xử lí hiệu ứng khi không có data nút xoay tròn
                  isLoading={loginAccountMutation.isPending}
                  disabled={loginAccountMutation.isPending}
                >
                  Đăng nhập
                </Button>
              </div>
              <div className='flex items-center justify-center mt-5'>
                <span className='text-slate-400'>Bạn mới biết đến Shopee?</span>
                <Link to={path.register} className='text-red-400 ml-1'>
                  Đăng ký
                </Link>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  )
}
