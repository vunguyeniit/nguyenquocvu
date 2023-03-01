@extends('layout.Clone-Admin')


@section('content')
    <section id="content">
        {{-- @include('admin.header') --}}
    @section('header')
        <nav>
            <div class="header-right">
                <div class="header-left">
                    <h2 style="color: #7E7D88;margin-right:1rem">Thiết bị <i class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2 style="color: #7E7D88;margin-right:1rem"> Danh sách thiết bị <i
                            class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2> Thêm thiết bị</h2>
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
                <h2>Quản lí thiết bị</h2>
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
                        <h2 class="mb-5 fs-1" style="color: #FF9138">Thông tin thiết bị</h2>
                        <form class="row gx-5 gy-3">

                            @csrf

                            <div class="col-md-6">
                                <label for="inputEmail4" class="form-label fs-3">Mã thiết bị</label>
                                <input type="text" class="form-control py-2 fs-3  fs-3" id="inputEmail4"
                                    placeholder="Nhập mã thiết bị">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3 ">Loại thiết bị</label>
                                <select class="form-select py-2 fs-3 opacity-75" aria-label="Default select example">
                                    <option selected>Chọn loại thiết bị </option>
                                    <option value="2">Kiosk</option>
                                    <option value="3">Display counter</option>
                                </select>
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Tên thiết bị</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập tên thiết bị">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Tên đăng nhập</label>

                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập tên đăng nhập">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Địa chỉ IP</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập tên địa chỉ IP">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Mật khẩu</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập mật khẩu">
                            </div>
                            <div class="col-md-12">
                                <label for="inputPassword4" class="form-label fs-3">Dịch vụ sử dụng</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4"
                                    placeholder="Nhập dịch vụ sử dụng">
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
                <button type="submit">Thêm thiết bị</button>
            </div>
        </div>
    </main>
</section>
@endsection
