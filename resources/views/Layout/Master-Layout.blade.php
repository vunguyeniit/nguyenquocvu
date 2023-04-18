<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <link rel="stylesheet" href="{{asset('asset/css/style.css')}}">
  <link rel="stylesheet" href="https://kit.fontawesome.com/ef6c647e92.css" crossorigin="anonymous">
  <link href="https://fonts.googleapis.com/css2?family=Coiny&family=Montserrat:wght@500;700&display=swap" rel="stylesheet">
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet">
  <link rel="stylesheet" href="{{ asset('asset/css/bootstrap-datepicker.min.css') }}">
  <title>Little</title>
</head>
<body>
  <section id="main">
  @include('Layout.Header.Header')
    <section class="content">
     @yield('content')
    </section>
  </section>
</body>
<script src="{{ asset('asset/js/style.js') }}"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.3/jquery.min.js"></script>
<script src="https://kit.fontawesome.com/ef6c647e92.js" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
<script type="text/javascript">
  $('#datepicker').datepicker({
        autoclose: true,
          format: 'yyyy-mm-dd',
          orientation: 'bottom',
          todayHighlight: true
});
</script>


</html>