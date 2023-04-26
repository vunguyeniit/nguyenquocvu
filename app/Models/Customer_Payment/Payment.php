<?php

namespace App\Models\Customer_Payment;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Payment extends Model
{
    use HasFactory;
    protected $table = 'payment';
    protected $fillable = [
        'id',
        'card_number',
        'card_holder',
        'expiration_date',
        'CVV',
        'customer_id',

    ];

    public function user()
    {
        return $this->belongsTo(Customer::class, 'customer_id', 'id');
    }
}
