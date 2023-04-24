<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

class PaymentController extends Controller
{
    public function HandelePayment(Request $request)
    {

        return view('Payment_success.Payment');
    }
}
