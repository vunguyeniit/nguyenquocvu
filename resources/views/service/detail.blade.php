@extends('layout.Clone-Admin')
@section('content')
    <section id="content">
        {{-- @include('admin.header') --}}
    @section('header')
        <nav>
            <div class="header-right">
                <div class="header-left">
                    <h2 style="color: #7E7D88;margin-right:1rem">Dịch vụ <i class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2 style="color: #7E7D88;margin-right:1rem">Danh sách dịch vụ <i
                            class="fa-solid fa-chevron-right fs-4"></i></h2>

                    <h2>Chi tiết</h2>
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


        <div class="data" style="grid-gap: 0px">

            <div class="content-data"style="flex-basis: 140px">
                <div class="head d-block">


                    <div class="title d-block my-5" style="color: #FF9138">
                        <h2 class="">Thông tin dịch vụ</h2>
                    </div>
                    <div class="check-content">
                        <div class="check-input">
                            <div class="form-check mb-5 p-0">

                                <label class="form-check-label"style="margin-right:5rem" for="flexCheckDefault">
                                    Mã dịch vụ
                                </label>
                         
                          
                            
                                <p class="fs-4 d-inline">{{$ordinal->servicecode}}</p>

                            </div>
                            <div class="form-check mb-5 p-0">

                                <label style="margin-right:4rem" class="form-check-label" for="flexCheckChecked">
                                    Tên dịch vụ
                                </label>
                                <p class="fs-4 d-inline">{{$ordinal->servicename}}</p>

                            </div>

                            <div class="form-check mb-5 p-0">
                                <label style="margin-right:7rem" class="form-check-label" for="flexCheckChecked">
                                    Mô tả
                                </label>
                                <p class="fs-4 d-inline">{{$ordinal->description}}</p>
                              
                            </div>

                        </div>

                    </div>


                    <div class="title d-block mb-5" style="color: #FF9138">
                        <h2 class="">Quy tắc cấp số</h2>
                    </div>
                    <div class="check-content">
                        <div class="check-input">
                            <div class="form-check mb-5 p-0">

                                <label class="form-check-label me-2" for="flexCheckDefault">
                                    Tăng tự động từ
                                </label>
                                <span>0001</span>
                                <p class="d-inline fs-4 mx-3">đến</p>
                                <span>9999</span>
                            </div>
                            <div class="form-check mb-5 p-0">

                                <label style="margin-right:7.9rem" class="form-check-label" for="flexCheckChecked">
                                    Prefix
                                </label>
                                <span>0001</span>
                            </div>

                            <div class="form-check mb-5 p-0">

                                <label class="form-check-label" for="flexCheckChecked">
                                    Reset mỗi ngày
                                </label>
                            </div>

                            <div class="form-check mb-5 p-0">

                                <label class="form-check-label" for="flexCheckChecked">
                                    Ví dụ 201 - 2001
                                </label>
                            </div>

                        </div>

                    </div>
                </div>
            </div>

            <div class="content-data">
                <div class="head d-block">

                    <div class="select">
                        <form action="{{route('service.show', $ordinal->id)}}" method="GET" class="row mb-4" style="flex-wrap: nowrap;">
                            @csrf
                            <div class="col-md-3">
                                <label for="inputEmail4" class="form-label fs-3">Trạng thái</label>
                                <select class="form-select py-2 fs-4" aria-label="Default select example"id="status-detail">
                                    <option value="" selected>Tất cả</option>
                                    <option value="0"> Đã hoàn thành</option>
                                    <option value="1"> Đang thực hiện</option>
                                    <option value="2"> Vắng</option>
                                </select>
                            </div>
                            <div class="col-md-3">
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
                            <div class="col-md-3">
                                <label style=" visibility: hidden;"type="" for="inputPassword4"
                                    class="form-label fs-3">Chọn thời
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
                            <div class="col-md-3"style="">
                                <label for="inputPassword4" class="form-label fs-3">Từ khóa</label>
                                <div class="search">
                                    <input type="text" class="form-control py-1 fs-2 " id="inputPassword4"name="search_deatil">
                                    <button style="border: none;background-color: #ddd9d9;" type="submit"><i  class="fa-solid fa-magnifying-glass"></i></button>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div class="form-user">
                        <table id="customers">
                            <tr>
                                <th>Số thứ tự</th>
                                <th>Trạng thái</th>
                            </tr>
                            <tbody id="tbody-detail">
                                @foreach ($paginate as $item)
                            <tr>
                               <td>{{$item->number}}</td>
                                @if ($item->status==0) 
                               <td><i class="fa-solid fa-circle text-success fs-6"></i> Đã hoàn thành</td>
                               @elseif($item->status==1)
                               <td><i class="fa-solid fa-circle text-primary fs-6"></i> Đang thực hiện</td>
                               @else 
                                <td><i class="fa-solid fa-circle text-secondary fs-6"></i> Vắng</td>
                            </tr>
    
                                @endif 
                               @endforeach
                        </tbody>
                        </table>
                   
                            {{ $paginate->links()}}
                      
                    </div>
                </div>
            </div>



            <div class="content-add">
                <div class="add">
                    <a href="{{ route('device.create') }}">
                        <div class="btn-add">
                            <img srcset="{{ asset('./assets/images/add-square.png 1x') }}">
                            <span>Thêm thiết bị</span>
                        </div>
                    </a>
                </div>

                <div class="add" style="top: 9.8rem;">
                    <a href="{{ route('device.create') }}">
                        <div class="btn-add">
                            <img srcset="{{ asset('./assets/images/add-square.png 1x') }}">
                            <span>Thêm thiết bị</span>
                        </div>
                    </a>
                </div>
            </div>
        </div>
        </div>
    </main>
    
</section>
@endsection
