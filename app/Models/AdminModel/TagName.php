<?php

namespace App\Models\AdminModel;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\MorphToMany;

class TagName extends Model
{
    use HasFactory;
    protected $table = "tagname";
    protected $fillable = [
        'id',
        'devicename'

    ];
}
