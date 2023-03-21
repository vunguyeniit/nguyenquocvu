@extends('layout.Clone-Admin')
@section('content')
    <section id="content">
    @section('header')
        <nav>
            <div class="header-right">
                <div class="header-left">
                    <h2 style="color: #7E7D88;margin-right:1rem">Báo cáo <i class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2>Lập báo cáo</h2>
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
                    <label for="inputPassword4" class="form-label fs-3" style="visibility: hidden;">Chọn thời
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
                                <th>
                                    <div class="box-icon">
                                        <i class="fa-solid fa-caret-up fs-4"></i>
                                        <i class="fa-solid fa-caret-down fs-4"></i>
                                    </div>
                                    <div class="dropdown">
                                        Số thứ tự
                                    
                            
                                        <div class="dropdown-content">
                                            @forEach($report as $item)
                                            <a href="#home">{{$item->number_print}}</a>
                                            @endforeach
                                        </div>
                                    </div>
                                </th>

                                <th>

                                    <div class="box-icon">
                                        <i class="fa-solid fa-caret-up fs-4"></i>
                                        <i class="fa-solid fa-caret-down fs-4"></i>
                                    </div>
                                    <div class="dropdown">
                                        Tên dịch vụ

                                        <div class="dropdown-content">
                                           
                                            <a href="#home">Khám tim mạch</a>
                                            <a href="#home">Khám tổng quát</a>
                                            <a href="#home">Khám mắt</a>
                                            <a href="#home">Khám hô hấp</a>
                                          
                                           
                                        
                                        </div>
                                    </div>
                                </th>

                                <th>
                                    <div class="box-icon">
                                        <i class="fa-solid fa-caret-up fs-4"></i>
                                        <i class="fa-solid fa-caret-down fs-4"></i>
                                    </div>
                                    <div class="dropdown">
                                        Thời gian cấp

                                        <div class="dropdown-content">
                                            @forEach($report as $item)
                                            <a href="#home">{{$item->grant_time}}</a>
                                            @endforeach
                                           
                                        </div>
                                    </div>
                                </th>

                                <th>
                                    <div class="box-icon">
                                        <i class="fa-solid fa-caret-up fs-4"></i>
                                        <i class="fa-solid fa-caret-down fs-4"></i>
                                    </div>
                                    <div class="dropdown">
                                        Tình trạng

                                        <div class="dropdown-content">
                                          
                                          
                                            
                                            <a href="#home">Đang sử dụng</a>
                                            <a href="#home">Đang chờ</a>
                                            <a href="#home">Bỏ qua</a>
                                            
                                                
                                   
                                        </div>
                                    </div>
                                </th>

                                <th>
                                    <div class="box-icon">
                                        <i class="fa-solid fa-caret-up fs-4"></i>
                                        <i class="fa-solid fa-caret-down fs-4"></i>
                                    </div>
                                    <div class="dropdown">
                                        Nguồn cấp

                                        <div class="dropdown-content">
                                           
                                            <a href="#home">Kiosk</a>
                                            <a href="#home">Hệ thống</a>
                                       
                                           
                                        </div>
                                   
                            
                                    </div>
                                </th>







                            </tr>
                            @forEach($report as $item)
                            
                                <tr>
                                    <td>{{$item->number_print}}</td>
                                    <td>{{$item->servicename}}</td>
                                    <td>{{$item->grant_time}}</td>
                                    @if ($item->status==0) 
                                    <td><i class="fa-solid fa-circle text-secondary fs-6"></i> Đã sử dụng</td>
                                    @elseif($item->status==1)
                                    <td><i class="fa-solid fa-circle text-primary fs-6"></i> Đang chờ</td>
                                    @else 
                                     <td><i class="fa-solid fa-circle text-danger fs-6"></i> Bỏ qua</td>
                                     @endif 
                                    <td>{{$item->supply}}</td>
    
    
                                </tr>
                             @endforeach
                            
                           
                        </table>



                    </div>
                </div>
            </div>
            <div class="content-add">

                <div class="add">
                    <a href="{{ route('device.create') }}">
                        <div class="btn-add">
                            <img srcset="{{ asset('./assets/images/add-square.png 1x') }}">
                            <span class="d-block">Tải về</span>
                        </div>
                    </a>
                </div>

            </div>
        </div>
        </div>
        {{-- <div class="pagination">

        </div> --}}
    </main>
</section>
@endsection
