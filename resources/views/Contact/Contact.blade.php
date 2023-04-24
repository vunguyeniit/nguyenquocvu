@extends('Layout.Master-Layout')
@section('content')
    <div class="content-contact">
        <div class="title-contact">
            <h2>Liên hệ</h2>
        </div>
        <div class="container">
            <div class="contact-left">

                <img srcset="{{ asset('./asset/images/Group.png 2.6x') }}" alt="">
                <div class="form-contact">
                    <div class="content-title">
                        <h4>Lorem ipsum dolor sit amet consectetur adipisicing elit. Blanditiis, fugiat accusamus laboriosam
                            fugit quisquam temporibus exercitationem</h4>
                    </div>
                    <form action="{{ route('handleContact') }}" method="POST">
                        @csrf
                        <div class="form-text">
                            <div class="form-col-3">
                                <input type="text" name="username" placeholder="Tên">
                            </div>
                            <div class="form-col-6">
                                <input type="text" name="email" placeholder="Email">
                            </div>

                        </div>


                        <div class="form-text">
                            <div class="form-col-3">
                                <input type="text" name="phone" placeholder="Số điện thoại">
                            </div>
                            <div class="form-col-6">
                                <input type="text" name="address" placeholder="Địa chỉ">
                            </div>

                        </div>


                        <div class="form-text">
                            <textarea placeholder="Lời nhắn..." name="description" rows="2" cols="120"></textarea>
                        </div>
                        <div class="form-text">
                            <button type="submit">Gửi liên hệ</button>
                        </div>
                    </form>
                </div>


            </div>

            <div class="contact-right">
                <div class="contact">
                    <img srcset="{{ asset('./asset/images/contact.png 2.9x') }}" alt="">
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
                    <img srcset="{{ asset('./asset/images/contact.png 2.9x') }}" alt="">
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
                    <img srcset="{{ asset('./asset/images/contact.png 2.9x') }}" alt="">
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
    {{-- MODEL --}}



    <!-- Modal -->


    @if (session('success'))
        <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
            aria-labelledby="staticBackdropLabel" aria-hidden="false">
            <div class="modal-dialog modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header" style="border-bottom:none">

                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">

                        <p class="fs-4">Gửi liên hệ thành công.<br>Vui lòng kiên nhẫn đợi phản hồi từ<br> chúng tôi,bạn
                            nhé!</p>
                    </div>


                </div>
            </div>
        </div>
    @endif
    {{-- EndModel --}}
@endsection
