@extends('layout.master')
@section('content')
    <div class="content-page">
        <!-- Start content -->
        <div class="content">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="page-title-box">
                            {{-- <h4 class="page-title">User</h4> --}}
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

                <div class="row d-flex align-center" style="display:flex;align-items:center">

                    <div class="col-md-6 m-b-10">
                        <div class="input-group m-t-10">
                            <input type="text" class="form-control" placeholder="Search">
                            <span class="input-group-btn">
                                <button type="button" class="btn waves-effect waves-light btn-custom"><i
                                        class="fa fa-search m-r-5"></i> Search</button>
                            </span>
                        </div>
                    </div>
                    <div class=" col-md-6 m-r-5 text-right">
                        <a href="{{ route('user.create') }}"><button type="button"
                                class="btn btn-custom waves-light waves-effect w-md ">New
                                User</button></a>

                    </div>
                </div>

                <!-- end row -->


                <div class="row">


                    <!-- end col -->

                    <div class="col-md-12">
                        <div class="panel panel-color panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Users</h3>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <table class="table table table-hover m-0">
                                        <thead>
                                            <tr>
                                                <th>ID</th>
                                                <th>NAME</th>
                                                <th>EMAIL</th>
                                                <th>PHONE</th>
                                                <th>LOCATION</th>
                                                <th>ROLE</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {{-- @foreach ($get_new_user as $get_new_users)
                                                <tr>
                                                    <td>{{ $get_new_users->id }}</td>
                                                    <td>{{ $get_new_users->name }}</td>
                                                    <td>{{ $get_new_users->email }}</td>
                                                    <td>{{ $get_new_users->phone }}</td>
                                                    <td>
                                                        <p class="p-0">{{ $get_new_users->location->location_name }}</p>
                                                        <p class="p-0">
                                                            {{ $get_new_users->location->department->department_name }}
                                                    </td>
                                                    <td>{{ $get_new_users->roles->role_name }}</td>
                                                </tr>
                                            @endforeach --}}




                                        </tbody>
                                    </table>

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

    </div>
@endsection
