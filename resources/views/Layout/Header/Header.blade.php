<header>
    <nav>
        <ul>
            <img srcset="{{ asset('./asset/images/Logo.png 3x') }}">
            <div class="nav-list">
                <li class="{{ Request::is('/') ? 'active' : '' }}"><a class="select" href="{{ route('home') }}">Trang
                        Chủ</a></li>
                <li class="{{ Request::is('event') ? 'active' : '' }}"><a class="select" href="{{ route('event') }}">Sự
                        Kiện</a></li>
                <li class="{{ Request::is('contact') ? 'active' : '' }}"><a class="select"
                        href="{{ route('contact') }}">Liên Hệ</a></li>
            </div>
            <div class="right">
                <img srcset="{{ asset('./asset/images/logo_phone.png 2x') }}">
                <li>0938342942</li>
            </div>

        </ul>
    </nav>
</header>
