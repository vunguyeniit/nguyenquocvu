import React, { HtmlHTMLAttributes, InputHTMLAttributes } from 'react'
import type { UseFormRegister, RegisterOptions } from 'react-hook-form'
// interface Props {
//   type: React.HTMLInputTypeAttribute
//   errorMessage?: string
//   placeholder?: string
//   className?: string
//   name: string
//   register: UseFormRegister<any>
//   //rules?: RegisterOptions
// }
interface Props extends InputHTMLAttributes<HTMLInputElement> {
  errorMessage?: string
  classNameInput?: string
  classNameError?: string
  register?: UseFormRegister<any>
  //rules?: RegisterOptions
}
export default function Input({
  type,
  errorMessage,
  placeholder,
  className,
  name,
  register,
  classNameInput = 'p-3 w-full outline-none border border-gray-300 focus:border-gray-500 rounded-sm focus:shadow-sm',
  classNameError = 'mt-1 text-red-600 min-h-[1rem] text-sm'
  //rules
}: Props) {
  const RegisterResults = register && name ? register(name) : {}
  return (
    <div className={className}>
      <input
        type={type}
        className={classNameInput}
        placeholder={placeholder}
        //   {...register(
        //     name
        //     // rules
        //   )}
        {...RegisterResults}
      />
      <div className={classNameError}>{errorMessage}</div>
    </div>
  )
}
