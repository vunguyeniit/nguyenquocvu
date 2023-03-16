<?php

namespace App\Models\System;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class role extends Model
{
    use HasFactory;
    protected $table = "role";
    protected $fillable = [
        'rolename',
        'member',
        'description',
        'permission',




    ];
}
