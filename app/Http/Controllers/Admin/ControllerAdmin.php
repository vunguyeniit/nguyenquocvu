<?php

namespace App\Http\Controllers\Admin;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\LoginAdmin;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Crypt;


class ControllerAdmin extends Controller
{
    //

    public function index()
    {
        if (Auth::check()) {
            $user = Auth::user();

            // $user2 = password-ve($user->password);
            // dd($user2);
            return view('admin.account-user', compact('user'));
        } else {

            return "chua dang nhap";
        }
    }
}