@extends('layout.login_admin');

@section('login')
<form  action={{route('admin.handlelogin')}} method="POST">
  <div class="input-group">
    <label for="">Tên Đăng Nhập *</label>
    <input type="text" name="username"value={{old('username')}}>
@error('username')
    <small style="color:#E73F3F">{{$message}}</small>
@enderror

  </div>
  <div class="input-group">
    <label for="">Mật Khẩu *</label>
    <div style="position: relative;" class="icon-eye">
    <input class="showpass" type="password" name="password" value={{old('password1')}}>
   
      <i class="fa-regular fa-eye-slash"></i>
    </div>
 
 @error('password')
    <small style="color:#E73F3F">{{$message}}</small>
@enderror
  </div>
  @if (session('error'))
  <i style="color: #E73F3F;font-size:1.8rem" class="fa-solid fa-circle-exclamation"></i>
  <a>{{session('error')}}</a>
  @else
  @endif

  <div class="input-group">
    @if (!session('error'))
    <a href={{route('admin.forgot')}}>Quên mật khẩu?</a>
    @else
    <div class="btn_forgot">
      <a href={{route('admin.forgot')}}>Quên mật khẩu?</a>
    </div>
    @endif
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

