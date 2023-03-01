{{-- @extends('layout.login_admin');

@section('login')

<form  action="{{route('admin.handlerest')}}" method="POST">
  <h2>Đặt Lại Mật Khẩu Mới</h2>
  <div class="input-group">
    <label for="">Mật Khẩu*</label>
    <input class="showpass" type="password" name="password" >
    <div class="show_pwd">
      <i class="fa-regular fa-eye-slash"></i>
    </div>
   
  </div>
  <div class="input-group">
    <label for="">Nhập Lại Mật Khẩu *</label>
    <div style="position: relative;" class="show_confirm_pass" >
    <input class="showpass" type="password" name="confirm_password">
    @if (session('error'))
    <small style="color:red padding :0.5rem 0">{{session('error')}}</small>
    @endif
      <i class="fa-regular fa-eye-slash" id="confirm"></i>
    </div>
  </div>
 
  <input type="hidden" value="{{$code}}" name='code'>
  <div class="btn_login">
    <button type="submit">Xác Nhận</button>
  </div>
  @csrf
</form>
@endsection
@section('image')
<img srcset="{{asset('./assets/images/Frame.png 2x')}}">
@endsection --}}



@extends('Auth.layout.login_admin');

@section('login')
<form  action={{route('admin.handlerest')}} method="POST">
  <h2>Đặt Lại Mật Khẩu Mới</h2>
  <div class="input-group">
    <label for="">Mật Khẩu* *</label>
    <div style="position: relative;" class="icon-eye">
    <input class="showpass" type="password" name="password">
    <i class="fa-regular fa-eye-slash"></i>
  </div>
  </div>

  <div class="input-group">
    <label for="">Nhập Lại Mật Khẩu *</label>
    <div style="position: relative;" class="icon-eye">
    <input class="showpass" type="password" name="confirm_password">
  
      <i class="fa-regular fa-eye-slash"></i>
    
    </div>
    @if (session('error'))
    <small>{{session('error')}}</small>
    @endif
  </div>



  <div class="input-group">
  <div class="btn_login">
    <button type="submit">Xác Nhận</button>
  </div>
</div>
<input type="hidden" value="{{$code}}" name='code'>
  @csrf
</form>
@endsection
@section('image')
<img srcset="{{asset('./assets/images/Frame.png 2x')}}">
@endsection

