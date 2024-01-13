<?php

namespace App\Http\Controllers\Auth;

use App\Models\User;
use Illuminate\Support\Str;
use Illuminate\Http\Request;
use App\Mail\ForgotPasswordMail;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Mail;
use Illuminate\Support\Facades\Validator;
use TimeHunter\LaravelGoogleReCaptchaV2\Validations\GoogleReCaptchaV2ValidationRule;

class ForgotController extends Controller
{
    //Trang forgotPassword
    public function forgotPassword(){
        return view('auth.layout.forgot');
    }
    //Gửi mail lấy lại mật khẩu
    public function forgotSendMail(Request $request){
       $validator = Validator::make($request->all(), [
            'email' => 'required',
            //Kiểm tra có check Captcha
            'g-recaptcha-response' => new GoogleReCaptchaV2ValidationRule()
        ]);
            if ($validator->fails()) {
            return back()->withErrors($validator->errors())->withInput();
        }
        //Kiểm tra nhập đúng mail
        $email = $request->email;
        $forgLogin =  User::where('email', $request->email)->first();
        if (!$forgLogin) {
            return redirect()->route('forgot.password')->with('error', 'Vui lòng nhập chính xác email')->withInput();
        }
        //Tạo mã get_token_pass lưu vào database
        $get_token_pass = Str::random();
        $forgLogin->token_forgot_pass = $get_token_pass;
        $forgLogin->save();
        //Tạo url chuyền qua view
        $get_url_route = route('forgot.resetpass',['token'=>$get_token_pass]);
        Mail::to($request->email)->send(new ForgotPasswordMail($get_url_route));
      
         return redirect()->route('forgot.password')->with('success', 'Vui lòng kiểm tra email lấy lại mật khẩu');
    }


    public function checkUrlResetPass(Request $request)
    {
        //Kiểm tra lấy đúng đường dẫn token resetpassowrd
    $checkToken =  User::where('token_forgot_pass', $request->token)->first();
    if(!$checkToken){
        return redirect()->route('forgot.password')->with('error', 'Đường dẫn lấy mật khẩu không đúng vui lòng kiểm tra lại');
   }else{
        $get_token_pass =  $checkToken->token_forgot_pass;
        return view('auth.layout.reset-pass',compact('get_token_pass'));
   }
        
    }

    public function handleResetPassword(Request $request){
            //Xử lí lấy lại mật khẩu
        $checkToken =  User::where('token_forgot_pass', $request->get_token_pass)->first();
        if ($request->password === $request->confirm_password) {
            $get_Password = Hash::make($request->password);
            $checkToken->password = $get_Password;
            $checkToken->token_forgot_pass = '';
            $checkToken->save();
            return redirect()->route('login')->with('success', 'Cập nhật mật khẩu thành công');
        } else {
            return redirect()->route('forgot.resetpass', ['token' => $checkToken->token_forgot_pass])->with('error', 'Mật khẩu không khớp với nhau');
        }
            
    }
}
