<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

use Illuminate\Support\Facades\Session;

class HomeController extends Controller
{
    public function index()
    {
        return view('Home.Home');
    }

    public function getPayment(Request $request)

    {
        $data  = $request->all();
        $number = 90000;
        $price =  $data['price_ticket'] * $number;
        return view('Pay.Pay', compact('data', 'price'));
    }
}
