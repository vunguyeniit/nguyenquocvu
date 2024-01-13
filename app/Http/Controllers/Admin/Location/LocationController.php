<?php

namespace App\Http\Controllers\Admin\Location;

use App\Models\Location;
use App\Models\Department;
use Illuminate\Http\Request;
use App\Exports\LocationExport;
use App\Imports\LocationImport;
use Illuminate\Support\Facades\DB;
use App\Http\Controllers\Controller;
use Maatwebsite\Excel\Facades\Excel;
use App\Http\Requests\LocationRequest;
use Illuminate\Support\Facades\Validator;
use Illuminate\Database\Eloquent\ModelNotFoundException;

class LocationController extends Controller
{

     public function index(Request $request){
  
        $get_location_all = Location::query();
        if($request->has('search')){
            $search = $request->search;
           $get_location_all->where(function($query) use ($search) {
            $query->orWhere('street_address', 'like', '%' . $search . '%')
              ->orWhere('location_name', 'like', '%' . $search . '%')
              ->orWhere('city', 'like', '%' . $search . '%')
              ->orWhere('country', 'like', '%' . $search . '%')
              ->orWhere('notes', 'like', '%' . $search . '%');
    });
        }
        $get_location =  $get_location_all->paginate(3);
        return view('layout.location.location',compact('get_location'));
    }


    //Hiển thị cột select department
    public function create(){
        $get_department = Department::all();
        return view('layout.location.create-location',compact('get_department'));
    }


    public function store(LocationRequest $request){
    
       $location = Location::create($request->all());
        if($location){
            return redirect()->route('location')->with('success','Thêm thành công');
        }else{
            return redirect()->route('location.create')->with('error','Thêm thất bại')->withInput();
        }
      
    }

    public function edit(Request $request, ?string $id = null){
      
          try {
              $get_department = Department::all();
              $get_location = Location::findOrFail($id);
            } 
            catch (ModelNotFoundException $e) {
             return redirect()->route('location')->withError($e->getMessage());
        }
        return view('layout.location.edit-location',compact('get_location','get_department'));  
}
      

       public function update(LocationRequest $request, ?string $id = null){
            try{
                $get_location = Location::findOrFail($id);
                $update_location = $get_location->update($request->all());

            }    catch(ModelNotFoundException $e){
             return redirect()->route('location.list')->withError($e->getMessage());
            }
             return redirect()->route('location.list')->with('success','Edit thành công');
} 


       public function destroy(Request $request, ?string $id = null){
           try {
            $get_location = Location::findOrFail($id);
            $get_location->delete();
        } catch (ModelNotFoundException $e) {
          return redirect()->route('location.list')->withError($e->getMessage());
        }
         return redirect()->route('location.list')->with('success','Xóa thành công');
} 

        public function copy(Request $request, ?string $id = null){
             try {
            $get_location = Location::findOrFail($id);
            $copy_location = $get_location->replicate(); 
            $copy_location->save();

             }catch(ModelNotFoundException $e){
            return redirect()->route('location.list')->withError($e->getMessage());
             }
             return redirect()->route('location.list')->with('success','Copy thành công');
} 


     public function importLocation(Request $request) 
    {
         $validator = Validator::make($request->all(), [
           'file' => 'required|mimes:xlsx,csv',
        ]);
         if ($validator->fails()) {
            $errors = $validator->messages();
            foreach ($errors->all() as $error) {
            return back()->with('error', $error);             
         }
    }
         Excel::import(new LocationImport, $request->file('file'));
        return redirect()->route('location.list')->with('success', 'Import dữ liệu thành công!');
    }


    public function exportLocation(Request $request){
        // dd($request->query('file_extension'));
        $currentTimestamp = time();
        $formattedDate = date('Y-m-d H:i:s', $currentTimestamp);
        $filename = 'LocationExport' . '_' . $formattedDate . $request->query('file_extension');
        return Excel::download(new LocationExport,  $filename);
    }

   
}
    




