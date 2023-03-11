@extends('layout.Clone-Admin')
@section('content')
    <section id="content">
        {{-- @include('admin.header') --}}
    @section('header')
        <nav>
            <div class="header-right">
                <div class="header-left">
                    <h2 style="color: #7E7D88;margin-right:1rem">Thiết bị <i class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2>Danh sách dịch vụ</h2>
                </div>
                <div class="profile">
                    <div class="icon-bell">
                        <i class="fa-solid fa-bell"></i>
                    </div>
                    <div class="img_content">
                        <img src="{{ asset('assets/images/user.png') }}">
                        <div class="user-content">
                            <p>Xin Chào</p>
                            <h3>Lê Quỳnh Ái Vân</h3>
                        </div>
                    </div>

                </div>

            </div>

            <div class="title">
                <h2>Quản lý dịch vụ</h2>
            </div>
        </nav>
    @endsection



    <main>

        <div class="select">
            <form action="" class="row">
                <div class="col-md-2">
                    <label for="inputEmail4" class="form-label fs-3">Tên dịch vụ</label>
                    <select class="form-select py-2 fs-3" aria-label="Default select example">
                        <option selected>Tất cả</option>
                        <option value="2">Hoạt động</option>
                        <option value="3">Ngưng hoạt động</option>
                    </select>
                </div>
                <div class="col-md-2">
                    <label for="inputEmail4" class="form-label fs-3">Tình trạng</label>
                    <select class="form-select py-2 fs-3" aria-label="Default select example">
                        <option selected>Tất cả</option>
                        <option value="2">Hoạt động</option>
                        <option value="3">Ngưng hoạt động</option>
                    </select>
                </div>
                <div class="col-md-2">
                    <label for="inputEmail4" class="form-label fs-3">Nguồn cáp</label>
                    <select class="form-select py-2 fs-3" aria-label="Default select example">
                        <option selected>Tất cả</option>
                        <option value="2">Hoạt động</option>
                        <option value="3">Ngưng hoạt động</option>
                    </select>
                </div>
                <div class="col-md-2">
                    <label for="inputPassword4" class="form-label fs-3">Chọn thời gian</label>
                    <input type="date" class="form-control py-1 fs-2 " id="inputPassword4">
                </div>
                <div class="col-md-2">
                    <label for="inputPassword4" class="form-label fs-3" style=" visibility: hidden;">Chọn thời
                        gian</label>
                    <input type="date" class="form-control py-1 fs-2 " id="inputPassword4">
                </div>
                <div class="col-md-3"style="width:21.2%">
                    <label for="inputPassword4" class="form-label fs-3">Từ khóa</label>
                    <div class="search">
                        <input type="text" class="form-control py-1 fs-2 " id="inputPassword4">
                        <i class="fa-solid fa-magnifying-glass"></i>
                    </div>
                </div>
            </form>
        </div>

        <div class="data " style="grid-gap:0">
            <div class="content-data p-0">
                <div class="head">
                    <div class="form-user">

                        <table id="customers">
                            <tr>
                                <th>STT</th>
                                <th>Tên khách hàng</th>
                                <th>Tên dịch vụ</th>
                                <th>Thời gian cấp</th>
                                <th>Hạn sử dụng</th>
                                <th>Trạng thái</th>
                                <th>Nguồn cấp</th>
                                <th>Nguồn cấp</th>
                            </tr>
                            <tr>
                                <td>Alfreds Futterkiste</td>
                                <td>Maria Anders</td>
                                <td>Germany</td>
                                <td>Germany</td>
                                <td>Germany</td>
                                <td>Germany</td>
                                <td>Germany</td>

                            </tr>

                        </table>



                    </div>
                </div>
            </div>
            <div class="content-add">

                <div class="add">
                    <a href="{{ route('nublevel.create') }}">
                        <div class="btn-add" style="padding: 2rem 1.4rem">
                            <img srcset="{{ asset('./assets/images/add-square.png 1x') }}">
                            <span>Cấp số mới</span>
                        </div>
                    </a>
                </div>

            </div>
        </div>
        </div>
        {{-- <div class="pagination">

        </div> --}}
    </main>
</section>
@endsection
