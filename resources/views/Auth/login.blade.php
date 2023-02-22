@extends('layout.login_admin');

@section('login')
<form  action={{route('admin.handlelogin')}} method="POST">
  <div class="input-group">
    <label for="">Tên Đăng Nhập *</label>
    <input type="text" name="username">
    @error('username')
    <small style="color:red">{{$message}}</small>
@enderror

  </div>
  <div class="input-group">
    <label for="">Mật Khẩu *</label>
    <input type="password" name="password">
    @error('password')
    <small style="color:red">{{$message}}</small>
@enderror
  </div>
  @if (session('error'))
  {{-- <i class="fa-solid fa-circle-exclamation"></i> --}}
  <a>{{session('error')}}</a>

  @else
  
  @endif
  <div class="input-group">
  <a href={{route('admin.forgot')}}>Quên Mật Khẩu ?</a>
  </div>
  <div class="btn_login">
    <button type="submit">Đăng Nhập</button>
  </div>
  @csrf
</form>

@endsection

@section('image')
<img srcset="{{asset('./assets/images/group.png 2x')}}">
<div class="content">
  <h2>Hệ Thống</h2>
  <span>Quản Lý Xếp Hàng</span>
</div>
@endsection
