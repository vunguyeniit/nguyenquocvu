@extends('layout.master')
@section('content')
    <div class="content-page">
        <!-- Start content -->
        <div class="content">
            <div class="container">
                <div class="row">

                    <div class="col-xs-12">
                        <div class="page-title-box">
                            <h4 class="page-title">Location</h4>

                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
                <div class="" style="display:flex;align-items:center">
                    <div class="row col-md-8 p-0">
                        <form action="{{ route('search.location') }}" method="get">
                            @csrf

                            <div class="col-md-10 m-b-10">
                                <div class="input-group m-t-10">

                                    <input type="text" class="form-control" name="search" placeholder="Search">
                                    <span class="input-group-btn">
                                        <button type="submit" class="btn waves-effect waves-light btn-custom"><i
                                                class="fa fa-search m-r-5"></i> Search</button>
                                    </span>

                                </div>
                            </div>
                        </form>
                    </div>
                    <div class=" col-md-5 m-r-5 p-0 text-right">
                        <a href="{{ route('location.create') }}"><button type="button"
                                class="btn btn-custom waves-light waves-effect w-md ">New
                                Location</button></a>
                        <button class="btn btn-custom waves-effect waves-light" data-toggle="modal"
                            data-target="#custom-width-modal">Import Locations</button>
                    </div>

                </div>
                <!-- end row -->
                <div class="row">
                    <!-- end col -->
                    <div class="col-md-12">
                        <div class="panel panel-color panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Location</h3>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <table class="table table table-hover m-0">
                                        <thead>
                                            <tr>
                                                <th>NO</th>
                                                <th>NAME</th>
                                                <th>ADDRESS</th>
                                                <th>NOTES</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            @foreach ($get_location as $get_locations)
                                                <tr>
                                                    <th>
                                                        <p class="p-0">{{ $get_locations->id }}</p>
                                                    </th>
                                                    <td>
                                                        <p class="p-0">{{ $get_locations->location_name }}</p>
                                                        <p class="p-0">{{ $get_locations->department->department_name }}
                                                        </p>
                                                    </td>
                                                    <td>
                                                        <p class="p-0">
                                                            {{ $get_locations->building }},
                                                            {{ $get_locations->street_address }},
                                                            {{ $get_locations->city }},<br>
                                                            {{ $get_locations->country }},
                                                            {{ $get_locations->zip_code }}
                                                        </p>
                                                    </td>
                                                    <td>
                                                        <p class="p-0"> {{ $get_locations->notes }}</p>
                                                    </td>

                                                    <td> <a href="{{ route('location.edit', $get_locations->id) }}">
                                                            <button
                                                                class="btn btn-primary waves-effect w-xs waves-light m-b-5">Edit</button>
                                                        </a></td>

                                                    <form action="{{ route('location.copy', $get_locations->id) }}"
                                                        method="post">
                                                        @csrf
                                                        <td><button type="submit"
                                                                class="btn btn-purple waves-effect w-xs waves-light m-b-5">Copy</button>
                                                        </td>
                                                    </form>

                                                    <form action="{{ route('location.delete', $get_locations->id) }}"
                                                        method="post">
                                                        @csrf
                                                        @method('delete')
                                                        <td><button type="submit"
                                                                class="btn btn-danger waves-effect waves-light w-xs m-b-5">Delete</button>
                                                        </td>
                                                    </form>
                                                </tr>
                                            @endforeach

                                        </tbody>


                                    </table>
                                    <div class="text-right">
                                        {{ $get_location->links() }}\
                                    </div>
                                </div> <!-- table-responsive -->
                            </div> <!-- end panel body -->
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
        {{-- Modal --}}

        <div id="custom-width-modal" class="modal fade in" tabindex="-1" role="dialog"
            aria-labelledby="custom-width-modalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog" style="width:55%;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title" id="custom-width-modalLabel">IMPORT LOCATIONS (Popup Modal)</h4>
                    </div>
                    <form action="{{ route('import.location') }}" method="post" enctype="multipart/form-data">
                        <div class="modal-body">
                            @csrf
                            <div class="form-group">
                                <label class="control-label">Upload File</label>

                                <input data-placeholder= "UPLOAD YOUR FILE"type="file" id='file' accept=".xlsx"
                                    name="file" class="filestyle" data-iconname="fa fa-cloud-upload">
                                @error('file')
                                    <small style="color:#E73F3F">{{ $message }}</small>
                                @enderror
                            </div>
                            <p>Accepted files (click to download sample)</p>
                            <ul>
                                <a href="{{ route('export.location', ['file_extension' => '.csv']) }}">
                                    <li class="text-custom">CSV (.csv)</li>
                                </a>
                                <a href="{{ route('export.location', ['file_extension' => '.xlsx']) }}">
                                    <li class="text-custom">EXCEL (.xlsx)</li>
                                </a>

                            </ul>
                        </div>
                        <div class="modal-footer">
                            <button type="submit" class="btn btn-primary waves-effect waves-light">IMPORT</button>
                            <button type="button" class="btn btn-brown waves-effect" data-dismiss="modal">CANCEL</button>
                        </div>
                    </form>
                </div><!-- /.modal-content -->
            </div><!-- /.modal-dialog -->
        </div>

        {{-- End Modal --}}
    @endsection
    {{-- text --}}
