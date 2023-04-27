@extends('Layout.Master-Layout')
@section('content')
    <div class="content-event">
        <div class="title-event">
            <h2>Sự Kiện Nổi Bật</h2>
        </div>
        <img class="flag-2" src="{{ asset('./asset/images/flag-2.png') }}">
        <img class="flag-1" src="{{ asset('./asset/images/flag-1.png') }}">
        <div class="container-event">
            @foreach ($getEvent as $item)
                <div class="list-slick">
                    <div class="card-event">
                        <img srcset="{{ asset('./asset/images/event-1.png 2.5x') }}">
                        <div class="item">
                            <h2>{{ $item->title_event }}</h2>
                            <p class="title">Đầm sen Park</p>
                            <p><img src="{{ asset('./asset/images/date.png') }}"> {{ $item->start_day }} -
                                {{ $item->end_day }}</p>
                            <p> {{ $item->price_ticket }} VNĐ</p>
                            <p><a href="{{ route('event-detail', $item->id) }}">Xem chi tiết</a></p>
                        </div>
                    </div>
                </div>
            @endforeach
        </div>
    </div>
    </div>
@endsection
