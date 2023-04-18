@extends('Layout.Master-Layout')
@section('content')
    <div class="content-pay">
        <div class="title-pay">
            <h2>Thanh toán</h2>
        </div>
        <div class="container">
            <div class="ticket-pay">
                <img srcset="{{ asset('./asset/images/ticket-pay-1.png 2.5x') }}" alt="">
                <img srcset="{{ asset('./asset/images/ticket-pay-2.png 2.5x') }}" alt="">
            </div>


            <div class="content-left">
                <img src="{{ asset('./asset/images/Group.png') }}" alt="">

                <div class="list-content">

                    <div class="text-form">
                        <div class="form-pay">
                            <div class="form-col-5">
                                <label for="">Số tiền thanh toán</label>
                                <input type="text">
                            </div>
                            <div class="form-col-2">
                                <label for="">Số lượng vé</label>
                                <input type="text">
                            </div>

                            <div class="form-col-5">
                                <label for="">Ngày sử dụng</label>
                                <input type="text">
                            </div>
                        </div>
                    </div>


                    <div class="text-form">

                        <div class="form-col-8">
                            <label for="">Thông tin liên hệ</label>
                            <input type="text">
                        </div>
                    </div>


                    <div class="text-form">
                        <div style="width:30%" class="form-col-5">
                            <label for="">Điện thoại</label>
                            <input type="text">
                        </div>
                    </div>


                    <div class="text-form">
                        <div class="form-col-8">
                            <label for="">Email</label>
                            <input type="text">
                        </div>
                    </div>


                </div>
            </div>
            <div class="content-center">
                <img src="{{ asset('./asset/images/Vector.png') }}" alt="">
            </div>


            <div class="content-right">
                <img src="{{ asset('./asset/images/form.png') }}" alt="">
                <div class="form-control2">
                    <form action="">

                        <div class="text-form">
                            <label for="">Số thẻ</label>
                            <input type="text">
                        </div>
                        <div class="text-form">
                            <label for="">Họ tên chủ thẻ</label>
                            <input type="text">
                        </div>

                        <div class="text-form">
                            <label for="">Ngày hết hạn</label>
                            <div class="input-form">

                                <div class="input-group date" id="datepicker">

                                    <input type="text" class="form-control datepicker">
                                    <span class="input-group-append">
                                        <span class="input-group-text d-block">
                                            <img srcset="{{ asset('./asset/images/Frame.png 2.5x') }}">
                                        </span>
                                    </span>

                                </div>
                            </div>


                        </div>

                        <div class="text-form">
                            <label for="">CVV/CVC</label>
                            <input type="text">
                        </div>
                        <div class="text-form text-center ">
                            <a href="{{ route('pay-store') }}" class="learn-more mt-3">Thanh Toán</a>
                        </div>
                    </form>
                </div>

            </div>

        </div>
    </div>
    <div class="icon-img">
        <img srcset="{{ asset('./asset/images/Trini_Arnold.png 2.5x') }}" alt="">
    </div>
@endsection
