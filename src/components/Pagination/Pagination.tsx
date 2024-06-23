import classNames from 'classnames'
import { QueryConfig } from '../../pages/ProductList/ProductList'
import { Link, createSearchParams } from 'react-router-dom'
import path from '../../constants/path'
interface Props {
  queryConfig: QueryConfig
  //   setPage: React.Dispatch<React.SetStateAction<number>>
  pageSize: number
}
const RANGE = 2
const Pagination = ({ queryConfig, pageSize }: Props) => {
  // format page thành number vì page queryConfig là string
  const page = Number(queryConfig.page)
  const renderPagination = () => {
    let dotAfter = false
    let dotBefore = false

    const renderDotBefore = (index: number) => {
      if (!dotBefore) {
        dotBefore = true
        return (
          <span key={index} className='bg-white rounded px-3 py-2 shadow-sm mx-2 cursor-pointer border'>
            ...
          </span>
        )
      }
      return null
    }
    const renderDotAfter = (index: number) => {
      if (!dotAfter) {
        dotAfter = true
        return (
          <span key={index} className='bg-white rounded px-3 py-2 shadow-sm mx-2 cursor-pointer border'>
            ...
          </span>
        )
      }
      return null
    }
    return Array(pageSize)
      .fill(0)
      .map((_, index) => {
        const pageNumber = index + 1
        //Trường hợp 1
        if (page <= RANGE * 2 + 1 && pageNumber > page + RANGE && pageNumber < pageSize - RANGE + 1) {
          //
          return renderDotAfter(index)
        } else if (page > RANGE * 2 + 1 && page < pageSize - RANGE * 2) {
          if (pageNumber < page - RANGE && pageNumber > RANGE) {
            return renderDotBefore(index)
          } else if (pageNumber > page + RANGE && pageNumber < pageSize - RANGE + 1) {
            return renderDotAfter(index)
          }
        } else if (page >= pageSize - RANGE * 2 && pageNumber > RANGE && pageNumber < page - RANGE) {
          return renderDotBefore(index)
        }
        console.log('>>>>>>>', { ...queryConfig })
        return (
          //  <button
          <Link
            to={{
              pathname: path.home,
              search: createSearchParams({
                ...queryConfig,

                //vì  page: pageNumber nó number mà createSearchParams là string nên chấm toString
                page: pageNumber.toString() //ghi đè page queryConfig khi số page chuyển đổi ví dụ 2
                //vì createSearchParams trả về string nên phải .toString
              }).toString()
            }}
            key={index}
            className={classNames('bg-white rounded px-3 py-2 shadow-sm mx-2 cursor-pointer', {
              'border-r-emerald-700': pageNumber == page,
              'border-transparent': pageNumber !== page
            })}
            // onClick={() => setPage(pageNumber)}
          >
            {pageNumber}
          </Link>
          //  </button>
        )
      })
  }

  return (
    <div className='flex flex-wrap mt-6 justify-center'>
      {page === 1 ? (
        <span className='bg-white rounded px-3 py-2 shadow-sm mx-2 cursor-not-allowed border'>Prev</span>
      ) : (
        <Link
          to={{
            pathname: path.home,
            search: createSearchParams({
              ...queryConfig,

              page: (page - 1).toString()
            }).toString()
          }}
          className='bg-white rounded px-3 py-2 shadow-sm mx-2 cursor-pointer border'
        >
          Prev
        </Link>
      )}

      {renderPagination()}
      {page === 1 ? (
        <span className='bg-white rounded px-3 py-2 shadow-sm mx-2 cursor-not-allowed  border'>Next</span>
      ) : (
        <Link
          to={{
            pathname: path.home,
            search: createSearchParams({
              ...queryConfig,

              page: (page + 1).toString()
            }).toString()
          }}
          className='bg-white rounded px-3 py-2 shadow-sm mx-2 cursor-pointer border'
        >
          Next
        </Link>
      )}
    </div>
  )
}
export default Pagination
