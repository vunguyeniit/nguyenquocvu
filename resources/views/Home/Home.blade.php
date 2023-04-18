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
            <img src="{{ asset('./asset/images/Group.png') }}" alt="">
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
            <img src="{{ asset('./asset/images/Vector.png') }}" alt="">
        </div>


        <div class="content-right">
            <img src="{{ asset('./asset/images/form.png') }}" alt="">
            <img src="{{ asset('./asset/images/hair.png') }}">
            <img src="{{ asset('./asset/images/title-ticker.png') }}">

            <div class="form-control2">
                <form action="">

                    <div class="text-form">
                        <div class="select-service">
                            <input type="text" placeholder="">
                            <img src="{{ asset('./asset/images/Show.png') }}">
                        </div>

                    </div>
                    <div class="text-form">
                        <div class="input-form">
                            <input type="text" placeholder="Số lượng vé">
                            <div class="input-group date" id="datepicker">
                                <input type="text" class="form-control datepicker" placeholder="Ngày sử dụng">
                                <span class="input-group-append">
                                    <span class="input-group-text d-block">
                                        <img srcset="{{ asset('./asset/images/Frame.png 2.5x') }}">
                                    </span>
                                </span>

                            </div>
                        </div>


                    </div>
                    <div class="text-form">
                        <input type="text" placeholder="Họ và tên">
                    </div>
                    <div class="text-form">
                        <input type="text"placeholder="Số điện thoại">
                    </div>
                    <div class="text-form">
                        <input type="text"placeholder="Địa chỉ email">
                    </div>
                    <div class="text-form text-center">
                        <a href="{{ route('pay') }}" class="learn-more">Đặt Vé</a>

                    </div>
                </form>
            </div>
        </div>

    </div>
@endsection
