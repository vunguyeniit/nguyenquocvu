<?php

namespace App\Models\AdminModel;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\MorphToMany;


class TagId extends Model
{
    use HasFactory;
    protected $table = "tagid";
    protected $fillable = [
        'id',
        'user_id',
        'tag_id'

    ];
}
