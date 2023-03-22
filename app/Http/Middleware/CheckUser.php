<?php

namespace App\Http\Middleware;

use Closure;
use Illuminate\Http\Request;
use Auth;

class CheckUser
{

    public function handle(Request $request, Closure $next)
    {
        //Kiểm tra nếu chưa đăng nhập không vào được trang index
        if (Auth::guest()) {
            return redirect()->route('admin.login');
        }
        return $next($request);
    }
}