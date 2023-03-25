@extends('layout.Clone-Admin')
@section('content')
<section id="content">
{{-- @include('admin.header') --}}
@section('header')
<nav>
  <div class="header-right">
 <div class="header-left">
  <h2>Thông Tin Cá Nhân</h2>
 </div>

  <div class="profile">
    <div class="icon-bell">
      <i class="fa-solid fa-bell"></i>
    </div>
    @include('admin.user')
 
  </div>
  
  </div>
</nav>
@endsection
  <main>

    <div class="data">
      <div class="content-data">
        <div class="head">
          <div class="menu">
            <img src="{{asset('assets/images/user2.png')}}">
            <h2>Lê Quỳnh Ái Vân</h2>
            <div class="camera">
              <img src="{{asset('assets/images/camera.png')}}">
            </div>
          </div>

          <div class="form-user">
            <fieldset disabled >
            <form class="row gx-5 gy-3">
            
          
                  
            @csrf

              <div class="col-md-6">
                <label for="inputEmail4" class="form-label fs-3">Tên người dùng</label>
                <input type="text"  class="form-control py-3 fs-3  fs-3 opacity-75" id="inputEmail4" value="{{$user->username}}">
              </div>
              <div class="col-md-6">
                <label for="inputPassword4" class="form-label fs-3">Tên đăng nhập</label>
                <input type="text" class="form-control py-3 fs-3 opacity-75" id="inputPassword4" value="{{$user->loginname}}">
              </div>
              <div class="col-md-6">
                <label for="inputPassword4" class="form-label fs-3">Số điện thoại</label>
                <input type="text" class="form-control py-3 fs-3 opacity-75" id="inputPassword4"value="{{$user->phone}}">
              </div>
              <div class="col-md-6">
                <label for="inputPassword4" class="form-label fs-3">Mật khẩu</label>
                @php
         
                @endphp
                <input type="text" class="form-control py-3 fs-3 opacity-75" id="inputPassword4"value="">
              </div>
              <div class="col-md-6">
                <label for="inputPassword4" class="form-label fs-3">Email</label>
                <input type="text" class="form-control py-3 fs-3 opacity-75" id="inputPassword4"value="{{$user->email}}">
              </div>
              <div class="col-md-6">
                <label for="inputPassword4" class="form-label fs-3">Vai trò</label>
                <input type="text" class="form-control py-3 fs-3 opacity-75" id="inputPassword4"value="{{$user->role}}">
              </div>
           
            </form>
          </fieldset>
          </div>
        </div>
      </div>
    </div>
  </main>
</section>
@endsection
