<?php

namespace App\Http\Controllers\AuthLogin;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\LoginAdmin;
// use App\Models\User;
use Illuminate\Support\Facades\Auth;



class CheckLogin extends Controller
{
    public function getLogin()
    {
        return view('Auth.login');
    }
    public function handleLogin(Request $request)
    {
        $request->validate([
            'username' => 'required',
            'password' => 'required',
        ], [
            'username.required' => 'Tài khoản không được để trống',
            'password.required' => 'Mật khẩu không được để trống'
        ]);
        $arr = [
            'loginname' => $request->username,
            'password' => $request->password
        ];

        if (Auth::attempt($arr)) {
            // return redirect()->route('admin.forgot');
            return "dang nhap thanh cong";
        } else {
            return redirect()->route('admin.login')->with('error', 'Sai mật khẩu hoặc tên đăng nhập');
        }
    }

    public function getForgot()
    {

        return view('Auth.forgot_pas');
    }


    public function handleforgot(Request $request)
    {
        $forgLogin =  LoginAdmin::where('email', $request->email)->first();
        if ($forgLogin) {
            return redirect()->route('admin.reset');
        } else {
            return redirect()->route('admin.forgot')->with('error', 'Vui lòng nhập đúng email');
        }
    }
    public function getResPass()
    {
        return view('Auth.reset_pas');
    }

    public function handlerestpas(Request $request, LoginAdmin $loginAdmin)
    {


        $request->validate([

            'password' => 'required',
            'confirm_password' => 'required',
        ]);
        $pass = bcrypt($request->password);

        $loginAdmin->update(['password' => $pass]);
        return "thanh cong";
    }
}
