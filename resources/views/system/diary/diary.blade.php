@extends('layout.Clone-Admin')

@section('content')
    <section id="content">
        {{-- @include('admin.header') --}}
    @section('header')
        <nav>
            <div class="header-right">
                <div class="header-left">
                    <h2 style="color: #7E7D88;margin-right:1rem">Thiết bị <i class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2>Nhật ký hoạt động</h2>
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


        </nav>
    @endsection



    <main>

        <div class="select">
            <form action="" class="row">

                <div class="col-md-2">
                    <label for="inputPassword4" class="form-label fs-3">Chọn thời gian</label>
                <div class="input-group date" id="datepicker">
                        <span class="input-group-append">
                            <span class="input-group-text bg-light d-block">
                                <i class="fa fa-calendar"></i>
                            </span>
                            </span>
                         <input type="text" class="form-control py-1 fs-2 " id="inputPassword4" id="date">
                
                        
                    </div>
                </div>
                <div class="col-md-2">
                    <label for="inputPassword4" class="form-label fs-3" style="visibility: hidden">Chọn thời
                        gian</label>
                <div class="input-group date" id="datepicker">
                        <span class="input-group-append">
                            <span class="input-group-text bg-light d-block">
                                <i class="fa fa-calendar"></i>
                            </span>
                            </span>
                         <input type="text" class="form-control py-1 fs-2 " id="inputPassword4" id="date">
                
                        
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
                                <th>Tên dăng nhập</th>
                                <th>Thời gian tác động</th>
                                <th>IP thực hiện</th>
                                <th>Thao tác thực hiện</th>

                            </tr>
                            @foreach ($diary as $item)
                            <tr>
                                <td>{{ $item->username}}</td>
                                <td>{{ $item->usetime}}</td>
                                <td>{{ $item->ip}}</td>
                                <td>{{ $item->perform}}</td>


                            </tr>
                            @endforeach
                          

                        </table>



                    </div>
                </div>
            </div>

        </div>
        </div>
        {{-- <div class="pagination">

        </div> --}}
    </main>
</section>
@endsection
