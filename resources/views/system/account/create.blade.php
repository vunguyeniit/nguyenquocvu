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
                    <h2 style="color: #7E7D88;margin-right:1rem"> Quản lý tài khoản<i
                            class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2> Thêm tài khoản</h2>
                </div>
                <div class="profile">
                    <div class="icon-bell">
                        <i class="fa-solid fa-bell"></i>
                    </div>
                    @include('admin.user')

                </div>

            </div>

            <div class="title">
                <h2>Quản lý tài khoản</h2>
            </div>
        </nav>
    @endsection
    <main>
        <div class="select">
        </div>

        <div class="data">
            <div class="content-data">
                <div class="head">
                    <div class="form-user">
                        <h2 class="mb-5 fs-1" style="color: #FF9138">Thông tin tài khoản</h2>
                        <form class="row gx-5 gy-3" method="POST" action="{{route('account.store')}}">

                            @csrf

                            <div class="col-md-6">
                                <label for="inputEmail4" class="form-label fs-3">Họ và tên</label>
                                <input type="text" class="form-control py-2 fs-3  fs-3" id="inputEmail4"
                                    placeholder="Nhập họ và tên"name='fullname'>
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3 ">Tên đăng nhập</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập tên đăng nhập"name='username'>

                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Số điện thoại</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập số điện thoại"name='phone'>
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Mật khẩu</label>

                                <input type="password" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập mật khẩu"name='password'>
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Email</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập email"name='email'>
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Nhập lại mật khẩu</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập lại mật khẩu"name='confirm_password'>
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Vai trò</label>
                                <select class="form-select py-2 fs-3 opacity-75" aria-label="Default select example" name='role'>
                                    <option selected>Chọn vai trò </option>
                                    <option value="Kế toán">Kế toán</option>
                                    <option value="Quản lý">Quản lý</option>
                                    <option value="Admin">Admin</option>
                                </select>
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3" >Tình trạng</label>
                                <select class="form-select py-2 fs-3 opacity-75" aria-label="Default select example"name='status'>
                                    <option selected>Hoạt động</option>
                                    <option value="0">Ngưng hoạt động</option>
                                    <option value="1">Hoạt động</option>
                                </select>
                            </div>
                            <div class="col-md-3">
                                <p class="fs-4">* Là trường thông tin bắt buộc</p>
                            </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="btn mt-2">
            <div class="btn_close">
                <button class="fs-4"><a style="color: #FF9138" href="{{ route('admin.login') }}">Hủy bỏ</a></button>
            </div>
            <div class="btn_login">
                <button type="submit">Thêm</button>
            </div>
        </div>
    </form>
    </main>
</section>
@endsection
