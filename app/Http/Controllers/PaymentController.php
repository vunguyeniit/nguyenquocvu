<?php

namespace App\Http\Controllers;

use App\Models\Customer_Payment\Customer;
use App\Models\Customer_Payment\Payment;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class PaymentController extends Controller
{
    public function HandelePayment(Request $request)
    {
        $user = Customer::create([
            "username" => $request->username,
            "phone" => $request->phone,
            "email" => $request->email,
            "date" => $request->date,
            "ticket" => $request->ticket,
            "price_ticket" => $request->money,
        ]);
        $user->Payment()->create([
            'card_number' => $request->card_number,
            'card_holder' => $request->card_holder,
            'expiration_date' => $request->expiration_date,
            'CVV' => $request->CVV,
            'customer_id' => $user->id,
        ]);

        $getPay = DB::table('customer')
            ->select('date')
            ->orderBy('id', 'desc')
            ->get();

        return view('Payment_success.Payment', compact('getPay'));
    }
}
