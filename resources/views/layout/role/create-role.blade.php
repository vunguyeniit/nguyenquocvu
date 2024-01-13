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
                                <h3 class="panel-title">New Roles</h3>
                            </div>
                            <div class="row">
                                {{-- form --}}
                                <div class="col-md-11">
                                    <form class="form-horizontal" action="{{ route('role.store') }}" method="POST">
                                        @csrf
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Name</label>
                                            <div class="col-md-10">
                                                <input type="text" class="form-control" name="role_name" value=""
                                                    placeholder=" Name">
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">DESCRIPTION</label>
                                            <div class="col-md-10">
                                                <textarea name='description' class="form-control" rows="3" placeholder='Notes'></textarea>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-2 control-label">MODULE</label>
                                            <div class="col-md-10">
                                                {{-- TABLE --}}
                                                <div class="col-md-12">
                                                    <div class="panel panel-color panel-info">

                                                        <div class="panel-body">
                                                            <div class="table-responsive">

                                                                <table class="table table table-hover m-0">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>MODULE</th>
                                                                            <th>NONE</th>
                                                                            <th>FULL</th>
                                                                            <th>SELECT</th>
                                                                            <th>VIEW</th>
                                                                            <th>CREATE</th>
                                                                            <th>EDIT</th>
                                                                            <th>DELETE</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>ROLE</td>
                                                                            <td>
                                                                                <div class="radio radio-info radio-single">
                                                                                    <input type="radio" class="role"
                                                                                        id="none" value="option2.1"
                                                                                        name="radioSingle1"
                                                                                        aria-label="Single radio Two"checked="">
                                                                                    <label></label>
                                                                                </div>
                                                                            </td>

                                                                            <td>
                                                                                <div class="radio radio-info radio-single">
                                                                                    <input type="radio" class="role"
                                                                                        id="full" value="option2.1"
                                                                                        name="radioSingle1"
                                                                                        aria-label="Single radio Two"
                                                                                        checked="">
                                                                                    <label></label>
                                                                                </div>
                                                                            </td>

                                                                            <td>
                                                                                <div class="radio radio-info radio-single">
                                                                                    <input type="radio" class="role"
                                                                                        id="select" value="option2.1"
                                                                                        name="radioSingle1"
                                                                                        aria-label="Single radio Two"checked="">
                                                                                    <label></label>
                                                                                </div>
                                                                            </td>
                                                                            @foreach ($filteredRoutes->take(-4) as $get_permissions)
                                                                                <td>
                                                                                    <div class="checkbox checkbox-custom">
                                                                                        <input id="checkbox1"
                                                                                            {{-- onclick="onCheckboxClick" --}}
                                                                                            type="checkbox"name="checkbox[]"
                                                                                            class="checkbox_role"
                                                                                            value="{{ $get_permissions }}">
                                                                                        <label for="checkbox1">

                                                                                        </label>
                                                                                    </div>
                                                                                </td>
                                                                            @endforeach
                                                                        </tr>

                                                                        <tr>
                                                                            <td>Location</td>
                                                                            <td>
                                                                                <div class="radio radio-info radio-single">
                                                                                    <input type="radio" id="none"
                                                                                        value="option2.1"class="location"
                                                                                        name="radioSingle11"
                                                                                        aria-label="Single radio Two"checked="">
                                                                                    <label></label>
                                                                                </div>
                                                                            </td>

                                                                            <td>
                                                                                <div class="radio radio-info radio-single">
                                                                                    <input type="radio" value="option2.1"
                                                                                        id="full" class="location"
                                                                                        name="radioSingle11"
                                                                                        aria-label="Single radio Two"checked="">
                                                                                    <label></label>
                                                                                </div>
                                                                            </td>

                                                                            <td>
                                                                                <div class="radio radio-info radio-single">

                                                                                    <input type="radio" value="option2.1"
                                                                                        id="select" class="location"
                                                                                        name="radioSingle11"
                                                                                        aria-label="Single radio Two"checked="">
                                                                                    <label></label>
                                                                                </div>
                                                                            </td>


                                                                            @foreach ($filteredRoutes->take(4) as $get_permissions)
                                                                                <td>
                                                                                    <div class="checkbox checkbox-custom">
                                                                                        <input id="checkbox1"
                                                                                            onclick="onCheckboxClick"
                                                                                            type="checkbox"name="checkbox[]"
                                                                                            class="checkbox_location"
                                                                                            value="{{ $get_permissions }}">
                                                                                        <label for="checkbox1">

                                                                                        </label>
                                                                                    </div>
                                                                                </td>
                                                                            @endforeach

                                                                        </tr>

                                                                    </tbody>
                                                                </table>

                                                            </div> <!-- table-responsive -->
                                                        </div> <!-- end panel body -->
                                                    </div>
                                                    <!-- end panel -->
                                                </div>
                                                {{-- END TABLE --}}
                                            </div>
                                        </div>
                                </div>
                                {{-- end form --}}
                            </div>
                            <div class="btn-submit text-center">
                                <button type="submit"
                                    class="btn btn-custom waves-light waves-effect w-lg m-b-10 m-r-10">CREATE
                                    ROLE</button>
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
