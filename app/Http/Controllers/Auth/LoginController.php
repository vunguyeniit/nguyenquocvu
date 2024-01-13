<?php

namespace App\Http\Controllers\Auth;

use Illuminate\Http\Request;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Session;
use Illuminate\Support\Facades\Validator;

class LoginController extends Controller
{
    public function index(){
        
        return view('auth.layout.login');
    }
       public function storeLogin(Request $request){
       
    //  dd($request->session()->getId());

     $validator = Validator::make($request->all(), [
           'email' => ['required'],
            'password' =>['required'],
        ],[
            'email.required' => 'Email không được để trống',
            'password.required' => 'Mật khẩu không được để trống'
        ]);
        $arr = [
            'email' => $request->email,
            'password' => $request->password,
            'is_activated' => 1
        ];
          if ($validator->fails()) {
           return back()->withErrors($validator->errors());
          }
        if (Auth::attempt($arr)) {
            $request->session()->regenerate();
            $user = Auth::user();
            Session::put('user', $user->name);

            return redirect()->intended('dasboard')->with(
            'success','Đăng nhập thành công');
        }else{
             return back()->withErrors(['auth'=>"Tài khoản hoặc mật khẩu không khớp"])->withInput(); 
        }
       
       
    }
        public function logout(Request $request, $id=null){
            Auth::logout();
            $request->session()->invalidate();
            $request->session()->regenerateToken();
            //dd($request->session()->token());
            return redirect()->route('login');
        }
       
    
}
