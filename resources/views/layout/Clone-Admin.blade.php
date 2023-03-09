<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link rel="stylesheet" href={{ asset('assets/css/style.css') }}>
    {{-- CDN GoogleFont --}}
    <link href="https://fonts.googleapis.com/css2?family=Nunito:wght@400;700;900&display=swap" rel="stylesheet">
    {{-- CDN Fontawesome --}}
    <link rel="stylesheet" href="https://kit.fontawesome.com/ef6c647e92.css" crossorigin="anonymous">
    {{-- CDN Bootstrap --}}
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet">
    {{-- CDN select2 --}}
    <link rel="stylesheet" href="{{ asset('assets/select2/select2/dist/css/select2.min.css') }}">
    <title>Login</title>
</head>

<body style="background-color: #ddd9d9;overflow-x:unset">
    <div class="wrapper">

        @include('sidebar.sidebar')


        @yield('header');
        @yield('content')
    </div>
    <script src={{ asset('assets/js/style.js') }}></script>
    {{-- CDN Bootstrap --}}
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>
    {{-- CDN Fontawesome --}}
    <script src="https://kit.fontawesome.com/ef6c647e92.js" crossorigin="anonymous"></script>
    {{-- CDN Ckeditor --}}
    {{-- <script src="//cdn.ckeditor.com/4.20.2/standard/ckeditor.js"></script>
    <script>
        CKEDITOR.replace('ckeditor');
    </script> --}}
    {{-- CDN Jquery --}}
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.3/jquery.min.js"></script>
    {{-- CDN select2 --}}
    <script src="{{ asset('assets/select2/select2/dist/js/select2.min.js') }}"></script>

    <script type="text/javascript">
        // $(document).ready(function() {
        //     $('#js-select2').select2();
        // });

        $("#js-select2").select2({
            tags: true,
            tokenSeparators: [',']
        });
    </script>

    <script type="text/javascript">
        $("#status-device").on('change', function() {
            var statusid = $(this).val();
            console.log(statusid);
            $.ajax({
                url: "{{ route('device.index') }}",
                type: "GET",
                data: {
                    'statusid': statusid
                },
                success: function(data) {
                    var sta = data.devicestatus;
                    console.log(sta);
                    var html = '';
                    if (sta.length > 0) {
                        for (let i = 0; i < sta.length; i++) {
                            html +=
                         '<tr>\
                            <td>' + sta[i]['devicecode'] +'</td>\
                            <td>' + sta[i]['devicename'] +'</td>\
                            <td>' + sta[i]['addressip'] +'</td>\
                            </tr>';
                         
                        }
                    } else {
                        html +=
                            '<tr>\
                                                                                                                                                                        <td>Khong Có san pham</td>\
                                                                                                                                                                        </tr>';
                    }
                    $("#tbody")

                        .html(html);

                }
            })
        });
    </script>
</body>

</html>
