import { useSearchParams } from 'react-router-dom'
//Lấy tham số trên thanh URL
const useQueryParams = () => {
  const [searchParams] = useSearchParams()
  return Object.fromEntries([...searchParams])
}

export default useQueryParams
