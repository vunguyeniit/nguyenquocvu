<?php

namespace App\Models\ServiceMode;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Ordinal extends Model
{
    use HasFactory;
    protected $table = 'ordinal';
    protected $fillable = [
        'number',
        'service_id',
        'is_printed',
        'status'

    ];
}
