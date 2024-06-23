import classNames from 'classnames'
import { sortBy, order as orderConstant } from '../../../constants/product'
import { QueryConfig } from '../ProductList'
import { ProductListConfig } from '../../../types/product.type'
import { Link, createSearchParams, useNavigate } from 'react-router-dom'
import path from '../../../constants/path'
import { omit } from 'lodash'

interface Props {
  queryConfig: QueryConfig
  pageSize: number
}
export const SortProductList = ({ queryConfig, pageSize }: Props) => {
  const page = Number(queryConfig.page)

  const navigate = useNavigate()
  //Nếu sort by không có mặc định lấy sortBy createAt
  const { sort_by = sortBy.createdAt, order } = queryConfig
  //Hàm check active nút button
  //Exclude của typescript loại bỏ undefined
  const isActiveSortBy = (sortByValue: Exclude<ProductListConfig['sort_by'], undefined>) => {
    return sort_by === sortByValue //Xử lí active thì classname nó đổi màu
  }
  //Xử lý khi onclick
  const handleSort = (sortByValue: Exclude<ProductListConfig['sort_by'], undefined>) => {
    navigate({
      pathname: path.home,
      search: createSearchParams(
        //dùng omit để loại tham số url order vì khi sort phổ biến hay mới nhất trả về sp phổ biến hay mới nhất thôi nếu có order sort thì không đúng
        omit(
          {
            ...queryConfig,
            sort_by: sortByValue //Xử lí active khi onclick SortBy
          },
          ['order']
        )
      ).toString()
    })
  }
  //Xử lý select Gía từ cao đến thấp
  const handlePriceOrder = (orderValue: Exclude<ProductListConfig['order'], undefined>) => {
    navigate({
      pathname: path.home,
      search: createSearchParams({
        ...queryConfig,
        //sort theo giá cả và giá cả thấp đến cao hay đến thấp
        sort_by: sortBy.price, //theo giá
        order: orderValue //theo giá cao hay thấp
      }).toString()
    })
  }
  return (
    <div className='bg-gray-300/40 py-4 px-3'>
      <div className='flex flex-wrap items-center justify-between gap-2'>
        <div className='flex flex-wrap items-center gap-2'>
          <div>Sắp xếp theo</div>
          <button
            className={classNames('h-8 px-4 capitalize text-sm  text-center', {
              'bg-orange text-white hover:bg-orange/80 ': isActiveSortBy(sortBy.view),
              'bg-white text-black hover:bg-slate-100': !isActiveSortBy(sortBy.view)
            })}
            onClick={() => handleSort(sortBy.view)}
          >
            Phổ biến
          </button>
          <button
            className={classNames('h-8 px-4 capitalize text-sm  text-center', {
              'bg-orange text-white hover:bg-orange/80 ': isActiveSortBy(sortBy.createdAt),
              'bg-white text-black hover:bg-slate-100': !isActiveSortBy(sortBy.createdAt)
            })}
            onClick={() => handleSort(sortBy.createdAt)}
          >
            Mới nhất
          </button>

          <button
            className={classNames('h-8 px-4 capitalize text-sm  text-center', {
              'bg-orange text-white hover:bg-orange/80 ': isActiveSortBy(sortBy.sold),
              'bg-white text-black hover:bg-slate-100': !isActiveSortBy(sortBy.sold)
            })}
            onClick={() => handleSort(sortBy.sold)}
          >
            Bán chạy
          </button>

          <button className='h-8 px-4 capitalize bg-white text-black text-sm hover:bg-slate-100 text-center'>
            Gía
          </button>
          <select
            className={classNames('h-8 px-4 capitalize text-sm   outline:none text-left', {
              'bg-orange text-white hover:bg-orange/80 ': isActiveSortBy(sortBy.price),
              'bg-white text-black hover:bg-slate-100': !isActiveSortBy(sortBy.price)
            })}
            //Nếu mặc định không có order thì lấy option giá
            value={order || ''}
            // Argument of type 'string' is not assignable to parameter of type '"asc" | "desc"'.ts(2345) nó báo lỗi event.target.value nó string nhưng handlePriceOrder nhận desc asc nên ép kiểu as
            onChange={(event) => handlePriceOrder(event.target.value as Exclude<ProductListConfig['order'], undefined>)}
          >
            <option value='' disabled className='bg-white text-black'>
              Gía
            </option>
            {/* Xứ lí lọc giá thấp đến cao */}
            <option className='bg-white text-black ' value={orderConstant.asc}>
              Gía: Thấp đến cao
            </option>
            <option className='bg-white text-black' value={orderConstant.desc}>
              Gía: Cao đến thấp
            </option>
          </select>
        </div>

        <div className='flex items-center'>
          <div>
            <span className='text-orange'>{page}</span>
            <span>/{pageSize}</span>
          </div>
          {/* Check 2 nút */}
          <div className='ml-2 flex'>
            {/*  */}
            {page === 1 ? (
              <span className='flex items-center justify-center h-8 w-9 rounded-tl-sm rounded-bl-sm bg-white/60 hover:bg-slate-100 cursor-not-allowed'>
                <svg
                  xmlns='http://www.w3.org/2000/svg'
                  fill='none'
                  viewBox='0 0 24 24'
                  strokeWidth={1.5}
                  stroke='currentColor'
                  className='w-3 h-3'
                >
                  <path strokeLinecap='round' strokeLinejoin='round' d='M15.75 19.5 8.25 12l7.5-7.5' />
                </svg>
              </span>
            ) : (
              <Link
                to={{
                  pathname: path.home,
                  search: createSearchParams({
                    ...queryConfig,

                    page: (page - 1).toString()
                  }).toString()
                }}
                className='flex items-center justify-center h-8 w-9 rounded-tl-sm rounded-bl-sm bg-white hover:bg-slate-100 '
              >
                <svg
                  xmlns='http://www.w3.org/2000/svg'
                  fill='none'
                  viewBox='0 0 24 24'
                  strokeWidth={1.5}
                  stroke='currentColor'
                  className='w-3 h-3'
                >
                  <path strokeLinecap='round' strokeLinejoin='round' d='M15.75 19.5 8.25 12l7.5-7.5' />
                </svg>
              </Link>
            )}
            {/*  */}

            {/*  */}
            {page === pageSize ? (
              <span className='flex items-center justify-center h-8 w-9 rounded-tl-sm rounded-bl-sm bg-white/60 hover:bg-slate-100 cursor-not-allowed'>
                <svg
                  xmlns='http://www.w3.org/2000/svg'
                  fill='none'
                  viewBox='0 0 24 24'
                  strokeWidth={1.5}
                  stroke='currentColor'
                  className='w-3 h-3'
                >
                  <path strokeLinecap='round' strokeLinejoin='round' d='m8.25 4.5 7.5 7.5-7.5 7.5' />
                </svg>
              </span>
            ) : (
              <Link
                to={{
                  pathname: path.home,
                  search: createSearchParams({
                    ...queryConfig,

                    page: (page + 1).toString()
                  }).toString()
                }}
                className='flex items-center justify-center h-8 w-9 rounded-tl-sm rounded-bl-sm bg-white hover:bg-slate-100  '
              >
                <svg
                  xmlns='http://www.w3.org/2000/svg'
                  fill='none'
                  viewBox='0 0 24 24'
                  strokeWidth={1.5}
                  stroke='currentColor'
                  className='w-3 h-3'
                >
                  <path strokeLinecap='round' strokeLinejoin='round' d='m8.25 4.5 7.5 7.5-7.5 7.5' />
                </svg>
              </Link>
            )}
            {/*  */}
          </div>
        </div>
      </div>
    </div>
  )
}
