<?php

namespace App\Models\NubModel;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Number_Print extends Model
{
    use HasFactory;
    protected $table = "number_print";
    protected $fillable = [
        'number_print',
        'id_print',
        'user_id',
        'grant_time',
        'expired'

    ];
}
