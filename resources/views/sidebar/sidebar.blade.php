 <!-- SIDEBAR -->
 <section id="sidebar">
     <div class="image">
         <img srcset="{{ asset('./assets/images/Logo_alta.png 3x') }}">
     </div>
     <ul class="side-menu">
         <li><a href="{{ route('das.index') }}"><img srcset="{{ asset('./assets/images/element-4.png 1.5x') }}">Dashboard</a></li>
         <li><a href="{{ route('device.index') }}"><img srcset="{{ asset('./assets/images/monitor.png 1.5x') }}">Thiết
                 bị</a></li>
         <li><a href="{{ route('service.index') }}"><img srcset="{{ asset('./assets/images/Frame-2.png 1.5x') }}">Dịch
                 vụ</a></li>
         <li><a href="{{ route('nublevel.index') }}"><img srcset="{{ asset('./assets/images/fi_layers.png 1.5x') }}">Cấp
                 số</a></li>
         <li><a href="{{ route('report.index') }}"><img srcset="{{ asset('./assets/images/Frame.png 1.5x') }}">Báo
                 cáo</a></li>
         <li>
             <a href="#"><img srcset="{{ asset('./assets/images/setting.png 1.5x') }}">Cài
                 đặt hệ thống<i class='bx bx-chevron-right icon-right'></i></a>
             <ul class="side-dropdown">
                 <li><a href="{{ route('role.index') }}">Quản lý vai trò</a></li>
                 <li><a href="{{ route('account.index') }}">Quản lý tài khoản</a></li>
                 <li><a href="{{ route('diary.index') }}">Nhật ký người dùng</a></li>

             </ul>

         </li>
         <div class="btn-sidebar">
             <div class="btn-logout">
                 <i class="fa-solid fa-arrow-right-from-bracket"></i>
                 <li><a href="#">Đăng xuất</a></li>
             </div>

         </div>
     </ul>


 </section>
 <!-- SIDEBAR -->
