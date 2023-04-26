<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.9.0/slick-theme.min.css"
        integrity="sha512-17EgCFERpgZKcm0j0fEq1YCJuyAWdz9KUtv1EjVuaOz8pDnh/0nZxmU6BBXwaaxqoi9PQXnRWqlcDB027hgv9A=="
        crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.9.0/slick.min.css"
        integrity="sha512-yHknP1/AwR+yx26cB1y0cjvQUMvEa2PFzt1c9LlS4pRQ5NOTZFWbhBig+X9G9eYW/8m0/4OXNx8pxJ6z57x0dw=="
        crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="stylesheet" href="{{ asset('asset/css/style.css') }}">
    <link rel="stylesheet" href="https://kit.fontawesome.com/ef6c647e92.css" crossorigin="anonymous">
    <link href="https://fonts.googleapis.com/css2?family=Coiny&family=Montserrat:wght@500;700&display=swap"
        rel="stylesheet">
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
{{-- slick --}}
<script src="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.9.0/slick.min.js"
    integrity="sha512-HGOnQO9+SP1V92SrtZfjqxxtLmVzqZpjFFekvzZVWoiASSQgSr4cw9Kqd2+l8Llp4Gm0G8GIFJ4ddwZilcdb8A=="
    crossorigin="anonymous" referrerpolicy="no-referrer"></script>

<script src="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.9.0/slick.js"
    integrity="sha512-eP8DK17a+MOcKHXC5Yrqzd8WI5WKh6F1TIk5QZ/8Lbv+8ssblcz7oGC8ZmQ/ZSAPa7ZmsCU4e/hcovqR8jfJqA=="
    crossorigin="anonymous" referrerpolicy="no-referrer"></script>



{{-- End slick --}}



<script type="text/javascript">
    $('#datepicker').datepicker({
        autoclose: true,
        format: 'dd/mm/yyyy',
        orientation: 'bottom',
        todayHighlight: true
    });
</script>
{{-- Xử lí trang Modal --}}
<script>
    $(document).ready(function() {
        $('.modal').modal('show');
    });
</script>


<script type="text/javascript">
    $(".list-ticket").slick({
        slidesToShow: 4,
        slidesToScroll: 4,
        infinite: false,
        arrows: true,
        draggable: false,
        prevArrow: `<img class='slick-prev slick-arrow' srcset="{{ asset('./asset/images/previous.png 2.5x') }}">`,
        nextArrow: `<img class='slick-next slick-arrow' srcset="{{ asset('./asset/images/next.png 2.5x') }}">`,
        responsive: [{
                breakpoint: 1025,
                settings: {
                    slidesToShow: 3,
                },
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    arrows: false,
                    infinite: false,

                },
            },
        ],

    });
</script>


<script type="text/javascript">
    $(".container-event").slick({
        slidesToShow: 4,
        slidesToScroll: 1,
        infinite: false,
        arrows: true,
        draggable: false,
        prevArrow: `<img class='slick-prev slick-arrow' srcset="{{ asset('./asset/images/previous.png 2.5x') }}">`,
        nextArrow: `<img class='slick-next slick-arrow' srcset="{{ asset('./asset/images/next.png 2.5x') }}">`,
        responsive: [{
                breakpoint: 1025,
                settings: {
                    slidesToShow: 3,
                },
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    arrows: false,
                    infinite: false,

                },
            },
        ],

    });
</script>

</html>
