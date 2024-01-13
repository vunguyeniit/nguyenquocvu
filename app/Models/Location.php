<?php

namespace App\Models;

use App\Models\NewUsers;
use App\Models\Department;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Factories\HasFactory;

class Location extends Model
{
    use HasFactory;
     protected $table = 'location';
     protected $fillable = [
        'location_name',
        'notes',
        'department_id',
        'building',
        'street_address',
        'city',
        'state',
        'country',
        'zip_code',
          
    ];
    public function department(){
          return $this->belongsto(Department::class,'department_id','id');
    }
//     public function newusers(){
//           return $this->hasMany(NewUsers::class,'location_id','id');
//     }
}
