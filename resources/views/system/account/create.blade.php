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
                        <form class="row gx-5 gy-3">

                            @csrf

                            <div class="col-md-6">
                                <label for="inputEmail4" class="form-label fs-3">Họ và tên</label>
                                <input type="text" class="form-control py-2 fs-3  fs-3" id="inputEmail4"
                                    placeholder="Nhập họ và tên">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3 ">Tên đăng nhập</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập tên đăng nhập">

                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Số điện thoại</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập số điện thoại">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Mật khẩu</label>

                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập mật khẩu">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Email</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập email">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Nhập lại mật khẩu</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập lại mật khẩu">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Vai trò</label>
                                <select class="form-select py-2 fs-3 opacity-75" aria-label="Default select example">

                                    <option selected>Chọn vai trò </option>

                                    <option value="2">Kế toán</option>
                                    <option value="3">Quản lý</option>
                                    <option value="3">Admin</option>
                                </select>
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Tình trạng</label>
                                <select class="form-select py-2 fs-3 opacity-75" aria-label="Default select example">
                                    <option selected>Hoạt động </option>
                                    <option value="2">Ngưng hoạt đọng</option>
                                    <option value="3">Hoạt động</option>
                                </select>
                            </div>

                            <div class="col-md-3">
                                <p class="fs-4">* Là trường thông tin bắt buộc</p>
                            </div>
                        </form>
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
    </main>
</section>
@endsection
