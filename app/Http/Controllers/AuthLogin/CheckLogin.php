<?php

namespace App\Http\Controllers\AuthLogin;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\LoginAdmin;

use Illuminate\Support\Str;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Mail;
use Illuminate\Support\Facades\Session;

class CheckLogin extends Controller
{

    public function getLogin()
    {
        return view('Auth.login');
    }
    public function handleLogin(Request $request)
    {
        $request->flash();
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
            return redirect()->route('admin.indexLogin');
        } else {

            return redirect()->route('admin.login')->with('error', 'Sai mật khẩu hoặc tên đăng nhập');
        }
    }

    public function indexLogin()
    {
        $user = Auth::user();

        return view('admin.account-user', compact('user'));
    }
    public function logout()
    {
        Auth::logout();
        return redirect()->route('admin.login');
    }

    public function getForgot()
    {

        return view('Auth.forgot_pas');
    }


    public function handleforgot(Request $request)
    {
        $request->flash();
        $email = $request->email;
        $forgLogin =  LoginAdmin::where('email', $request->email)->first();
        if (!$forgLogin) {
            return redirect()->route('admin.forgot')->with('error', 'Vui lòng nhập chính xác email');
        }
        $code = Str::random();
        // dd($code);
        $forgLogin->code = $code;
        $forgLogin->update();


        $url = route('admin.reset', ['code' => $code]);
        $data = [
            'route' => $url
        ];
        // dd($data);
        Mail::send('Auth.check-mail', $data, function ($message) use ($email) {
            $message->from($email, 'Reset Password');
            $message->subject('Reset Password');
            $message->to($email, 'Reset Password');
        });
        return redirect()->route('admin.forgot')->with('success', 'Vui lòng check lại mail để lấy lại mật khẩu');
    }

    public function getResPass(Request $request)
    {
        // $code = $request->code;
        $checkUser =  LoginAdmin::where('code', $request->code)->first();

        //dd($checkCode);
        if (!$checkUser) {
            return redirect()->route('admin.forgot')->with('error', 'Đường dẫn láy mật khẩu không đúng');
        } else {
            $code = $checkUser['code'];
            return view('Auth.reset_pas', compact('code'));
        }
    }

    public function handlerestpas(Request $request)
    {
        $checkCode =  LoginAdmin::where('code', '=', $request->code)->first();

        if ($request->password == $request->confirm_password) {

            // dd($checkCode);

            $checkCode1 = bcrypt($request->password);
            $checkCode->password = $checkCode1;
            $checkCode->save();
            return redirect()->route('admin.forgot')->with('success', 'Cập nhật mật khẩu thành công');
        } else {
            $code =  $checkCode['code'];
            return redirect()->route('admin.reset', ['code' => $code])->with('error', 'Mật khẩu không khớp với nhau');
        }
    }
}
