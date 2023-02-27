
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta http-equiv="X-UA-Compatible" content="ie=edge">
  <link href="https://fonts.googleapis.com/css2?family=Nunito:wght@400;700;900&display=swap" rel="stylesheet">
  <link rel="stylesheet" href="https://kit.fontawesome.com/ef6c647e92.css" crossorigin="anonymous">
  <link rel="stylesheet" href={{asset('assets/css/style.css')}}>
  
  <title>Login</title>
</head>
<body>
<div class="wrapper">

@include('layout-admin.sidebar')
{{-- @include('layout-admin.header') --}}
@yield('content')
</div>

<script src="https://kit.fontawesome.com/ef6c647e92.js" crossorigin="anonymous"></script>
<script src={{asset('assets/js/style.js')}}></script>
</body>
</html>
