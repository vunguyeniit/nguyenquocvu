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

        {{-- <div class="select">

        </div> --}}

        <div class="data " style="grid-gap:0">
            <div class="content-data">
                <div class="head">
                    <div class="form-user text-center" style="height:60vh; padding:2rem 0">

                        <h2 style="color: #FF9138; font-size:3rem">Cấp số mới</h2>
                        <p class="fs-2">Dịch vụ khách hàng lựa chọn</p>
                        <div class="col-md-5  d-inline-block">
                            {{-- <label for="inputEmail4" class="form-label fs-3">Tên dịch vụ</label> --}}
                            <select class="form-select py-2 fs-3" aria-label="Default select example">
                                <option selected>Chọn dịch vụ</option>
                                <option value="0">Khám tim mạch</option>
                                <option value="1">Khám sản phụ khoa</option>
                                <option value="2">Khám răng hàm mặt</option>
                                <option value="3">Khám tai mũi họng</option>
                            </select>
                        </div>
                        <div class="btn mt-5">
                            <div class="btn_close">
                                <button class="fs-4"><a style="color: #FF9138"
                                        href="http://127.0.0.1:8000/admin/login">Hủy</a></button>
                            </div>
                            <div class="btn_login">
                                <button type="submit">In số</button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
        </div>

    </main>
</section>
@endsection
