<?php

namespace App\Http\Controllers\Admin\User;
use App\Models\User;
use App\Models\Roles;
use App\Models\Location;
use Illuminate\Http\Request;
use App\Http\Controllers\Controller;

class UserController extends Controller
{
  
    public function index(){
     
        return view('layout.user.user');
    }
    public function create(Request $request){
            $get_role = Roles::all();
            $get_location = Location::all();
           if($request->ajax()){
             $data = Location::where('id',$request->id)->first();
                $data->department;
                    return response()->json([
                    'data' => $data,
            ]);
        }
        return view('layout.user.create-user',compact('get_role','get_location'));
    }
    public function store(Request $request){

        $user = User::create([
            'name'=>$request->name,
            'email'=>$request->email,
            'password'=>bcrypt(123),
            'is_activated'=> 1,
            'location_id'=>$request->location_id
        ]);
    foreach($request->role_id as $roles){
             $getId_roles[] = $roles;
      }
       $user->roles()->attach($getId_roles);
        return true;
    }
}
