<?php

namespace App\Models\AdminModel;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class TagName extends Model
{
    use HasFactory;
    protected $table = "tagname";
    protected $fillable = [
        'devicename'

    ];
}
