<?php

namespace App\Models\System;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class account extends Model
{
    protected $table = "account";
    protected $fillable = [
        'username',
        'fullname',
        'phone',
        'email',
        'password',
        'confirm_password',
        'role',
        'status',


    ];
    use HasFactory;
}
