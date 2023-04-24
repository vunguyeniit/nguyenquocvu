@extends('Layout.Master-Layout')
@section('content')
    <div class="list-img">
        <img srcset="{{ asset('./asset/images/Lisa_Arnold.png 2.5x') }}">
        <img src="{{ asset('./asset/images/Converted-06.png') }}">
        <img src="{{ asset('./asset/images/image-2.png') }}">
        <img src="{{ asset('./asset/images/Converted-03.png') }}">
        <img src="{{ asset('./asset/images/Converted-02.png') }}">
        <img src="{{ asset('./asset/images/Converted-05.png') }}">
        <img src="{{ asset('./asset/images/Converted-03.png') }}">
    </div>
    <div class="container">
        <div class="content-left">
            <img srcset="{{ asset('./asset/images/Group.png 2.6x') }}" alt="">
            <img src="{{ asset('./asset/images/Converted-04.png') }}">
            <div class="list-content">
                <div class="content-title">
                    <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quidem quae hic voluptatum sit numquam ipsa
                        aut quibusdam neque pariatur reprehenderit </p>
                    <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quidem quae hic voluptatum sit numquam ipsa
                        aut quibusdam neque pariatur reprehenderit</p>
                </div>

                <div class="list-title">
                    <p><img src="{{ asset('./asset/images/star.png') }}"> Lorem ipsum, dolor sit amet consectetur
                        adipisicing elit.</p>
                    <p><img src="{{ asset('./asset/images/star.png') }}"> Lorem ipsum, dolor sit amet consectetur
                        adipisicing elit.</p>
                    <p><img src="{{ asset('./asset/images/star.png') }}"> Lorem ipsum, dolor sit amet consectetur
                        adipisicing elit.</p>
                    <p><img src="{{ asset('./asset/images/star.png') }}"> Lorem ipsum, dolor sit amet consectetur
                        adipisicing elit.</p>
                </div>

            </div>
        </div>
        <div class="content-center">
            <img srcset="{{ asset('./asset/images/Vector.png 2.6x') }}" alt="">
        </div>


        <div class="content-right">
            <img srcset="{{ asset('./asset/images/form.png 2.6x') }}" alt="">
            <img src="{{ asset('./asset/images/hair.png') }}">
            <img src="{{ asset('./asset/images/title-ticker.png') }}">

            <div class="form-control2">
                <form action="{{ route('pay') }}" method="GET">
                    @csrf
                    <div class="text-form">
                        <div class="select-service">
                            <input type="text" value="Gói gia đình" name="name_ticket" placeholder="">
                            <img src="{{ asset('./asset/images/Show.png') }}">
                        </div>

                    </div>
                    <div class="text-form">
                        <div class="input-form">
                            <input type="text" placeholder="Số lượng vé" name="price_ticket">
                            <div class="input-group date" id="datepicker">
                                <input type="text" class="form-control datepicker" name="date"
                                    placeholder="Ngày sử dụng">
                                <span class="input-group-append">
                                    <span class="input-group-text d-block">
                                        <img srcset="{{ asset('./asset/images/Frame.png 2.5x') }}">
                                    </span>
                                </span>

                            </div>
                        </div>


                    </div>
                    <div class="text-form">
                        <input type="text" placeholder="Họ và tên" name="username">
                    </div>
                    <div class="text-form">
                        <input type="text"placeholder="Số điện thoại" name="phone">
                    </div>
                    <div class="text-form">
                        <input type="text"placeholder="Địa chỉ email"name="email">
                    </div>
                    <div class="text-form text-center">

                        <button type="submit">Đặt vé</button>

                    </div>
                </form>
            </div>
        </div>

    </div>
@endsection
