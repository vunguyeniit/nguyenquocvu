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
                                        <span>GET STARTED FREE</span>
                                    </a>
                                </h2>
                                <!--<h4 class="text-uppercase font-bold m-b-0">Sign In</h4>-->
                            </div>
                            <div class="account-content">
                                <form class="form-horizontal" action="{{ route('store.sign') }}" method="POST">
                                    @csrf
                                    <div class="form-group ">
                                        <div class="col-xs-12">
                                            <input class="form-control" name="name" type="text"
                                                placeholder="YOUR NAME">
                                            @error('name')
                                                <small style="color:#E73F3F">{{ $message }}</small>
                                            @enderror
                                        </div>
                                    </div>


                                    <div class="form-group ">
                                        <div class="col-xs-12">
                                            <input class="form-control" name="email" type="email" placeholder="EMAIL">
                                            @error('email')
                                                <small style="color:#E73F3F">{{ $message }}</small>
                                            @enderror
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <div class="col-xs-12">
                                            <input class="form-control" name="password" type="password"
                                                placeholder="PASSWORD">
                                            @error('password')
                                                <small style="color:#E73F3F">{{ $message }}</small>
                                            @enderror
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-xs-12">
                                            <input class="form-control" name="company" type="text" placeholder="COMPANY">
                                            @error('company')
                                                <small style="color:#E73F3F">{{ $message }}</small>
                                            @enderror
                                        </div>
                                    </div>


                                    <div class="form-group account-btn text-center m-t-10">
                                        <div class="col-xs-12">
                                            <button class="btn w-md btn-danger btn-bordered waves-effect waves-light"
                                                type="submit">SIGN UP FREE</button>
                                        </div>
                                    </div>
                                </form>

                                <div class="clearfix"></div>

                            </div>
                        </div>
                        <!-- end card-box-->


                        <div class="row m-t-50">
                            <div class="col-sm-12 text-center">
                                <p class="text-muted">OR<a href="{{ route('login') }}" class="text-primary m-l-5"><b>LOGIN
                                            TO
                                            YOUR ACCOUNT</b></a></p>
                            </div>
                        </div>

                    </div>
                    <!-- end wrapper -->

                </div>
            </div>
        </div>
    </section>
@endsection
