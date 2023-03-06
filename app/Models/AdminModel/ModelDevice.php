<?php

namespace App\Models\AdminModel;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\MorphToMany;

class ModelDevice extends Model
{
    use HasFactory;
    protected $table = "device";
    protected $fillable = [
        'id',
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

    public function tags1()
    {
        return $this->belongsToMany(TagName::class, 'tagid', 'user_id', 'tag_id')->withTimestamps();
    }
}
