<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta http-equiv="X-UA-Compatible" content="ie=edge">
  <link rel="stylesheet" href={{asset('assets/css/style.css')}}>
  <link href="https://fonts.googleapis.com/css2?family=Nunito:wght@400;700;900&display=swap" rel="stylesheet">
  {{-- <link rel="stylesheet" href="https://kit.fontawesome.com/ef6c647e92.css" crossorigin="anonymous"> --}}
  
  <title>Login</title>
</head>
<body>
<div class="container">
  <div class="login ">
    <div class="login_user ">
    <div class="hero_logo ">
      <img srcset="{{asset('./assets/images/Logo_alta.png 2x')}}">
    </div>
    <div class="hero_login">
      @yield('login')
    </div>
  </div>
  </div>
    <div class="hero_img">
      @yield('image')
    </div>
</div>
<script src="https://kit.fontawesome.com/ef6c647e92.js" crossorigin="anonymous"></script>
<script src={{asset('assets/js/style.js')}}></script>
</body>
  
</html>