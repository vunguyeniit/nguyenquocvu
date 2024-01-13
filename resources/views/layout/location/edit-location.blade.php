@extends('layout.master')
@section('content')
    <div class="content-page">

        <!-- Start content -->
        <div class="content">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="page-title-box">
                            <h4 class="page-title">Location </h4>
                            <ol class="breadcrumb p-0 m-0">
                                <li>
                                    <a href="#">Zircos</a>
                                </li>
                                <li>
                                    <a href="#">Dashboard </a>
                                </li>
                                <li class="active">
                                    Dashboard 2
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
                                <h3 class="panel-title">Edit Location</h3>
                            </div>
                            <div class="row">
                                {{-- form --}}
                                <div class="col-md-11">
                                    <form class="form-horizontal" action="{{ route('location.update', $get_location->id) }}"
                                        method="POST">
                                        @csrf
                                        @method('PUT');
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Location Name</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" name="location_name"
                                                    placeholder="Location Name" value="{{ $get_location->location_name }}">
                                                @error('location_name')
                                                    <small style="color:#E73F3F">{{ $message }}</small>
                                                @enderror
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Notes</label>
                                            <div class="col-md-10">
                                                <textarea class="form-control" rows="3" name="notes" placeholder='Notes'>{{ $get_location->notes }}</textarea>
                                                @error('notes')
                                                    <small style="color:#E73F3F">{{ $message }}</small>
                                                @enderror
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Department</label>
                                            <div class="col-md-10">

                                                <select name="department_id" class="selectpicker" data-style="btn-default">
                                                    @foreach ($get_department as $get_departments)
                                                        <option value="{{ $get_departments->id }}"
                                                            {{ $get_departments->id == $get_location->department_id ? 'selected' : '' }}>
                                                            {{-- @selected(old('department_id') == $get_departments->id)> --}}
                                                            {{ $get_departments->department_name }}</option>
                                                    @endforeach
                                                </select>

                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Building</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" name="building"
                                                    placeholder="Building" value="{{ $get_location->building }}">
                                                @error('building')
                                                    <small style="color:#E73F3F">{{ $message }}</small>
                                                @enderror
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Street Address</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" name="street_address"
                                                    placeholder="Street Address"
                                                    value="{{ $get_location->street_address }}">
                                                @error('street_address')
                                                    <small style="color:#E73F3F">{{ $message }}</small>
                                                @enderror
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">City</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" placeholder="City" name="city"
                                                    value="{{ $get_location->city }}">
                                                @error('city')
                                                    <small style="color:#E73F3F">{{ $message }}</small>
                                                @enderror
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">State</label>
                                            <div class="col-md-10">
                                                <select name="state" class="selectpicker" data-style="btn-default">
                                                    <option value="1" @selected(old('state') == $get_location->state)>
                                                        Enable</option>
                                                    <option value="0" @selected(old('state') == $get_location->state)>
                                                        Disable</option>
                                                </select>

                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Country</label>
                                            <div class="col-md-10">
                                                <select name="country" class="selectpicker" data-style="btn-default">
                                                    <option>Country</option>
                                                    <option value="VietNames" selected>VietNames</option>
                                                </select>
                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Zip Code</label>
                                            <div class="col-md-10">
                                                <input type="number" class="form-control" placeholder="Zip Code"
                                                    name="zip_code" value="{{ $get_location->zip_code }}">
                                                @error('zip_code')
                                                    <small style="color:#E73F3F">{{ $message }}</small>
                                                @enderror
                                            </div>
                                        </div>

                                        <div class="btn-submit text-center">
                                            <button type="submit"
                                                class="btn btn-custom waves-light waves-effect w-lg m-b-10 m-r-10">EDIT
                                                LOCATION</button>
                                            <button type="submit"
                                                class="btn btn-brown waves-light waves-effect w-lg m-b-10 m-l-10">CANCEL</button>
                                        </div>

                                    </form>
                                </div>
                                {{-- end form --}}

                            </div>

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
