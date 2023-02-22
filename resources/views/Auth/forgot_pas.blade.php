@extends('layout.login_admin');



@section('login')
<form  action={{route('admin.handleforgot')}} method="POST">
  <div class="content_forgot">
    <h2>Đặt Lại Mật Khẩu</h2>
  <p>Vui lòng nhập email để đặt lại mật khẩu của bạn *</p>
    <input type="email" name="email" >
    @if(session('error'))
    
  <a>{{session('error')}}</a>
  @endif
  </div>
 
  <div class="btn">
    <div class="btn_close">
      <button type="submit">Hủy</button>
    </div>
    <div class="btn_login">
      <button type="submit">Tiếp Tục</button>
    </div>
  </div>
  @csrf

</form>
@endsection

@section('image')
<img srcset="{{asset('./assets/images/Frame.png 2x')}}">
@endsection

