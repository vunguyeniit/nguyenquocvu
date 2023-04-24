@extends('Layout.Master-Layout')
@section('content')
    <div class="content-payment">
        <div class="title-pay">
            <h2>Thanh toán thành công</h2>
        </div>
        <div class="container">

            <div class="bg-payment">

                <img srcset="{{ asset('./asset/images/bg-pay.png 2.7x') }}" alt="">
                <div class="list-ticket">
                    <div class="previous btn">
                        <img src="" alt="" srcset="{{ asset('./asset/images/previous.png 2.5x') }}">
                    </div>
                    {{--  --}}
                    <div class="list-item">
                        <div class="card-event">
                            <img srcset="{{ asset('./asset/images/qr_pay.png 2.5x') }}">
                            <div class="item">
                                <h2>ALT20220418</h2>
                                <p>VÉ CỔNG</p>
                                <p>---</p>
                                <p> Ngày sử dụng- 01/06/2021</p>
                                <p> <img srcset="{{ asset('./asset/images/tick.png 2x') }}"></p>
                            </div>
                        </div>
                    </div>

                    <div class="list-item">
                        <div class="card-event">
                            <img srcset="{{ asset('./asset/images/qr_pay.png 2.5x') }}">
                            <div class="item">
                                <h2>ALT20220418</h2>
                                <p>VÉ CỔNG</p>
                                <p>---</p>
                                <p> Ngày sử dụng- 01/06/2021</p>
                                <p> <img srcset="{{ asset('./asset/images/tick.png 2x') }}"></p>
                            </div>
                        </div>
                    </div>


                    <div class="list-item">
                        <div class="card-event">
                            <img srcset="{{ asset('./asset/images/qr_pay.png 2.5x') }}">
                            <div class="item">
                                <h2>ALT20220418</h2>

                                <p>VÉ CỔNG</p>
                                <p>---</p>
                                <p> Ngày sử dụng- 01/06/2021</p>
                                <p> <img srcset="{{ asset('./asset/images/tick.png 2x') }}"></p>
                            </div>
                        </div>
                    </div>



                    <div class="list-item">
                        <div class="card-event">
                            <img srcset="{{ asset('./asset/images/qr_pay.png 2.5x') }}">
                            <div class="item">
                                <h2>ALT20220418</h2>
                                <p>VÉ CỔNG</p>
                                <p>---</p>
                                <p> Ngày sử dụng- 01/06/2021</p>
                                <p><img srcset="{{ asset('./asset/images/tick.png 2x') }}"></p>
                            </div>
                        </div>
                    </div>

                    <div class="count-ticket">
                        <span>Số lượng vé 14</span>
                    </div>
                    <div class="next btn">
                        <img src="" alt="" srcset="{{ asset('./asset/images/next.png 2.5x') }}">
                    </div>


                </div>
                {{--  --}}
            </div>


        </div>
    </div>

    <div class="btn-pay">
        <a href="" class="learn-more mt-3">Tải về</a>
        <a href="" class="learn-more mt-3">Gửi email</a>
    </div>
    <div class="icon-img">
        <img srcset="{{ asset('./asset/images/Alvin_Arnold.png 2.5x') }}" alt="">
    </div>
@endsection
