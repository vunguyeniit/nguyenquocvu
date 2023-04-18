@extends('Layout.Master-Layout')
@section('content')
    <div class="content-event">
        <div class="title-event">
            <h2>Sự Kiện Nổi Bật</h2>
        </div>
        <img class="flag-2" src="{{ asset('./asset/images/flag-2.png') }}">
        <img class="flag-1" src="{{ asset('./asset/images/flag-1.png') }}">
        //
        <div class="container">
            <div class="previous btn">
                <img src="" alt="" srcset="{{ asset('./asset/images/previous.png 2.5x') }}">
            </div>
            <div class="list-item">
                <div class="card-event">

                    <img srcset="{{ asset('./asset/images/event-1.png 2.5x') }}">
                    <div class="item">
                        <h2>Sự Kiện 1</h2>
                        <p class="title">Đầm sen Park</p>
                        <p><img src="{{ asset('./asset/images/date.png') }}"> 30/05/2021 - 01/06/2021</p>
                        <p>50.000 VNĐ</p>
                        <p><a href="http://">Xem chi tiết</a></p>
                    </div>
                </div>
            </div>


            <div class="list-item">
                <div class="card-event">
                    <img srcset="{{ asset('./asset/images/event-1.png 2.5x') }}">
                    <div class="item">
                        <h2>Sự Kiện 1</h2>
                        <p class="title">Đầm sen Park</p>
                        <p><img src="{{ asset('./asset/images/date.png') }}"> 30/05/2021 - 01/06/2021</p>
                        <p>50.000 VNĐ</p>
                        <p><a href="{{ route('event-detail') }}">Xem chi tiết</a></p>
                    </div>
                </div>
            </div>


            <div class="list-item">
                <div class="card-event">
                    <img srcset="{{ asset('./asset/images/event-2.png 2.5x') }}">
                    <div class="item">
                        <h2>Sự Kiện 2</h2>
                        <p class="title">Đầm sen Park</p>
                        <p><img src="{{ asset('./asset/images/date.png') }}"> 30/05/2021 - 01/06/2021</p>
                        <p>50.000 VNĐ</p>
                        <p><a href="http://">Xem chi tiết</a></p>
                    </div>
                </div>
            </div>


            <div class="list-item">
                <div class="card-event">
                    <img srcset="{{ asset('./asset/images/event-3.png 2.5x') }}">
                    <div class="item">
                        <h2>Sự Kiện 3</h2>
                        <p class="title">Đầm sen Park</p>
                        <p><img src="{{ asset('./asset/images/date.png') }}"> 30/05/2021 - 01/06/2021</p>
                        <p>50.000 VNĐ</p>
                        <p><a href="http://">Xem chi tiết</a></p>
                    </div>
                </div>
            </div>



            <div class="next btn">
                <img src="" alt="" srcset="{{ asset('./asset/images/next.png 2.5x') }}">
            </div>

        </div>
    </div>
@endsection
