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
                        <a href="{{ route('role.create') }}"><button type="button"
                                class="btn btn-custom waves-light waves-effect w-md ">New
                                Roles</button></a>

                    </div>
                </div>

                <!-- end row -->


                <div class="row">


                    <!-- end col -->

                    <div class="col-md-12">
                        <div class="panel panel-color panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Roles</h3>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <table class="table table table-hover m-0">
                                        <thead>
                                            <tr>
                                                <th>ID</th>
                                                <th>NAME</th>
                                                <th>DESCRICPTION</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            @foreach ($get_role_all as $get_role_alls)
                                                <tr>
                                                    <td>{{ $get_role_alls->id }}</td>
                                                    <td>{{ $get_role_alls->role_name }}</td>
                                                    <td>{{ $get_role_alls->description }}</td>

                                                    <td> <a href="{{ route('role.edit', $get_role_alls->id) }}">
                                                            <button
                                                                class="btn btn-primary waves-effect w-xs waves-light m-b-5">Edit</button>
                                                        </a></td>

                                                    <form action="{{ route('role.copy', $get_role_alls->id) }}"
                                                        method="post">
                                                        @csrf
                                                        <td><button type="submit"
                                                                class="btn btn-purple waves-effect w-xs waves-light m-b-5">Copy</button>
                                                        </td>
                                                    </form>

                                                    <form action="{{ route('role.delete', $get_role_alls->id) }}"
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
