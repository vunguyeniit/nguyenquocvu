<?php

namespace App\Http\Controllers\Admin\Dasboard;

use Illuminate\Http\Request;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Auth;

class DasboardController extends Controller
{
     public function index(){
        return view('layout.dasboard');
   } 
}
