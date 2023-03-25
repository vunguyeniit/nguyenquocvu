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
                    <h2>Quản lý vai trò</h2>
                </div>
                <div class="profile">
                    <div class="icon-bell">
                        <i class="fa-solid fa-bell"></i>
                    </div>
                    @include('admin.user')
                </div>

            </div>

            <div class="title">
                <h2>Danh sách vai trò</h2>
            </div>
        </nav>
    @endsection



    <main>

        <div class="select">
            <form action="{{route('role.index')}}" method="GET" class="row me-3" style="justify-content: flex-end">
@csrf
                <div class="col-md-3">
                    <label for="inputPassword4" class="form-label fs-3">Từ khóa</label>
                    <div class="search">
                        <input type="text" class="form-control py-1 fs-2 " id="inputPassword4" name="search">
                        <button style="border: none;background-color: #ddd9d9;" type="submit"><i  class="fa-solid fa-magnifying-glass"></i></button>
                    </div>
                </div>
            </form>
        </div>

        <div class="data">
            <div class="content-data p-0">
                <div class="head">
                    <div class="form-user">

                        <table id="customers">
                            <tr>
                                <th>Tên vai trò</th>
                                <th>Số người dùng</th>
                                <th>Mô tả</th>
                                <th></th>

                            </tr>
                            @foreach ($role as $item)
                                
                          
                            <tr>
                                <td>{{$item->rolename}}</td>
                                <td>{{$item->member}}</td>
                                <td>{{$item->description}}</td>
                                <td><a href="{{route('role.edit',$item->id)}}">Cập nhật</a></td>


                            </tr>
                            @endforeach
                        </table>



                    </div>
                </div>
            </div>
            <div class="content-add">

                <div class="add">
                    <a href="{{ route('role.create') }}">
                        <div class="btn-add">
                            <img srcset="{{ asset('./assets/images/add-square.png 1x') }}">
                            <span>Thêm vai trò</span>
                        </div>
                    </a>
                </div>

            </div>
        </div>
        </div>

    </main>
</section>
@endsection
