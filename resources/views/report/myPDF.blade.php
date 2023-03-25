<!DOCTYPE html>
<html>
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Laravel 9 Create PDF File using DomPDF Tutorial - LaravelTuts.com</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">

<style>
  *{ font-family: DejaVu Sans !important;}
</style>


  </head>
<body>
    <h1>{{ $title }}</h1>
    <p>Date: {{ $date }}</p>

  
    <table class="table table-bordered">
        <tr>
            <th>ID</th>
            <th>Số thứ tự</th>
            <th>Tên dịch vụ</th>
            <th>Thời gian cấp</th>
            <th>Tình trạng</th>
            <th>Nguồn cấp</th>
        </tr>
        @foreach($reports as $report)
            <tr>
                <td>{{ $report->id }}</td>
                <td>{{ $report->number_print }}</td>
                <td>{{ $report->servicename }}</td>
                <td>{{ $report->grant_time }}</td>
                @if ($report->status==0) 
                                    <td><i class="fa-solid fa-circle text-secondary fs-6"></i> Đã sử dụng</td>
                                    @elseif($report->status==1)
                                    <td><i class="fa-solid fa-circle text-primary fs-6"></i> Đang chờ</td>
                                    @else 
                                     <td><i class="fa-solid fa-circle text-danger fs-6"></i> Bỏ qua</td>
                                     @endif 
                                    <td>{{$report->supply}}</td>
    
            </tr>
        @endforeach
    </table>
</body>
</html>