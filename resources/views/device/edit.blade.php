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
                    <h2> Cập nhật thiết bị</h2>
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
                        <form class="row gx-5 gy-3">



                            @csrf

                            <div class="col-md-6">
                                <label for="inputEmail4" class="form-label fs-3">Mã thiết bị</label>
                                <input type="text" class="form-control py-2 fs-3  fs-3" id="inputEmail4">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Loại thiết bị</label>
                                <select class="form-select py-2 fs-3" aria-label="Default select example">
                                    <option selected>Kiosk</option>
                                    <option value="1">One</option>
                                    <option value="2">Two</option>
                                    <option value="3">Three</option>
                                </select>
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Tên thiết bị</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Tên đăng nhập</label>
                                @php
                                    
                                @endphp
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Địa chỉ IP</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4">
                            </div>
                            <div class="col-md-6">
                                <label for="inputPassword4" class="form-label fs-3">Mật khẩu</label>
                                <input type="text" class="form-control py-2 fs-3" id="inputPassword4">
                            </div>
                            <div class="col-md-12">
                                <label for="inputPassword4" class="form-label fs-3">Dịch vụ sử dụng</label>
                                <input type="text" name="noidung" class="form-control py-2 fs-3" value=>

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
                <button type="submit">Cập nhật</button>
            </div>
        </div>
        {{-- <a style="color: #FF9138" href="{{route('device.edit',$id)}}">Hủy bỏ</a> --}}
    </main>

</section>
@endsection
