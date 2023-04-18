<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

class PaymentController extends Controller
{
    public function HandelePayment()
    {
        return view('Payment_success.Payment');
    }
}
