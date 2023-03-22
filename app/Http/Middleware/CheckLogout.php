<?php

namespace App\Http\Middleware;

use Closure;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Session;
use Illuminate\Support\Facades\Auth;

class CheckLogout
{
    //Kiểm tra có tồn tại đăng nhập không được quay về trang login

    public function handle(Request $request, Closure $next)
    {
        if (Auth::check()) {
            return redirect()->route('admin.indexLogin');
        }
        return $next($request);
    }
}
