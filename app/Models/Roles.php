<?php

namespace App\Models;


use App\Models\User;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Factories\HasFactory;

class Roles extends Model
{
    use HasFactory;
     protected $table = 'roles';
     protected $fillable = [
        'id',
        'role_name',
        'permission_name',
        'description'
        
    ];
    //Many to Many Permission -> Roles
public function users(){
    return $this->belongsToMany(User::class, 'user_role', 'user_id', 'role_id','id','id');
}
  
}
