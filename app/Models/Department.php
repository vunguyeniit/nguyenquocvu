<?php

namespace App\Models;

use App\Models\Location;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Factories\HasFactory;

class Department extends Model
{
    use HasFactory;
     protected $table = 'department';
     protected $fillable = [
        'id',
        'department_name',
        
    ];
     public function location(){
          return $this->hasMany(Location::class,'department_id','id');
    }
    
}
