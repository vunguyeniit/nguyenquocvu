<?php

namespace App\Models\Customer_Payment;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Customer extends Model
{
    use HasFactory;
    protected $table = 'customer';
    protected $fillable = [
        'username',
        'phone',
        'email',
        'date',
        'ticket',
        'price_ticket',
    ];
    public function Payment()
    {
        return $this->hasOne(Payment::class, 'customer_id', 'id');
    }
}
