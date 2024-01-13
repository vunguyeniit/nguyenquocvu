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
                                           <span>FORGOT YOUR PASSWORD ?</span>
                                       </a>
                                   </h2>
                                   <!--<h4 class="text-uppercase font-bold m-b-0">Sign In</h4>-->
                               </div>
                               <div class="account-content">
                                   <div class="text-center m-b-20">
                                       <p class="text-muted m-b-0 font-13">Enter your email address and we'll send you an
                                           email with instructions to reset your password. </p>
                                   </div>
                                   <form class="form-horizontal" action="{{ route('forgot.sendmail') }}" method="POST">
                                       @csrf
                                       <div class="form-group">
                                           <div class="col-xs-12">
                                               <input class="form-control" type="email" name="email"
                                                   placeholder="Enter email" value="{{ old('email') }}">
                                               @error('email')
                                                   <small style="color:#E73F3F">{{ $message }}</small>
                                               @enderror
                                           </div>
                                       </div>
                                       <div class="form-group">
                                           <div id="g_recaptcha_response">
                                           </div>
                                           @error('g-recaptcha-response')
                                               <small style="color:#E73F3F">{{ $message }}</small>
                                           @enderror
                                       </div>

                                       <div class="form-group account-btn text-center m-t-10">
                                           <div class="col-xs-12">
                                               <button class="btn w-md btn-bordered btn-danger waves-effect waves-light"
                                                   type="submit">RESET
                                               </button>
                                           </div>
                                       </div>

                                   </form>

                                   <div class="clearfix"></div>

                               </div>
                           </div>
                           <!-- end card-box-->


                           <div class="row m-t-50">
                               <div class="col-sm-12 text-center">
                                   <p class="text-muted">GO BACK<a href="{{ route('login') }}"
                                           class="text-primary m-l-5"><b>LOGIN</b></a></p>
                               </div>
                           </div>

                       </div>
                       <!-- end wrapper -->

                   </div>
               </div>
           </div>
       </section>
   @endsection
