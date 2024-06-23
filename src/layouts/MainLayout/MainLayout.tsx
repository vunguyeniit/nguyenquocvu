import React from 'react'
import Footer from '../../components/Footer'
import { Header } from '../../components/Header/Header'
interface Props {
  children: React.ReactNode
}
export const MainLayout = ({ children }: Props) => {
  //   console.log('>>>', children)
  return (
    <div>
      <Header />
      {children}
      <Footer />
    </div>
  )
}
