@extends('layout.Clone-Admin')
@section('content')
    <section id="content">
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
                <h2>Quản lí thiết bị</h2>
            </div>
        </nav>
    @endsection

    <style>

    </style>

    <main>

        <div class="select">

        </div>


        <div class="data">
            <div class="content-data"style="height:50rem">
                <div class="head">
                    <div class="form-user">
                        <h2 class="mb-5 fs-1" style="color: #FF9138">Thông tin thiết bị</h2>
                        <div class="row">

                            <div class="detail col-6">
                                <div class="col-3">
                                    <p>Mã thiết bị</p>
                                    <p>Tên thiết bị</p>
                                    <p>Địa chỉ</p>


                                </div>

                                <div class="col-3">
                                    <p>KIO_01</p>
                                    <p>Kiosk</p>
                                    <p>128.172.308</p>

                                </div>
                            </div>
                            <div class="detail col-6">
                                <div class="col-3">
                                    <p>Loại thiết bị</p>
                                    <p>Tên đăng nhập</p>
                                    <p>Mật khẩu</p>

                                </div>

                                <div class="col-3">
                                    <p>Kiosk</p>
                                    <p>Linhkyo001</p>
                                    <p>CMS</p>

                                </div>
                            </div>
                        </div>


                        <div class="col-12">
                            <p class="fs-4">Dịch vụ sử dụng :</p>
                            <p class="fs-4">Lorem ipsum dolor sit amet consectetur, adipisicing elit. Adipisci quod
                                quibusdam quasi

                            </p>
                        </div>

                    </div>
                </div>
            </div>


            <div class="content-add">

                <div class="add">
                    <a href="{{ route('device.create') }}">
                        <div class="btn-add">
                            <img srcset="{{ asset('./assets/images/add-square.png 1x') }}">
                            <span class="d-block">Cập nhật thiết bị</span>
                        </div>
                    </a>
                </div>

            </div>
        </div>



    </main>

</section>
@endsection
