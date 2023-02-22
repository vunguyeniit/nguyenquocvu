@extends('layout.login_admin');


@section('login')
<form  action={{route('admin.handlerest')}} method="POST">
  <h2>Đặt Lại Mật Khẩu Mới</h2>
  <div class="input-group">
    <label for="">Mật Khẩu*</label>
    <input type="password" name="password" >
  </div>
  <div class="input-group">
    <label for="">Nhập Lại Mật Khẩu *</label>
    <input type="password" name="confirm_password">
  </div>
  <div class="btn_login">
    <button type="submit">Xác Nhận</button>
  </div>
  @csrf
</form>

@endsection

@section('image')
<img srcset="{{asset('./assets/images/Frame.png 2x')}}">
@endsection