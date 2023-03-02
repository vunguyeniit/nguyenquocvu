@extends('layout.Clone-Admin')

@section('content')
    <section id="content">
        {{-- @include('admin.header') --}}
    @section('header')
        <nav>
            <div class="header-right">
                <div class="header-left">
                    <h2 style="color: #7E7D88;margin-right:1rem">Cài đặt hệ thống <i
                            class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2>Quản lý tài khoản</h2>
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
                <h2>Danh sách tài khoản</h2>
            </div>
        </nav>
    @endsection



    <main>

        <div class="select">
            <form action="" class="row me-3" style="    justify-content: space-between;">
                <div class="col-md-3">
                    <label for="inputEmail4" class="form-label fs-3">Tên vai trò</label>
                    <select class="form-select py-2 fs-3" aria-label="Default select example">
                        <option selected>Tất cả</option>
                        <option value="2">Hoạt động</option>
                        <option value="3">Ngưng hoạt động</option>
                    </select>
                </div>

                <div class="col-md-3"style="margin-left:7rem">
                    <label for="inputPassword4" class="form-label fs-3">Từ khóa</label>
                    <div class="search">
                        <input type="text" class="form-control py-1 fs-2 " id="inputPassword4">
                        <i class="fa-solid fa-magnifying-glass"></i>
                    </div>
                </div>
            </form>
        </div>

        <div class="data">
            <div class="content-data p-0">
                <div class="head">
                    <div class="form-user">

                        <table id="customers">
                            <tr>
                                <th>Mã thiết bị</th>
                                <th>Tên thiết bị</th>
                                <th>Địa chỉ IP</th>
                                <th>Trạng thái hoạt động</th>
                                <th>Trạng thái kết nối</th>
                                <th>Dich vụ sử dụng</th>
                            </tr>
                            <tr>
                                <td>Alfreds Futterkiste</td>
                                <td>Maria Anders</td>
                                <td>Germany</td>
                                <td>Germany</td>
                                <td>Germany</td>
                                <td>Germany</td>

                            </tr>

                            <tr>
                                <td>Alfreds Futterkiste</td>
                                <td>Maria Anders</td>
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
                    <a href="{{ route('device.create') }}">
                        <div class="btn-add">
                            <img srcset="{{ asset('./assets/images/add-square.png 1x') }}">
                            <span>Thêm thiết bị</span>
                        </div>
                    </a>
                </div>

            </div>
        </div>
        </div>

    </main>
</section>
@endsection
