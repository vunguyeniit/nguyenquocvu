@extends('auth.auth-master')
@section('auth')
    <section>
        <div class="container-alt">
            <div class="row">

                {{-- Hiện thị lỗi đăng nhập --}}
                @error('auth')
                    <div class="alert alert-icon alert-danger alert-dismissible fade in" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                        <i class="mdi mdi-information"></i>
                        <strong></strong>{{ $message }}
                    </div>
                @enderror
                <div class="col-sm-12">

                    <div class="wrapper-page">

                        <div class="m-t-40 account-pages">
                            <div class="text-center account-logo-box">
                                <h2 class="text-uppercase">
                                    <a href="index.html" class="text-success">
                                        <span>LOGIN TO YOUR ACCOUNT</span>
                                    </a>
                                </h2>
                                <!--<h4 class="text-uppercase font-bold m-b-0">Sign In</h4>-->
                            </div>
                            <div class="account-content">
                                <form class="form-horizontal" action="{{ route('store.login') }}" method="POST">
                                    @csrf
                                    <div class="form-group ">
                                        <div class="col-xs-12">
                                            <input class="form-control" name="email" type="email" placeholder="EMAIL"
                                                value={{ old('email') }}>
                                            @error('email')
                                                <small style="color:#E73F3F">{{ $message }}</small>
                                            @enderror
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-xs-12">
                                            <input class="form-control" type="password" name="password"
                                                placeholder="PASSWORD" value={{ old('password') }}>
                                            @error('password')
                                                <small style="color:#E73F3F">{{ $message }}</small>
                                            @enderror
                                        </div>
                                    </div>



                                    <div class="form-group text-center m-t-30">
                                        <div class="col-sm-12">
                                            <a href="{{ route('forgot.password') }}" class="text-muted"><i
                                                    class="fa fa-lock m-r-5"></i> Forgot your password?</a>
                                        </div>
                                    </div>

                                    <div class="form-group account-btn text-center m-t-10">
                                        <div class="col-xs-12">
                                            <button class="btn w-md btn-bordered btn-danger waves-effect waves-light"
                                                type="submit">LOGIN</button>
                                        </div>
                                    </div>

                                </form>

                                <div class="clearfix"></div>

                            </div>
                        </div>
                        <!-- end card-box-->


                        <div class="row m-t-50">
                            <div class="col-sm-12 text-center">
                                <p class="text-muted">OR <a href="{{ route('sign') }}" class="text-primary m-l-5"><b>CREATE
                                            ACCOUNT</b></a></p>
                            </div>
                        </div>

                    </div>
                    <!-- end wrapper -->

                </div>
            </div>
        </div>
    </section>
@endsection
