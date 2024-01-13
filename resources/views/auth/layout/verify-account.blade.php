@extends('auth.auth-master')
@section('auth')
    <section>
        <div class="container-alt">
            <div class="row">

                <div class="col-sm-12">

                    <div class="wrapper-page">

                        <div class="m-t-40 account-pages">
                            <div class="text-center account-logo-box">
                                <h2 class="text-uppercase">
                                    <a href="index.html" class="text-success">
                                        <span>VERIFY ACCOUNT</span>
                                    </a>
                                </h2>
                                <!--<h4 class="text-uppercase font-bold m-b-0">Sign In</h4>-->
                            </div>
                            <div class="account-content">
                                <form class="form-horizontal" action="{{ route('verify.activate') }}" method="POST">
                                    @csrf
                                    <div class="form-group ">
                                        <div class="col-xs-12">
                                            <input class="form-control" name="token" type="number" required=""
                                                placeholder="ENTER CODE">
                                        </div>
                                    </div>

                                    <div class="form-group account-btn text-center m-t-10">
                                        <div class="col-xs-12">
                                            <button class="btn w-md btn-bordered btn-danger waves-effect waves-light"
                                                type="submit">VERIFY</button>
                                        </div>
                                    </div>

                                </form>

                                <div class="clearfix"></div>

                            </div>
                        </div>
                        <!-- end card-box-->




                    </div>
                    <!-- end wrapper -->

                </div>
            </div>
        </div>
    </section>
@endsection
