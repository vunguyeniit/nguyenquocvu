    <div class="left side-menu">
        <div class="sidebar-inner slimscrollleft">

            <!--- Sidemenu -->
            <div id="sidebar-menu">


                <div class="dropdown" id="setting-dropdown">
                    <ul class="dropdown-menu">
                        <li><a href="javascript:void(0)"><i class="mdi mdi-face-profile m-r-5"></i> Profile</a></li>
                        <li><a href="javascript:void(0)"><i class="mdi mdi-account-settings-variant m-r-5"></i>
                                Settings</a></li>
                        <li><a href="javascript:void(0)"><i class="mdi mdi-lock m-r-5"></i> Lock screen</a></li>
                        <li><a href="javascript:void(0)"><i class="mdi mdi-logout m-r-5"></i> Logout</a></li>
                    </ul>
                </div>

                <ul>
                    <li class="menu-title">Navigation</li>

                    <li class="has_sub">
                        <a href="{{ route('dasboard') }}" class="waves-effect"><i
                                class="mdi mdi-view-dashboard"></i><span>
                                Dashboard </span> </a>
                    </li>

                    <li class="has_sub">
                        <a href="{{ route('location.list') }}" class="waves-effect"><i
                                class="mdi mdi-view-dashboard"></i><span>
                                Locations </span> </a>
                    </li>

                    <li class="has_sub">
                        <a href="{{ route('user') }}" class="waves-effect"><i class="mdi mdi-view-dashboard"></i><span>
                                Users & Roles </span> </a>
                    </li>

                    <li class="has_sub">
                        <a href="{{ route('asset') }}" class="waves-effect"><i class="mdi mdi-view-dashboard"></i><span>
                                Assets </span> </a>
                    </li>
                </ul>
            </div>
            <!-- Sidebar -->
            <div class="clearfix"></div>


        </div>
        <!-- Sidebar -left -->

    </div>
