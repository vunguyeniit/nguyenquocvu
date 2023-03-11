<?php

namespace App\Models\ServiceMode;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Ordinal extends Model
{
    use HasFactory;
    protected $table = 'ordinal';
    protected $fillable = [
        'numerical_order',
        'service_id',
        'status'

    ];
}
