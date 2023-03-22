@extends('layout.Clone-Admin')
@section('content')
    <section id="content">
    @section('header')
        <nav>
            <div class="header-right">
                <div class="header-left">
                    <h2 style="color: #7E7D88;margin-right:1rem">Thiết bị <i class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2>Danh sách dịch vụ</h2>
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
    <main style="pad: 0">

        <div class="data " style="grid-gap:0">
            <div class="content-data">
                <div class="head">
                    <div class="form-user text-center" style="height:60vh; padding:2rem 0">
                    <form action="{{route('nublevel.store')}}" method="POST">
                    @csrf

                        <h2 style="color: #FF9138; font-size:3rem">Cấp số mới</h2>
                        <p class="fs-2">Dịch vụ khách hàng lựa chọn</p>
                        <div class="col-md-5  d-inline-block">
                          
                            <select name="select-service" class="form-select py-2 fs-3" aria-label="Default select example">
                                <option selected>Chọn dịch vụ</option>
                                @foreach ($nub as $item)
                                <option value="{{$item->id}}">{{$item->servicename}}</option>     
                                @endforeach
                            </select>
                        </div>
                        <div class="btn mt-5">
                            <div class="btn_close">
                                <button class="fs-4"><a style="color: #FF9138"
                                        href="http://127.0.0.1:8000/admin/login">Hủy</a></button>
                            </div>
                            <div class="btn_login">
                                
                              
                                 <button type="submit">In số</button> 

                            </div>
                        </div>
                    </form>
                        {{-- model --}}

                        @if(session('success'))
                      
                      
                          <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="false">
                            <div class="modal-dialog modal-dialog-centered">
                              <div class="modal-content">
                                <div class="modal-header" style="border-bottom:none">
                                  {{-- <h5 class="modal-title" id="staticBackdropLabel">Modal title</h5> --}}
                                  <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">
                                  <h1>Số thứ tự được cấp</h1>
                                  <h3>{{$number->number_print}}</h3>
                                        <p>DV: {{$number->servicename}} (<b>tại quầy số 1</b>)</p> 
                                </div>
                                <div class="modal-footer d-block">
                                    <span class="d-block">Thời gian cấp: {{$number->grant_time}}</span>
                                    <span class="d-block">Hạn sử dụng: {{$number->expired}}</span>
                                  </div>
                               
                              </div>
                            </div>
                          </div>
                 
                  
                          @endif
                    
    
    

                      
                        {{-- endmodel --}}

                    </div>

                </div>
            </div>

        </div>
        </div>

    </main>
</section>
@endsection
