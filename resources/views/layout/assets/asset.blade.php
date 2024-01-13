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
                                    <a href="">Asset</a>
                                </li>
                                <li>
                                    <a href="">Qr codes </a>
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
                        <a href=""><button type="button" class="btn btn-custom waves-light waves-effect w-md ">Print
                                Label</button></a>

                    </div>
                </div>

                <!-- end row -->


                <div class="row">


                    <!-- end col -->

                    <div class="col-md-12">
                        <div class="panel panel-color panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">List QR codes</h3>
                            </div>
                            <div class="panel-body">
                                {{-- <div class="table-responsive"> --}}
                                {{-- <table class="table table table-hover m-0">
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
                                            <tr>
                                                <td>1</td>
                                                <td>QUOCVU</td>
                                            </tr>
                                        </tbody>
                                    </table> --}}
                                <div class="card-box">
                                    <div class="row">
                                        <div class="col-lg-12">

                                            <div class="d-flex">
                                                <div class="col-lg-6">
                                                    <h6>12213</h6>
                                                    <h6>1221312213</h6>
                                                    <h6>12213</h6>
                                                    <h6>12213</h6>

                                                </div>
                                                <div class="col-lg-6 text-right">
                                                    <div style="padding: 0 1rem">
                                                        {{ QrCode::generate('Make me into a QrCode!') }}
                                                    </div>

                                                    <div class="">
                                                        <a href=""><button type="button"
                                                                class="btn btn-custom waves-light waves-effect w-md ">Change
                                                                Code</button></a>
                                                    </div>
                                                </div>

                                            </div>


                                        </div>
                                    </div>



                                </div>
                                {{-- </div> <!-- table-responsive --> --}}
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
