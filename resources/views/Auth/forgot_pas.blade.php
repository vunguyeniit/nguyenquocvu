@extends('Auth.layout.login_admin');

@section('login')
<form  action={{route('admin.handleforgot')}} method="POST">
  <div class="content_forgot">
    <h2>Đặt Lại Mật Khẩu</h2>
  <p>Vui lòng nhập email để đặt lại mật khẩu của bạn *</p>
    <input type="email" name="email" value="{{old('email')}}" >

  @if (session('error'))
  <small style="color:red">{{session('error')}}</small>
  @elseif(session('success'))


  <small style="color:green; margin:1rem 0;">{{session('success')}}</small>
  @endif
  </div>
 
  <div class="btn">
    <div class="btn_close">
  <button ><a href="{{route('admin.login')}}">Hủy</a></button>
    </div>
    <div class="btn_login">
      <button type="submit">Tiếp Tục</button>
    </div>
  </div>
 
  @csrf

</form>
@endsection

@section('image')
<img srcset="{{asset('./assets/images/Frame3.png 2x')}}">
@endsection

