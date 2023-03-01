 <!-- SIDEBAR -->
 <section id="sidebar">
     <div class="image">
         <img srcset="{{ asset('./assets/images/Logo_alta.png 3x') }}">
     </div>
     <ul class="side-menu">
         <li><a href="#"><img srcset="{{ asset('./assets/images/element-4.png 1.5x') }}"></i>Dashboard</a></li>
         <li><a href="{{ route('device.index') }}"><img srcset="{{ asset('./assets/images/monitor.png 1.5x') }}"></i>Thiết
                 bị</a></li>
         <li><a href="{{ route('service.index') }}"><img srcset="{{ asset('./assets/images/Frame-2.png 1.5x') }}"></i>Dịch
                 vụ</a></li>
         <li><a href="{{ route('nublevel.index') }}"><img
                     srcset="{{ asset('./assets/images/fi_layers.png 1.5x') }}"></i>Cấp số</a></li>
         <li><a href="#"><img srcset="{{ asset('./assets/images/Frame.png 1.5x') }}"></i>Báo cáo</a></li>
         <li>
             <a href="#"><img srcset="{{ asset('./assets/images/setting.png 1.5x') }}">Cài đặt hệ thống<i
                     class='bx bx-chevron-right icon-right'></i></a>
             <ul class="side-dropdown">
                 <li><a href="#">Basic</a></li>
                 <li><a href="#">Select</a></li>
                 <li><a href="#">Checkbox</a></li>
                 <li><a href="#">Radio</a></li>
             </ul>
         </li>
     </ul>
 </section>
 <!-- SIDEBAR -->
