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
                        <img srcset="{{ asset('./assets/images/user.png 2x') }}">
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
            <div class="content-data"style="height:50rem">
                <div class="head">
                    <div class="form-user">
                        <h2 class="mb-5 fs-1" style="color: #FF9138">Thông tin thiết bị</h2>
                        <div class="row">

                            <div class="detail col-6">
                                <div class="col-3">
                                    <strong class="d-block fs-4 mb-4">Mã thiết bị:</strong>
                                    <strong class="d-block fs-4 mb-4">Tên thiết bị:</strong>
                                    <strong class="d-block fs-4 mb-4">Địa chỉ IP:</strong>
                                </div>
                                <div class="col-3">

                                    <p class="d-block fs-4 mb-4">{{ $devishow->devicecode }}</p>
                                    <p class="d-block fs-4 mb-4">{{ $devishow->devicename }}</p>
                                    <p class="d-block fs-4 mb-4">{{ $devishow->addressip }}</p>

                                </div>
                            </div>
                            <div class="detail col-6">
                                <div class="col-3">
                                    <strong class="d-block fs-4 mb-4">Loại thiết bị:</strong>
                                    <strong class="d-block fs-4 mb-4">Tên đăng nhập:</strong>
                                    <strong class="d-block fs-4 mb-4">Mật khẩu:</strong>

                                </div>

                                <div class="col-3">
                                    <p class="d-block fs-4 mb-4">{{ $devishow->devicetype }}</p>
                                    <p class="d-block fs-4 mb-4">{{ $devishow->username }}</p>
                                    <p class="d-block fs-4 mb-4">{{ $devishow->password }}</p>
                                </div>
                            </div>
                        </div>


                        <div class="col-12">
                            <strong class="d-block fs-4 mb-4">Dịch vụ sử dụng:</strong>
                            @foreach ($devishow->tags1 as $tagItem)
                                <p class="d-inline-block fs-4 mb-4">{{ $tagItem->device_service . ',' }}</p>
                            @endforeach


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
