@extends('layout.Clone-Admin')


@section('content')
    <section id="content">
        {{-- @include('admin.header') --}}
    @section('header')
        <nav>
            <div class="header-right">
                <div class="header-left">
                    <h2 style="color: #7E7D88;margin-right:1rem">Dịch vụ <i class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2 style="color: #7E7D88;margin-right:1rem"> Danh sách dịch vụ <i
                            class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2 style="color: #7E7D88;margin-right:1rem">Chi tiết <i class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2>Cập nhật</h2>
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
                <h2>Quản lý dịch vụ</h2>
            </div>
        </nav>
    @endsection
    <main>
        <div class="select">
        </div>

        <div class="data">
            <div class="content-data">
                <div class="head d-block">
                    <div class="form-user">
                        <h2 class="mb-5 fs-1" style="color: #FF9138">Thông tin dịch vụ</h2>
                        <form class="row gx-5 gy-3" method="POST" action="{{route('service.update',$service->id)}}" >
                            @csrf
                            <div class="col-md-6">
                                <label for="inputEmail4" class="form-label fs-3">Mã dịch vụ</label>
                                <input type="text"  value="{{$service->servicecode}}"class="form-control py-2 fs-3  fs-3" id="inputEmail4"
                                    placeholder="Nhập mã thiết bị" name ="servicecode">

                                <label for="inputEmail4" class="form-label fs-3">Tên dịch vụ</label>
                                <input type="text" value="{{$service->servicename}}" class="form-control py-2 fs-3  fs-3"
                                    placeholder="Nhập mã thiết bị" name ="servicename" >

                            </div>
                            <div class="col-md-6">
                                <div class="col-md-3">
                                    <label for="inputPassword4" class="form-label fs-3 d-block ">Mô tả</label>
                                    <textarea style="resize: none; outline: none;font-size:2rem" id="w3review" name="description" rows="5" cols="50">  {{$service->description}}</textarea>
                                </div>
                            </div>
                    </div>

                    <div class="title d-block my-5" style="color: #FF9138">
                        <h2 class="">Quy tắc cấp số</h2>
                    </div>
                    <div class="check-content">
                        <div class="check-input">
                            <div class="form-check mb-5">
                                <input class="form-check-input" type="checkbox" name="checkbox"id="flexCheckDefault">
                                <label class="form-check-label me-5" for="flexCheckDefault">
                                    Tăng tự động từ
                                </label>
                                <span>0001</span>
                                <p class="d-inline fs-3 mx-3">đến</p>
                                <span>9999</span>
                            </div>
                            <div class="form-check mb-5">
                                <input class="form-check-input" type="checkbox" value="" id="flexCheckChecked">
                                <label style="margin-right:10.4rem" class="form-check-label" for="flexCheckChecked">
                                    Prefix
                                </label>
                                <span>0001</span>
                            </div>
                            <div class="form-check mb-5">
                                <input class="form-check-input" type="checkbox" value="" id="flexCheckChecked">
                                <label style="margin-right:10.4rem" class="form-check-label" for="flexCheckChecked">
                                    Surfix
                                </label>
                                <span>0001</span>
                            </div>
                            <div class="form-check mb-5">
                                <input class="form-check-input" type="checkbox" value="" id="flexCheckChecked">
                                <label class="form-check-label" for="flexCheckChecked">
                                    Reset mỗi ngày
                                </label>
                            </div>

                        </div>

                    </div>
                    <div class="col-md-3">
                        <p class="fs-4">* Là trường thông tin bắt buộc</p>
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
            @method('PUT');
        </form>
        </div>
    </main>
</section>
@endsection
