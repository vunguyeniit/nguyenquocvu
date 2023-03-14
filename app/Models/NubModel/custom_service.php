<?php

namespace App\Models\NubModel;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class custom_service extends Model
{
    use HasFactory;
    protected $table = "cumtomer_service";
    protected $fillable = [
        'id',
        'user_id',
        'ser_id',


    ];
}
