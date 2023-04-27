@extends('Layout.Master-Layout')
@section('content')
    <div class="content-event">
        <div class="title-event">
            <h2>Sự Kiện 1</h2>
        </div>
        <img class="flag-2" src="{{ asset('./asset/images/flag-2.png') }}">
        <img class="flag-1" src="{{ asset('./asset/images/flag-1.png') }}">
        <div class="content-detail">
            <div class="container">
                <div class="bg-payment">
                    <img srcset="{{ asset('./asset/images/bg-pay.png 2.5x') }}" alt="">
                    <div class="event-detail">
                        <div class="list-detail">
                            <img srcset="{{ asset('./asset/images/event-detail.png 2.8x') }}">
                            <p><img src="{{ asset('./asset/images/date.png') }}"> {{ $detailEvent->start_day }} -
                                {{ $detailEvent->end_day }}
                            </p>
                            <h2>Đầm sen Park</h2>
                            <p>{{ $detailEvent->price_ticket }} VNĐ</p>
                        </div>
                        <div class="list-detail">
                            <h3> <b>Lorem ipsum </b> dolor sit amet consectetur adipisicing elit. Et, quasi odit voluptatum
                                ea
                                placeat incidunt sed perferendis omnis nisi, hic asperiores eum porro accusantium veniam
                                quis. Veniam, quisquam velit! Vitae.
                                Lorem ipsum dolor sit amet consectetur adipisicing elit. Et, quasi odit voluptatum ea
                                placeat incidunt sed perferendis omnis nisi, hic asperiores eum porro accusantium veniam
                                quis. Veniam, quisquam velit! Vitae.
                            </h3>
                        </div>

                        <div class="list-detail">
                            <img srcset="{{ asset('./asset/images/event-1.png 2.8x') }}">
                            <h3 class="mt-5">Lorem ipsum dolor sit amet consectetur adipisicing elit. Et, quasi odit
                                voluptatum ea
                                placeat incidunt sed perferendis omnis nisi, hic asperiores eum porro accusantium veniam
                                quis. Veniam, quisquam velit! Vitae.</h3>
                        </div>


                        <div class="list-detail">
                            <h3 class="mb-5">Lorem ipsum dolor sit amet consectetur adipisicing elit. Et, quasi odit
                                voluptatum ea
                                placeat incidunt sed perferendis omnis nisi, hic asperiores eum porro accusantium veniam
                                quis. Veniam, quisquam velit! Vitae.</h3>
                            <img srcset="{{ asset('./asset/images/event-1.png 2.8x') }}">
                        </div>
                    </div>



                </div>
            </div>
        </div>
    </div>
@endsection
