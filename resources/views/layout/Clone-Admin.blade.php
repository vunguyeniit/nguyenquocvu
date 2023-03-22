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
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css">
    <title>Login</title>
 {{-- <style>
      .content{
    display: block;
    }
 </style> --}}
</head>

<body style="background-color: #ddd9d9;overflow-x:unset">
    <div class="wrapper" style="background: none">
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
{{-- Xử lí trang select2 --}}
<script type="text/javascript">
        $("#js-select2").select2({
            tags: true,
            tokenSeparators: [',']
        });
    </script>
{{-- Xử lí trang device --}}
 <script  type="text/javascript">

  $(document).ready(function() {
    bindBtnDetailClickEvent();

  });
     function bindBtnDetailClickEvent() {
        let btn_de = document.querySelectorAll('.btn-detail');
        let cont = document.querySelectorAll('.content');
        btn_de.forEach((button, index) => {
            $(button).on("click", function() {
            $(cont[index]).show();
            });
            $(cont[index]).on("click", function() {
            $(this).hide();
            });
        });
           
         }
    function fetchData(params) {
      $.ajax({
          url: "{{ route('device.index') }}",
          type: "GET",
          data: params,
          success: function (data) {
              var item = data.devicestatus;
              var html = '';
              if (data.devicestatus.length > 0) {
                item.forEach(element => {
                    html += '<tr>';
                      html += '<td>' + element.devicecode + '</td>';
                      html += '<td>' + element.devicename + '</td>';
                      html += '<td>' + element.addressip + '</td>';
                      html += '<td>' + (element.activestatus == 0 ? '<i class="fa-solid fa-circle text-danger fs-6"></i>Ngưng hoạt động' : '<i class="fa-solid fa-circle text-success fs-6"></i> hoạt động') + '</td>';
                      html += '<td>' + (element.connectionstatus == 0 ? '<i class="fa-solid fa-circle text-danger fs-6"></i>Mất kết nối' : '<i class="fa-solid fa-circle text-success fs-6"></i> kết nối') + '</td>';
                      html += '<td>';
                        html += '<div class="box-container">'
                        html += '<div class="content">';
                        html += '<span>' + element.device_service  + ', </span>';
                        html += '</div>'
                        html += '<p class="btn-detail">Xem thêm</p>'
                        html += '</div>'
                        html += '</td>'
                        html += '<td>';
                            html += '<a href="{{ route('device.show', ['device' => ':id']) }}">Chi tiết</a>'
                                        .replace(':id', element.id);
                            html += '</td>';
                            html += '<td>';
                            html += '<a href="{{ route('device.edit', ['device' => ':id']) }}">Cập nhật</a>'
                                        .replace(':id', element.id);
                            html += '</td>';
                      html += '</tr>';
                        
                });
              } else {
                  html += '<tr><td>Không có sản phẩm</td></tr>';
              }
              $("#tbody").html(html);
              bindBtnDetailClickEvent();
          }
      });
  }
  $("#connection-device,#status-device",).on('change', function () {
      var connection = $(this).val();
      fetchData({ 'connection': connection,
      });
  });
  
  $("#status-device").on('change', function () {
      var statusid = $(this).val();
      fetchData({ 'statusid': statusid });
  });

  </script>
{{-- Xử lí trang service --}}
<script  type="text/javascript">
    $("#status-service").on('change', function () {
      var statusid = $(this).val();
  
      $.ajax({
          url: "{{ route('service.index') }}",
          type: "GET",
          data: {
              'statusid': statusid
          },
          success: function (data) {
              var sta = data.servicestatus;
              
              var html = '';
              if (sta.length > 0) {
                    sta.forEach(element => {
                        console
                    html +='<tr>'     
                        html += '<td>'+ element.servicecode + '</td>'
                        html +='<td>' + element.servicename + '</td>'
                        html +='<td>' + element.description+ '</td>'
                        html +='<td>' + (element.status == 0 ? '<i class="fa-solid fa-circle text-danger fs-6"></i> Ngưng Hoạt động' : '<i class="fa-solid fa-circle text-success fs-6"></i>  hoạt động') + '</td>'
                        html += '<td>';
                            html += '<a href="{{ route('service.show', ['service' => ':id']) }}">Chi tiết</a>'
                                        .replace(':id', element.id);
                            html += '</td>';
                            html += '<td>';
                            html += '<a href="{{ route('service.edit', ['service' => ':id']) }}">Cập nhật</a>'
                                        .replace(':id', element.id);
                            html += '</td>';
                        html +='</tr>'     
                
                })
              } else {
                  html +=
                      '<tr>\                                                                                                                                                        <td>Khong Có san pham</td>\
                                                                                                                                                                          </tr>';
              }
              $("#tbody-service")
    
                  .html(html)
    
          }
      })
    }); 
    </script>
{{-- Xử lí trang service-detail --}}
<script  type="text/javascript">
    $("#status-detail,#status-id").on('change', function () {
          var statusid = $(this).val();
          var url = $(this).attr('href');
          $.ajax({
              url: url,
              type: "GET",
              data: {
                  'statusid': statusid
              },
              success: function (data) {
                  var sta = data.servicestatus;
                  console.log(sta)
                  var html = '';
                  if (sta.length > 0) {
                        sta.forEach(element => {
                            
                            html +='<tr>'     
                            html += '<td>'+ element.number + '</td>'
                            html += '<td>' +
                                        (element.status == 0 ? '<i class="fa-solid fa-circle text-success fs-6"></i> Đã hoàn thành' : '') +
                                        (element.status == 1 ? '<i class="fa-solid fa-circle text-primary fs-6"></i> Đang thực hiện' : '') +
                                        (element.status == 2 ? '<i class="fa-solid fa-circle text-secondary fs-6"></i> Vắng' : '') + '</td>';
                            html +='</tr>'  
                        });
                  } else {
                      html +=
                          '<tr>\       <td>Khong Có san pham</td>\
                            </tr>';
                  }
                  $("#tbody-detail")
        
                      .html(html)
        
              }
          })
        }); 
    </script>
{{-- Xử lí trang status-account --}}
<script  type="text/javascript">
    $("#status-account").on('change', function () {
          var statusid = $(this).val();
          console.log(statusid)
          $.ajax({
              url: "{{route('account.index')}}",
              type: "GET",
              data: {
                  'statusid': statusid
              },
              success: function (data) {
                  var sta = data.account;
                  var html = '';
                  if (sta.length > 0) {
                     sta.forEach(element => {
                            html +='<tr>'     
                            html += '<td>'+ element.username + '</td>'
                            html += '<td>'+ element.fullname + '</td>'
                            html += '<td>'+ element.phone + '</td>'
                            html += '<td>'+ element.email + '</td>'
                            html += '<td>'+ element.role + '</td>'
                            html += '<td>' + (element.status == 0 ? '<i class="fa-solid fa-circle text-danger fs-6"></i>Ngưng hoạt động' : '<i class="fa-solid fa-circle text-success fs-6"></i> hoạt động') + '</td>';
                            html += '<td>';
                            html += '<a href="{{ route('account.edit', ['account' => ':id']) }}">Cập nhật</a>'
                                        .replace(':id', element.id);
                            html += '</td>';
                            html +='</tr>'  
                         });
                  } else {
                      html +=
                          '<tr>\       <td>Khong Có san pham</td>\
                            </tr>';
                  }
                  $("#tbody-account")
        
                      .html(html)
        
              }
          })
        }); 
    </script>
{{-- Xử lí trang Modal --}}
<script>
    $(document).ready(function() {
        $('.modal').modal('show');
    });
