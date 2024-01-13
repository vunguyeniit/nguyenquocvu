@extends('layout.master')
@section('content')
    <div class="content-page">

        <!-- Start content -->
        <div class="content">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="page-title-box">

                            <ol class="breadcrumb p-0 m-0">
                                <li>
                                    <a href="{{ route('user') }}">Users</a>
                                </li>
                                <li>
                                    <a href="{{ route('role.list') }}">Roles </a>
                                </li>

                            </ol>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <!-- end col -->
                    <div class="col-md-12">
                        <div class="panel panel-color panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">New User</h3>
                            </div>
                            <div class="row">
                                {{-- form --}}
                                <div class="col-md-11">
                                    <form class="form-horizontal" action="{{ route('user.store') }}" method="POST">
                                        @csrf
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Name</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" value=""
                                                    placeholder=" Name" name="name">
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Email</label>
                                            <div class="col-md-10">
                                                <input type="email" class="form-control" value=""
                                                    placeholder="Email" name="email">
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Phone Number</label>
                                            <div class="col-md-10">
                                                <input type="number" class="form-control" value=""
                                                    placeholder="Phone Number"name="phone">
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Role</label>
                                            <div class="col-md-10">
                                                <select class="selectpicker" data-style="btn-default" name="role_id[]">
                                                    @foreach ($get_role as $get_roles)
                                                        <option value="{{ $get_roles->id }}">{{ $get_roles->role_name }}
                                                        </option>
                                                    @endforeach
                                                </select>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Location</label>
                                            <div class="col-md-10">
                                                <select id="select" class="selectpicker" data-style="btn-default"
                                                    name="location_id">
                                                    <option value="0" selected>Tất cả</option>
                                                    @foreach ($get_location as $get_locations)
                                                        <option value="{{ $get_locations->id }}">
                                                            {{ $get_locations->location_name }}</option>
                                                    @endforeach
                                                </select>

                                            </div>

                                        </div>


                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Department</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" value=""
                                                    placeholder="Department" id='department' readonly>
                                            </div>
                                        </div>




                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Building</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" value=""
                                                    placeholder="Building"id="building" readonly>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">STREET ADDRESS</label>
                                            <div class="col-md-10">
                                                <input type="text" value="" id="streetAddress" class="form-control"
                                                    placeholder="STREET ADDRESS"readonly>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">STATE</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" placeholder="STATE"
                                                    id="state"readonly>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">CITY</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" placeholder="CITY"id="city"
                                                    readonly>
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Country</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" placeholder="Country"
                                                    id="country"readonly>
                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">ZIP CODE</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" placeholder="ZIP CODE"
                                                    id="zip_code"readonly>
                                            </div>
                                        </div>



                                </div>
                                {{-- end form --}}

                            </div>
                            <div class="btn-submit text-center">
                                <button type="submit"
                                    class="btn btn-custom waves-light waves-effect w-lg m-b-10 m-r-10">CREATE
                                    USER</button>
                                <button type="button"
                                    class="btn btn-brown waves-light waves-effect w-lg m-b-10 m-l-10">CANCEL</button>
                            </div>
                            </form>
                        </div>
                        <!-- end panel -->

                    </div>
                    <!-- end col -->

                </div>
                <!-- end row -->
            </div> <!-- container -->

        </div> <!-- content -->



        <footer class="footer text-right">
            2016 - 2018 © Zircos.
        </footer>

    </div>
@endsection
