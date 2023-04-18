@extends('Layout.Master-Layout')
@section('content')
    <div class="content-contact">
        <div class="title-contact">
            <h2>Liên hệ</h2>
        </div>
        <div class="container">
            <div class="contact-left">

                <img src="{{ asset('./asset/images/Group.png') }}" alt="">
                <div class="form-contact">
                    <div class="content-title">
                        <h4>Lorem ipsum dolor sit amet consectetur adipisicing elit. Blanditiis, fugiat accusamus laboriosam
                            fugit quisquam temporibus exercitationem</h4>
                    </div>
                    <form action="">
                        <div class="form-text">
                            <div class="form-col-3">
                                <input type="text" placeholder="Tên">
                            </div>
                            <div class="form-col-6">
                                <input type="text" placeholder="Email">
                            </div>

                        </div>


                        <div class="form-text">
                            <div class="form-col-3">
                                <input type="text" placeholder="Số điện thoại">
                            </div>
                            <div class="form-col-6">
                                <input type="text" placeholder="Địa chỉ">
                            </div>

                        </div>


                        <div class="form-text">

                            <textarea placeholder="Lời nhắn..." name="w3review" rows="2" cols="120"></textarea>
                        </div>
                        <div class="form-text">

                            <a href="http://">Gửi liên hệ</a>
                        </div>
                    </form>
                </div>


            </div>

            <div class="contact-right">
                <div class="contact">
                    <img srcset="{{ asset('./asset/images/contact.png 3x') }}" alt="">
                    <div class="list-contact">
                        <div class="icon">
                            <img class="icon-contact" src="{{ asset('./asset/images/address.png') }}" alt="">
                        </div>

                        <div class="content-contact">
                            <h2>Địa chỉ</h2>
                            <p>86/33 Âu cơ, Phường 9, Quận Tân Bình, TP.Hồ Chí Minh </p>
                        </div>
                    </div>
                </div>
                <div class="contact">
                    <img srcset="{{ asset('./asset/images/contact.png 3x') }}" alt="">
                    <div class="list-contact">
                        <div class="icon">
                            <img class="icon-contact" src="{{ asset('./asset/images/mail.png') }}" alt="">
                        </div>

                        <div class="content-contact">
                            <h2>Email</h2>
                            <p>contact@gmail.com </p>
                        </div>
                    </div>
                </div>
                <div class="contact">
                    <img srcset="{{ asset('./asset/images/contact.png 2.7x') }}" alt="">
                    <div class="list-contact">
                        <div class="icon">
                            <img class="icon-contact" src="{{ asset('./asset/images/phone.png') }}" alt="">
                        </div>

                        <div class="content-contact">
                            <h2>Điện thoại</h2>
                            <p>+84 123 456 789 </p>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>
    <div class="icon-img">
        <img srcset="{{ asset('./asset/images/Alex_AR.png 2.5x') }}" alt="">
    </div>
@endsection
