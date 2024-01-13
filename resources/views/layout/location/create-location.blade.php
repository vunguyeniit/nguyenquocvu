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

                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <!-- end col -->
                    <div class="col-md-12">
                        <div class="panel panel-color panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">New Location</h3>
                            </div>
                            <div class="row">
                                {{-- form --}}
                                <div class="col-md-11">
                                    <form class="form-horizontal" action="{{ route('location.store') }}" method="POST">
                                        @csrf
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Location Name</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control " name="location_name"
                                                    placeholder="Location Name" value="{{ old('location_name') }}">
                                                @error('location_name')
                                                    <small class="text-danger">{{ $message }}</small>
                                                @enderror
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Notes</label>
                                            <div class="col-md-10">
                                                <textarea class="form-control" rows="3" name="notes" placeholder='Notes'>Notes</textarea>
                                                @error('notes')
                                                    <small class="text-danger">{{ $message }}</small>
                                                @enderror
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Department</label>
                                            <div class="col-md-10">

                                                <select name="department_id" class="selectpicker" data-style="btn-default">
                                                    @foreach ($get_department as $get_departments)
                                                        <option value="{{ $get_departments->id }}">
                                                            {{ $get_departments->department_name }}</option>
                                                    @endforeach

                                                </select>

                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Building</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" name="building"
                                                    placeholder="Building" value="{{ old('building') }}">
                                                @error('building')
                                                    <small class="text-danger">{{ $message }}</small>
                                                @enderror
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Street Address</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" name="street_address"
                                                    placeholder="Street Address" value="{{ old('street_address') }}">
                                                @error('street_address')
                                                    <small class="text-danger">{{ $message }}</small>
                                                @enderror
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">City</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" placeholder="City" name="city"
                                                    value="{{ old('city') }}">
                                                @error('city')
                                                    <small class="text-danger">{{ $message }}</small>
                                                @enderror
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">State</label>
                                            <div class="col-md-10">
                                                <select name="state" class="selectpicker" data-style="btn-default">
                                                    <option value="1">Enable</option>
                                                    <option value="0">Disable</option>
                                                </select>

                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Country</label>
                                            <div class="col-md-10">
                                                <select name="country" class="selectpicker" data-style="btn-default">
                                                    <option>Country</option>
                                                    <option value="VietNames">VietNames</option>
                                                </select>
                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Zip Code</label>
                                            <div class="col-md-10">
                                                <input type="number" id="zip_code" class="form-control "
                                                    placeholder="Zip Code" name="zip_code" value="{{ old('zip_code') }}">
                                                @error('zip_code')
                                                    <small class="text-danger">{{ $message }}</small>
                                                @enderror
                                            </div>
                                        </div>

                                        <div class="btn-submit text-center">
                                            <button type="submit"
                                                class="btn btn-custom waves-light waves-effect w-lg m-b-10 m-r-10">CREATE
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
