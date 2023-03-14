<?php

namespace App\Models\NubModel;

use App\Models\ServiceMode\Service;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use DB;

class Customer extends Model
{
    use HasFactory;
    protected $table = "customer";
    protected $fillable = [
        'id',
        'fullname',
        'phone',
        'email'

    ];
    // public function selectedServices()
    // {
    //     return $this->belongsToMany(Service::class, 'cumtomer_service', 'user_id', 'ser_id');
    // }

    // public function selectedServices()
    // {
    //     return DB::table('customer')
    //         ->join('cumtomer_service', 'customer.id', '=', 'cumtomer_service.user_id')
    //         ->join('service', 'cumtomer_service.ser_id', '=', 'service.id')
    //         ->select('service.*')
    //         ->where('customer.id', $this->id)
    //         ->get();
    // }
}