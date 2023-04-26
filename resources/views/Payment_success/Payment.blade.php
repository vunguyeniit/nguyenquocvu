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
                    {{--  --}}
                    @foreach ($getPay as $item)
                        <div class="list-item">
                            <div class="card-event">
                                <img srcset="{{ asset('./asset/images/qr_pay.png 2.5x') }}">
                                <div class="item">
                                    <h2>ALT20220418</h2>
                                    <p>VÉ CỔNG</p>
                                    <p>---</p>
                                    <p> Ngày sử dụng- {{ $item->date }}</p>
                                    <p> <img srcset="{{ asset('./asset/images/tick.png 2x') }}"></p>
                                </div>
                            </div>
                        </div>
                    @endforeach



                </div>
                <div class="count-ticket">
                    <span>Số lượng vé 14</span>
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
