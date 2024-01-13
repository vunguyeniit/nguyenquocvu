<?php

namespace App\Http\Controllers\Admin\Role;
use Illuminate\Support\Str;
use App\Models\Roles;
use Illuminate\Http\Request;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Route;
use Illuminate\Database\Eloquent\ModelNotFoundException;

class RoleController extends Controller
{
     public function index(){
        $get_role_all = Roles::all();
         return view('layout.role.role',compact('get_role_all'));
    }

    public function getRoute(){
          $routes = Route::getRoutes();
            return collect($routes)->filter(function ($route, $key) {
                    $routeName = $route->getName();
            return Str::startsWith($routeName, ['location', 'role']) &&
                        Str::endsWith($routeName, ['list', 'create', 'edit', 'delete']);
                            })->map(function ($route) {
            return htmlspecialchars($route->getName());
            });
}

    public function create(){
            return view('layout.role.create-role')->with([
                'filteredRoutes'=>  $this->getRoute()
        ]);
 }
   
    public function store(Request $request){
          
         $route= json_encode($request->checkbox);
         $role = Roles::create([
            'role_name' => $request->role_name,
            'description' => $request->description,
            'permission_name'=>  $route
        ]);
       return redirect()->route('role.list')->with('success','Create thành công');
    }

    public function edit(Request $request, ?string $id = null){

   
          try {
                $getId_role = Roles::findOrFail($id); 
                $yourFunction = function($permission, $param) {
                    return count(array_filter(json_decode($permission), function ($item) use ($param) {
                                return strpos($item, $param) !== false;
         }));
        };
            } 
            catch (ModelNotFoundException $e) {
            return redirect()->route('role.list')->withError($e->getMessage());
        };
                return view('layout.role.edit-role')->with([
                    'getId_role'=> $getId_role,
                    'yourFunction'=>  $yourFunction,
                    'filteredRoutes'=> $this->getRoute()
                ]);  
    }

       public function update(Request $request, ?string $id = null){
             try{
                $getId_role = Roles::findOrFail($id);
                $route= json_encode($request->checkbox);
                $getId_role->update([
                  'role_name' => $request->role_name,
                  'description' => $request->description,
                   'permission_name'=>  $route
                  ]);
               
           } catch(ModelNotFoundException $e){
               return redirect()->route('role.list')->withError($e->getMessage());
             }
            return redirect()->route('role.list')->with('success','Edit thành công');
            
} 
       public function destroy(Request $request, ?string $id = null){
            try {
          
              $getId_role = Roles::findOrFail($id);
            $getId_role->delete();
            
        } catch (ModelNotFoundException $e) {
          return redirect()->route('role.list')->withError($e->getMessage());
        }
         return redirect()->route('role.list')->with('success','Xóa thành công');
} 
        public function copy(Request $request, ?string $id = null){
             try {
            $getId_role = Roles::findOrFail($id);
            $copy_role = $getId_role->replicate();       
            $copy_role->save();
             }catch(ModelNotFoundException $e){
            return redirect()->route('role.list')->withError($e->getMessage());
             }
            return redirect()->route('role.list')->with('success','Copy thành công');
} 
}
