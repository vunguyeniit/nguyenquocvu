@extends('layout.Clone-Admin')

{{--  --}}

@section('content')
    <section id="content">
        {{-- @include('admin.header') --}}
    @section('header')
        <nav>
            <div class="header-right">
                <div class="header-left">
                    <h2 style="color: #7E7D88;margin-right:1rem">Thiết bị <i class="fa-solid fa-chevron-right fs-4"></i></h2>
                    <h2>Danh sách thiết bị</h2>
                </div>
                <div class="profile">
                    <div class="icon-bell">
                        <i class="fa-solid fa-bell"></i>
                    </div>
                    <div class="img_content">
                        <img src="{{ asset('assets/images/user.png') }}">
                        <div class="user-content">
                            <p>Xin Chào</p>
                            <h3>Lê Quỳnh Ái Vân</h3>
                        </div>
                    </div>

                </div>

            </div>

            <div class="title">
                <h2>Danh sách thiết bị</h2>
            </div>
        </nav>
    @endsection


    <main>

        <div class="select">
            <form action="" class="row">
                <div class="col-md-3">
                    <label for="inputEmail4" class="form-label fs-3">Trạng thái hoạt động</label>
                    <select class="form-select py-2 fs-3" aria-label="Default select example">

                        <option selected>Tất cả</option>
                        <option value="1">Hoạt động</option>
                        <option value="2">Ngưng hoạt động</option>
                    </select>
                </div>
                <div class="col-md-3">
                    <label for="inputPassword4" class="form-label fs-3">Trạng thái kết nối</label>
                    <select class="form-select py-2 fs-3" aria-label="Default select example" id="status-device">
                        <option value="" selected>Tất cả</option>
                        {{-- <option selected>Tất cả</option> --}}
                        <option value="1">Mất Kết nối</option>
                        <option value="0">Kết nối</option>

                    </select>
                </div>
                <div class="col-md-4 mx-5">
                    <label for="inputPassword4" class="form-label fs-3">Từ khóa</label>
                    <div class="search">
                        <input type="text" class="form-control py-1 fs-2 " id="inputPassword4">
                        <i class="fa-solid fa-magnifying-glass"></i>
                    </div>
                </div>
            </form>
        </div>

        <div class="data">
            <div class="content-data p-0">
                <div class="head">
                    <div class="form-user">

                        <table id="customers">
                        <thead>
                            <tr>
                                <th>Mã thiết bị</th>
                                <th>Tên thiết bị</th>
                                <th>Địa chỉ IP</th>
                                <th>Trạng thái hoạt động</th>
                                <th>Trạng thái kết nối</th>
                                <th>Dich vụ sử dụng</th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>
                            @if (count($device) > 0)
                                <tbody id="tbody">
                                    @foreach ($device as $item)
                                        <tr>

                                            <td>{{ $item->devicecode }}</td>
                                            <td>{{ $item->devicename }}</td>
                                            <td>{{ $item->addressip }}</td>
                                            @if ($item->activestatus == 0)
                                                <td> <i class="fa-solid fa-circle text-danger fs-6"></i> Ngưng hoạt động
                                                </td>
                                            @else
                                                <td><i class="fa-solid fa-circle text-success fs-6"> </i> Hoạt động</td>
                                            @endif

                                            @if ($item->connectionstatus == 0)
                                                <td> <i class="fa-solid fa-circle text-danger fs-6"></i> Mất kết nối
                                                </td>
                                            @else
                                                <td><i class="fa-solid fa-circle text-success fs-6"></i> Kết nối</td>
                                            @endif

                                            <td>
                                                <div class="box-container">
                                                    {{-- {{ Str::limit($item, 18, '...') }} --}}

                                                    <div class="content">

                                                        @foreach ($item->tags1 as $tagitem)
                                                            <span>{{ $tagitem->devicename . ' ,' }}</span>
                                                        @endforeach


                                                    </div>
                                                    <p class="btn-detail">Xem thêm</p>
                                                </div>
                                            </td>
                                            <td><a href="{{ route('device.show', $item->id) }}">Chi tiết</a></td>
                                            <td><a href="{{ route('device.edit', $item->id) }}">Cập
                                                    nhật</a></td>

                                        </tr>
                                    @endforeach
                                </tbody>
                            @endif
                        </table>



                    </div>
                </div>
            </div>
            <div class="content-add">

                <div class="add">
                    <a href="{{ route('device.create') }}">
                        <div class="btn-add">
                            <img srcset="{{ asset('./assets/images/add-square.png 1x') }}">
                            <span>Thêm thiết bị</span>
                        </div>
                    </a>
                </div>

            </div>
        </div>
        </div>
        {{-- <div class="pagination">

        </div> --}}
    </main>
</section>
@endsection
