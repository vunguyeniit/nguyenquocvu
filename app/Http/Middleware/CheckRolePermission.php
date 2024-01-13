<?php

namespace App\Http\Middleware;

use Closure;
use App\Models\Roles;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Symfony\Component\HttpFoundation\Response;

class CheckRolePermission
{
    /**
     * Handle an incoming request.
     *
     * @param  \Closure(\Illuminate\Http\Request): (\Symfony\Component\HttpFoundation\Response)  $next
     */
    public function handle(Request $request, Closure $next,$requiredPermission): Response
    {
             if (Auth::check()) {
            // Lấy danh sách quyền của người dùng từ cột permissions trong bảng role
            $userPermissions = Auth::user()->roles->value('permission_name');
            // Chuyển đổi chuỗi JSON thành mảng
            $userPermissionsArray = json_decode($userPermissions, true);
            
            // Kiểm tra xem người dùng có quyền truy cập không
            if (!in_array($requiredPermission, $userPermissionsArray)) {
                  return abort(403, 'Unauthorized');
            } else {
               
                 return $next($request);
            }
        }
        // Người dùng chưa đăng nhập, xử lý tùy chọn (chẳng hạn chuyển hướng hoặc trả về lỗi)
        return redirect()->route('login');
           

        }

       
      
    
    
}
