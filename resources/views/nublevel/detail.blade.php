@extends('layout.Clone-Admin')
@section('content')
    <section id="content">
    @section('header')
        <nav>
            <div class="header-right">
                <div class="header-left">
                    <h2 style="color: #7E7D88;margin-right:1rem">Thiết bị <i class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2 style="color: #7E7D88;margin-right:1rem"> Danh sách cấp số <i
                            class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2>Chi tiết</h2>
                </div>
                <div class="profile">
                    <div class="icon-bell">
                        <i class="fa-solid fa-bell"></i>
                    </div>
                    @include('admin.user')

                </div>

            </div>

            <div class="title">
                <h2>Quản lí cấp số</h2>
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
                        <h2 class="mb-5 fs-1" style="color: #FF9138">Thông tin cấp số</h2>
                        <div class="row">

                            <div class="detail col-6">
                                <div class="col-3 ">

                                    <strong class="d-block fs-4 mb-4">Họ và tên:</strong>
                                    <strong class="d-block fs-4 mb-4">Tên dịch vụ:</strong>
                                    <strong class="d-block fs-4 mb-4">Số thứ tự:</strong>
                                    <strong class="d-block fs-4 mb-4">Thời gian cấp:</strong>
                                    <strong class="d-block fs-4 mb-4">Hạn sử dụng:</strong>


                                </div>
f
                                <div class="col-4">
                                    
                                    <p class="d-block fs-4 mb-4">{{$number->fullname}}</p>
                                    <p class="d-block fs-4 mb-4">{{$number->servicename}}</p>
                                    <p class="d-block fs-4 mb-4">{{$number->number_print}}</p>
                                    <p class="d-block fs-4 mb-4">{{$number->grant_time}}</p>
                                    <p class="d-block fs-4 mb-4">{{$number->expired}}</p>

                                </div>
                            </div>
                            <div class="detail col-6">
                                <div class="col-3">

                                    <strong class="d-block fs-4 mb-4">Nguồn cấp:</strong>
                                    <strong class="d-block fs-4 mb-4">Trạng thái:</strong>
                                    <strong class="d-block fs-4 mb-4">Số thứ tự:</strong>
                                    <strong class="d-block fs-4 mb-4">Email:</strong>

                                </div>

                                <div class="col-4">

                                    <p class="d-block fs-4 mb-4">{{$number->supply}}</p>
                                    @if ($number->status==0) 
                                    <p class="d-block fs-4 mb-4"><i class="fa-solid fa-circle text-secondary fs-6"></i> Đã sử dụng</p>

                                    @elseif($number->status==1)
       
                                    <p class="d-block fs-4 mb-4"><i class="fa-solid fa-circle text-primary fs-6"></i> Đang chờ</p>
                                    @else 
                                  
                                     <p class="d-block fs-4 mb-4"><i class="fa-solid fa-circle text-danger fs-6"></i> Bỏ qua</p>
                                     @endif 
                                  
                                    <p class="d-block fs-4 mb-4">{{$number->phone}}</p>
                                    <p class="d-block fs-4 mb-4">{{$number->email}}</p>


                                </div>
                            </div>
                        </div>




                    </div>
                </div>
            </div>


            <div class="content-add">

                <div class="add">
                    <a href="{{ route('device.create') }}">
                        <div class="btn-add">
                            <img srcset="{{ asset('./assets/images/add-square.png 1x') }}">
                            <span class="d-block">Quay lại</span>
                        </div>
                    </a>
                </div>

            </div>
        </div>



    </main>

</section>
@endsection
