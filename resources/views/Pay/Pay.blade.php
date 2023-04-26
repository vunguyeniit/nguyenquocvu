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
            <form class="d-flex" action="{{ route('pay-store') }}" method="POST">
                @csrf


                <div class="content-left">
                    <img srcset="{{ asset('./asset/images/Group.png 2.5x') }}" alt="">

                    <div class="list-content">

                        <div class="text-form">
                            <div class="form-pay">
                                <div class="form-col-5">
                                    <label for="">Số tiền thanh toán</label>

                                    <input type="text" name="money"
                                        value="{{ number_format($price, 0, ', ', '.') . ' ' . 'vnđ' }}">
                                </div>
                                <div class="form-col-2">
                                    <label for="">Số lượng vé </label>
                                    <input type="text" name="ticket" value="{{ $data['price_ticket'] }}">
                                </div>

                                <div class="form-col-5">
                                    <label for="">Ngày sử dụng</label>
                                    <input type="text" name="date" value="{{ $data['date'] }}">
                                </div>
                            </div>
                        </div>


                        <div class="text-form">

                            <div class="form-col-8">
                                <label for="">Thông tin liên hệ</label>
                                <input type="text" name="username" value={{ $data['username'] }}>
                            </div>
                        </div>


                        <div class="text-form">
                            <div style="width:30%" class="form-col-5">
                                <label for="">Điện thoại</label>
                                <input type="text" name="phone" value={{ $data['phone'] }}>
                            </div>
                        </div>


                        <div class="text-form">
                            <div class="form-col-8">
                                <label for="">Email</label>
                                <input type="email" name="email" value={{ $data['email'] }}>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="content-center">
                    <img srcset="{{ asset('./asset/images/Vector.png 2.5x') }}" alt="">
                </div>


                <div class="content-right">
                    <img srcset="{{ asset('./asset/images/form.png 2.5x') }}" alt="">
                    <div class="form-control2">
                        {{-- <form action=""> --}}

                        <div class="text-form">
                            <label for="">Số thẻ</label>
                            <input type="text" name="card_number" maxlength="8" />
                        </div>
                        <div class="text-form">
                            <label for="">Họ tên chủ thẻ</label>
                            <input type="text" name="card_holder">
                        </div>

                        <div class="text-form">
                            <label for="">Ngày hết hạn</label>
                            <div class="input-form">

                                <div class="input-group date" id="datepicker">

                                    <input type="text" name="expiration_date" class="form-control datepicker">
                                    <span class="input-group-append">
                                        <span style="background: #FFF6D4;border:none" class="input-group-text d-block">
                                            <img srcset="{{ asset('./asset/images/Frame.png 2.5x') }}">
                                        </span>
                                    </span>

                                </div>
                            </div>


                        </div>

                        <div class="text-form">
                            <label for="">CVV/CVC</label>
                            <input style="width: 30%" type="password" name="CVV" maxlength="3">
                        </div>
                        <div class="text-form text-center mt-3 ">
                            <button type="submit">Thanh toán</button>
                        </div>
                        {{-- </form> --}}
                    </div>

                </div>

            </form>
        </div>
    </div>


    <div class="icon-img">
        <img srcset="{{ asset('./asset/images/Trini_Arnold.png 2.5x') }}" alt="">
    </div>
@endsection
