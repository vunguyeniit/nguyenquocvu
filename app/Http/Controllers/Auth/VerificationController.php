<?php

namespace App\Http\Controllers\Auth;

use App\Models\User;
use App\Models\Roles;
use App\Models\Verifytoken;
use Illuminate\Http\Request;
use App\Http\Controllers\Controller;

class VerificationController extends Controller
{
    public function verifyAccount(){
        
        return view('auth.layout.verify-account');
    }
    public function userActivate(Request $request){
        $get_token = $request->token; 
         $get_token = User::where('email_verified_at', $get_token)->first();
           if ($get_token) {
            $user = User::where('email', $get_token->email)->first();
            $user->is_activated = 1;
            $user->email_verified_at = null;
            $users = $user->save();
         return redirect('login')->with('success', 'Kích hoạt tài khoản thành công');
        } else {
        return redirect('verify')->with('error', 'Kích hoạt tài khoản không thành công');

    }
}
}
