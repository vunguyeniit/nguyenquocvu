<?php

namespace App\Models;

// use Illuminate\Contracts\Auth\MustVerifyEmail;
use App\Models\Roles;
use Laravel\Sanctum\HasApiTokens;
use Illuminate\Notifications\Notifiable;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Foundation\Auth\User as Authenticatable;

class User extends Authenticatable
{
    use HasApiTokens, HasFactory, Notifiable;

    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $table = 'users';
    protected $fillable = [
        'id',
        'name',
        'is_activated',
        'token_forgot_pass',
        'email',
        'company',
        'email_verified_at',
        'password',
        'phone',
        'location_id'
    ];
  public function roles(){
    return $this->belongsToMany(Roles::class,'user_role','user_id', 'role_id','id','id');
}
    // public function routes(){
    //     // $data=[];
    //     // foreach($this->role as $roles){
    //     //     $permi = json_decode($roles->permission_name);
    //     //     foreach( $permi as $p){
    //     //          if(!in_array($p,$data))
    //     //     {
    //     //         array_push($data,$p);
    //     //     }
    //     //     }
           
    //     // }
    //     // dd($data);
    // }

    /**
     * The attributes that should be hidden for serialization.
     *
     * @var array<int, string>
     */
    // protected $hidden = [
    //     'password',
    //     'remember_token',
    // ];

    // /**
    //  * The attributes that should be cast.
    //  *
    //  * @var array<string, string>
    //  */
    // protected $casts = [
    //     'email_verified_at' => 'datetime',
    //     'password' => 'hashed',
    // ];
}
