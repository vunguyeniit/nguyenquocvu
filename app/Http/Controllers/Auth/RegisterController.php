<?php

namespace App\Http\Controllers\Auth;

use App\Models\User;
use App\Models\Roles;
use App\Mail\VerifyMail;
use App\Models\Verifytoken;
use Illuminate\Http\Request;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Mail;
use Illuminate\Support\Facades\Validator;

class RegisterController extends Controller
{
    public function create(){
         return view('auth.layout.sign');
    }
    public function createRole(){
             Roles::create([
                        'role_name' => 'Admin',
                        'description' => 'Admin',
                        'permission_name'=> json_encode(['role.list', 
                        'role.create', 'role.edit', 'role.delete','location.list', 
                        'location.create', 'location.edit', 'location.delete'])
                ]);
    }
    public function store(Request $request)
    {
                    $validator = Validator::make($request->all(), [
                        'name' => 'required',
                        'email' =>'required',
                        'password'=>'required',
                        'company' =>'required',
                        ]);
           if ($validator->fails()) {
            return back()->withErrors($validator->errors())->withInput();
          }else{
            $this->createRole();
            $validToken = random_int(100000,200000);
            $user = User::create([
            'name'=>$request->name,
            'email'=>$request->email,
            'password'=>Hash::make($request->password),
            'company'=>$request->company,
            'email_verified_at'=>$validToken
 ]);
        $user->roles()->attach(1);
        $get_user_email = $request->email;
        $get_user_name = $request->name;
        Mail::to($request->email)->send(new VerifyMail($get_user_email, $validToken,$get_user_name));
        return redirect()->route('verify.account')->with('success', 'Đăng ký tài khoản thành công,vui lòng check mail xác thực');
    }

    }  
}
