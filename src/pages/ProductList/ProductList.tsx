import { keepPreviousData, useQuery } from '@tanstack/react-query'
import { AsideFilter } from './AsideFilter/AsideFilter'
import { Product } from './Product/Product'
import { SortProductList } from './SortProductList/SortProductList'
import useQueryParams from '../../hooks/useQueryParams'
import productApi from '../../apis/product.api'
import Pagination from '../../components/Pagination/Pagination'
import { ProductListConfig } from '../../types/product.type'
import { isUndefined, omitBy } from 'lodash'
export type QueryConfig = {
  [key in keyof ProductListConfig]: string
}
export default function ProductList() {
  //   const [page, setPage] = useState(1)
  const queryParams: QueryConfig = useQueryParams()
  //Lấy những giá trị query cần thiết  URL thôi của API
  const queryConfig: QueryConfig = omitBy(
    // omitBy,isUndefined loại bỏ undefined
    {
      page: queryParams.page || '1',
      sort_by: queryParams.sort_by,
      exclude: queryParams.exclude,
      name: queryParams.name,
      order: queryParams.order,
      limit: queryParams.limit || '20',
      price_max: queryParams.price_max,
      price_min: queryParams.price_min,
      rating_filter: queryParams.rating_filter
    },
    isUndefined
  )
  //   const { data } = useQuery({
  //     queryKey: ['products', queryParams],
  //     queryFn: () => {
  //       return productApi.getProducts(queryParams)
  //     }
  //   })
  //Gọi API
  const { data } = useQuery({
    queryKey: ['products', queryConfig],
    queryFn: () => {
      return productApi.getProducts(queryConfig as ProductListConfig)
    },
    placeholderData: keepPreviousData
  })
  //   console.log(queryConfig)
  //   console.log(data)
  return (
    <div className='bg-gray-200 py-6'>
      <div className='container'>
        {data && (
          <div className='grid grid-cols-12 gap-6'>
            <div className='col-span-3'>
              <AsideFilter />
            </div>
            <div className='col-span-9'>
              <SortProductList queryConfig={queryConfig} pageSize={data.data.data.pagination.page_size} />
              <div className='mt-6 grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-3'>
                {data.data.data.products.map((product) => (
                  <div className='col-span-1' key={product._id}>
                    <Product product={product} />
                  </div>
                ))}
              </div>
              {/* tại sao không truyền page={1} setPage={() => {}} pageSize={10} như vậy mà truyền queryConfig vì giúp giữ lại giá trị cũ ví dụ như page=2&sort=asc nếu thay đổi thì thay đổi page không xóa tham số phía sau */}
              <Pagination queryConfig={queryConfig} pageSize={data.data.data.pagination.page_size} />
            </div>
          </div>
        )}
      </div>
    </div>
  )
}
