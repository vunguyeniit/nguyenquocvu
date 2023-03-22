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
                    <h2>Quản lý tài khoản</h2>
                </div>
                <div class="profile">
                    <div class="icon-bell">
                        <i class="fa-solid fa-bell"></i>
                    </div>
                    <div class="img_content">
                        <img srcset="{{ asset('./assets/images/user.png 2x') }}">*-
                        <div class="user-content">
                            <p>Xin Chào</p>
                            <h3>Lê Quỳnh Ái Vân</h3>
                        </div>
                    </div>

                </div>

            </div>

            <div class="title">
                <h2>Danh sách tài khoản</h2>
            </div>
        </nav>
    @endsection



    <main>

        <div class="select">
            <form action="" class="row me-3" style="    justify-content: space-between;">
                <div class="col-md-3">
                    <label for="inputEmail4" class="form-label fs-3">Tên vai trò</label>
                    <select class="form-select py-2 fs-3" aria-label="Default select example" id="status-account">
                        <option value="" selected>Tất cả</option>
                        <option value="0">Ngưng hoạt động</option>
                        <option value="1">Hoạt động</option>
                     
                    </select>
                </div>

                <div class="col-md-3"style="margin-left:7rem">
                    <label for="inputPassword4" class="form-label fs-3">Từ khóa</label>
                    <div class="search">
                        <input type="text" class="form-control py-1 fs-2 " id="inputPassword4">
                        <i class="fa-solid fa-magnifying-glass"></i>
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
                                <th>Tên đăng nhập</th>
                                <th>Họ tên</th>
                                <th>Số điện thoại</th>
                                <th>Email</th>
                                <th>Vai trò</th>
                                <th>Trạng thái hoạt đọng</th>
                                <th></th>

                            </tr>
                            <tbody id="tbody-account">
                            @foreach ($account as $item)
                            <tr>
                                <td>{{$item->username}}</td>
                                <td>{{$item->fullname}}</td>
                                <td>{{$item->phone}}</td>
                                <td>{{$item->email}}</td>
                                <td>{{$item->role}}</td>
                                @if ($item->status==0)
                                <td><i class="fa-solid fa-circle text-danger fs-6"></i>Ngưng hoạt động</td>
                                @else
                                <td><i class="fa-solid fa-circle text-success fs-6"> </i>hoạt động</td>
                                @endif
                               
                                <td><a href="{{route('account.edit',$item->id)}}">Cập nhật</a></td>
                              
                            </tr>
                            @endforeach
                        </tbody>
                        </table>



                    </div>
                </div>
        
            </div>
            <div class="content-add">

                <div class="add">
                    <a href="{{ route('account.create') }}">
                        <div class="btn-add">
                            <img srcset="{{ asset('./assets/images/add-square.png 1x') }}">
                            <span>Thêm tài khoản</span>
                        </div>
                    </a>
                </div>

            </div>
        </div>
        {{ $account->links()}}
        </div>

    </main>
</section>
@endsection
