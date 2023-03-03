<?php

namespace App\Models\AdminModel;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class ModelDevice extends Model
{
    use HasFactory;
    protected $table = "device";
    protected $fillable = [
        'devicecode',
        'devicename',
        'devicetype',
        'username',
        'addressip',
        'password',
        'deviceuse',
        'active status',
        'connection status'

    ];
    // public $timestamps = false;
}