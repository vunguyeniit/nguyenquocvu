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
                    <h2 style="color: #7E7D88;margin-right:1rem"> Quản lý vai trò <i
                            class="fa-solid fa-chevron-right fs-4"></i></h2>

                    <h2>Cập nhật vai trò</h2>
                </div>
                <div class="profile">
                    <div class="icon-bell">
                        <i class="fa-solid fa-bell"></i>
                    </div>
                    @include('admin.user')

                </div>

            </div>

            <div class="title">
                <h2>Dach sách vai trò</h2>
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
                        <h2 class="mb-5 fs-1" style="color: #FF9138">Thông tin vai trò</h2>

                        <div class="row">
                            <div class="col-md-6">
                                <label style="font-weight: 600;" for="inputEmail4" class="form-label fs-3">Tên vai trò
                                    *</label>
                            </div>
                            <div class="col-md-6">
                                <label style="font-weight: 600;" for="inputEmail4" class="form-label fs-3">Phân quyền
                                    chức năng *</label>
                            </div>
                        </div>
                        <form class="row gx-5 gy-3" action="{{route('role.update',$role->id)}}" method="POST">

                            @csrf

                            <div class="col-md-6">
                                {{-- <label for="inputEmail4" class="form-label fs-3">Thông tin vai trò</label> --}}
                                <input type="text" class="form-control py-2 fs-3  fs-3" id="inputEmail4"
                                    placeholder="Nhập mã thiết bị"name="rolename" value="{{$role->rolename}}">

                                <label for="inputPassword4"
                                    class="form-label fs-3 d-block mt-4 "style="font-weight: 600;">Mô
                                    tả</label>
                                <textarea style=" resize: none;ouline:none;outline: none;
                                border: 1.5px solid #D4D4D7;"
                                    id="w3review" name="description" rows="7" cols="92">{{$role->description}} </textarea>
                                <p class="fs-4">* Là trường thông tin bắt buộc</p>

                            </div>

                            <div class="col-md-6  p-5"style="background-color: #FFF2E7;">


                                <div class="check-content">
                                    <div class="check-input">
                                        <h3 class="pb-3 fs-2" style="color: #FF9138">Nhóm chức năng A</h3>
                                        <div class="form-check mb-4">

                                            <input class="form-check-input" name="checkbox_a" type="checkbox" 
                                                id="flexCheckDefault"{{$role->permission==0 ? 'checked':''}}>

                                            <label class="form-check-label " for="flexCheckDefault">
                                                Tất cả
                                            </label>

                                        </div>
                                        <div class="form-check mb-4">
                                            <input class="form-check-input" name="checkbox_a" type="checkbox" 
                                                id="flexCheckChecked"{{$role->permission==0 ? 'checked':''}}>
                                            <label style="margin-right:12.2rem" class="form-check-label"
                                                for="flexCheckChecked">
                                                Chức năng x
                                            </label>

                                        </div>
                                        <div class="form-check mb-4">
                                            <input class="form-check-input" type="checkbox" name="checkbox_a"
                                                id="flexCheckChecked"{{$role->permission==0 ? 'checked':''}}>
                                            <label style="margin-right:12.2rem" class="form-check-label"
                                                for="flexCheckChecked">
                                                Chức năng y
                                            </label>

                                        </div>
                                        <div class="form-check mb-4">
                                            <input class="form-check-input" type="checkbox"name="checkbox_a" 
                                                id="flexCheckChecked"{{$role->permission==0 ? 'checked':''}}>
                                            <label class="form-check-label" for="flexCheckChecked">
                                                Chức năng z
                                            </label>
                                        </div>

                                    </div>

                                </div>



                                <h3 class="pb-3 pt-5 fs-2" style="color: #FF9138">Nhóm chức năng B</h3>
                                <div class="check-content">
                                    <div class="check-input">
                                        <div class="form-check mb-4">
                                            <input class="form-check-input" type="checkbox" name="checkbox_b"
                                                id="flexCheckDefault"{{$role->permission==1 ? 'checked':''}}>
                                            <label class="form-check-label " for="flexCheckDefault" >
                                                Tất cả
                                            </label>

                                        </div>
                                        <div class="form-check mb-4">
                                            <input class="form-check-input" type="checkbox"name="checkbox_b" 
                                                id="flexCheckChecked"{{$role->permission==1 ? 'checked':''}}>
                                            <label style="margin-right:12.2rem" class="form-check-label"
                                                for="flexCheckChecked">
                                                Chức năng x
                                            </label>

                                        </div>
                                        <div class="form-check mb-4">
                                            <input class="form-check-input" type="checkbox"name="checkbox_b" 
                                                id="flexCheckChecked"{{$role->permission==1 ? 'checked':''}}>
                                            <label style="margin-right:12.2rem" class="form-check-label"
                                                for="flexCheckChecked">
                                                Chức năng y
                                            </label>

                                        </div>
                                        <div class="form-check mb-4">
                                            <input class="form-check-input" type="checkbox"name="checkbox_b" 
                                                id="flexCheckChecked"{{$role->permission==1 ? 'checked':''}}>
                                            <label class="form-check-label" for="flexCheckChecked">
                                                Chức năng z
                                            </label>
                                        </div>

                                    </div>

                                </div>

                            </div>
                    </div> 
                </div>
            </div>
        </div>
        <div class="btn mt-2">
            <div class="btn_close">
                <button class="fs-4"><a style="color: #FF9138" href="{{ route('admin.login') }}">Hủy
                        bỏ</a></button>
            </div>
            <div class="btn_login">
                <button type="submit">Thêm</button>
            </div>
        </div>
        @method("PUT");
    </form>
    </main>
</section>
@endsection
