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
                    @include('admin.user')

                </div>

            </div>

            <div class="title">
                <h2>Quản lý thiết bị</h2>
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
                        <form class="row gx-5 gy-3" method="POST" action="{{ route('device.store') }}">

                            @csrf

                            <div class="col-md-6">
                                <label for="" class="form-label fs-3">Mã thiết bị</label>
                                <input type="text" class="form-control py-2 fs-3  fs-3" name="devicecode"
                                    placeholder="Nhập mã thiết bị">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3 ">Loại thiết bị</label>
                                <select class="form-select py-2 fs-3 opacity-75" aria-label="Default select example"
                                    name="devicetype">
                                    <option selected>Chọn loại thiết bị </option>
                                    <option value="Kiosk">Kiosk</option>
                                    <option value="Display counter">Display counter</option>
                                </select>
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Tên thiết bị</label>
                                <input type="text" class="form-control py-2 fs-3" name="devicename"
                                    placeholder="Nhập tên thiết bị">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Tên đăng nhập</label>

                                <input type="text" class="form-control py-2 fs-3" name="username"
                                    placeholder="Nhập tên đăng nhập">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Địa chỉ IP</label>
                                <input type="text" class="form-control py-2 fs-3" name="addressip"
                                    placeholder="Nhập tên địa chỉ IP">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Mật khẩu</label>
                                <input type="password" class="form-control py-2 fs-3" name="password"
                                    placeholder="Nhập mật khẩu">
                            </div>
                            <div class="col-md-12 fs-1">
                                <label for="inputPassword4" class="form-label fs-3">Dịch vụ sử dụng</label>
                                <select id="js-select2" class="form-select py-2 fs-1 opacity-75"
                                    aria-label="Default select example" name="tags[]" multiple>
                                    {{-- <option class="fs-1" value="Khám tim mạch">Khám tim mạch</option>
                                    <option value="Khám sản phụ khoa">Khám sản phụ khoa</option>
                                    <option value="Khám răng hàm mặt">Khám răng hàm mặt</option>
                                    <option value="Khám tai mũi họng">Khám tai mũi họng</option>
                                    <option value="Khám hô hấp">Khám hô hấp</option>
                                    <option value="Khám tổng quát">Khám tổng quát</option> --}}

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
                <button class="fs-4"><a style="color: #FF9138" href="#">Hủy bỏ</a></button>
            </div>
            <div class="btn_login">
                <button type="submit">Thêm thiết bị</button>
            </div>
        </div>
        </form>
    </main>
</section>

@endsection
