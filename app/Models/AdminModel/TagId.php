<?php

namespace App\Models\AdminModel;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class TagId extends Model
{
    use HasFactory;
    protected $table = "tagid";
    protected $fillable = [
        'user_id',
        'tag_id'

    ];
}