</script>
{{-- Xử lí trang Nublevel --}}
<script  type="text/javascript">
    function fetchNumber(params) {
      $.ajax({
          url: "{{ route('nublevel.index') }}",
          type: "GET",
          data: params,
          success: function (data) {
              var sta = data.servicename;
              console.log(sta)
              var html = '';
              if (sta.length > 0) {
                sta.forEach(element => {
                      html += '<tr>';
                      html += '<td>' + element.number_print + '</td>';
                      html += '<td>' + element.fullname + '</td>';
                      html += '<td>' + element.servicename + '</td>';
                      html += '<td>' + element.grant_time + '</td>';
                      html += '<td>' + element.expired + '</td>';
                      html += '<td>' +
                                        (element.status == 0 ? '<i class="fa-solid fa-circle text-secondary fs-6"></i> Đã sử dụng' : '') +
                                        (element.status == 1 ? '<i class="fa-solid fa-circle text-primary fs-6"></i> Đang chờ' : '') +
                                        (element.status == 2 ? '<i class="fa-solid fa-circle text-danger fs-6"></i> Bỏ qua' : '') + '</td>';
                                        html += '<td>' + element.supply + '</td>';

                                        html += '<td>';
                            html += '<a href="{{ route('nublevel.show', ['nublevel' => ':id']) }}">Chi tiết</a>'
                                        .replace(':id', element.id);
                            html += '</td>';
                                        html += '</tr>';
                                    }); 
              } else {
                  html += '<tr><td>Không có sản phẩm</td></tr>';
              }
              $("#tbody-nublevel").html(html);
            
          }
      });
  }
  $("#servicename").on('change', function () {
      var servicename = $(this).val();
      fetchNumber({ 'servicename': servicename });
  });
  
  $("#nub-status").on('change', function () {
      var nubstatus = $(this).val();
      fetchNumber({ 'nubstatus': nubstatus });
  });

  $("#nub-supply").on('change', function () {
      var nubsupply = $(this).val();
      fetchNumber({ 'nubsupply': nubsupply });
  });
  
  
  </script>






<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
<script  type="text/javascript">
$(document).ready(function() {
  // Initialize datepicker
  $('#startdate,#enddate').datepicker({
    autoclose: true,
     format: 'yyyy-mm-dd',
    todayHighlight: true
  });
});


$('#datepicker').datepicker({
    autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
});
// $('#datepicker').on('changeDate', function() {
    
//     $('#my_hidden_input').val(
//         $('#datepicker').datepicker( 'getFormattedDate'
//         )
        
//     );
// });
</script>


</body>
</html>
