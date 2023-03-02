<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link href="https://fonts.googleapis.com/css2?family=Nunito:wght@400;700;900&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="https://kit.fontawesome.com/ef6c647e92.css" crossorigin="anonymous">
    <link rel="stylesheet" href={{ asset('assets/css/style.css') }}>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="//cdn.ckeditor.com/4.20.2/standard/ckeditor.js"></script>

    <title>Login</title>
</head>

<body style="background-color: #ddd9d9;overflow-x:unset">
    <div class="wrapper">

        @include('sidebar.sidebar')
        {{-- @include('layout-admin.header') --}}

        @yield('header');
        @yield('content')
    </div>
    <script>
        CKEDITOR.replace('ckeditor');
    </script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://kit.fontawesome.com/ef6c647e92.js" crossorigin="anonymous"></script>
    <script src={{ asset('assets/js/style.js') }}></script>
</body>

</html>
