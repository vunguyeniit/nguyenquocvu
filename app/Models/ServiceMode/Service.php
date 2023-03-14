<?php

namespace App\Models\ServiceMode;

use App\Models\NubModel\Customer;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Service extends Model
{
    use HasFactory;
    protected $table = 'service';
    protected $fillable = [
        'servicecode',
        'servicename',
        'description'

    ];
    public function getService()
    {
        return $this->hasMany(Ordinal::class, 'service_id', 'id');
    }
    // public function getCustomer()
    // {
    //     $this->belongsToMany(Customer::class, 'cumtomer_service');
    // }
}