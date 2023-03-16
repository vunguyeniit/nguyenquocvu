<?php

namespace App\Http\Controllers\Admin;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\AdminModel\ModelDevice;
use App\Models\AdminModel\TagId;
use Illuminate\Support\Facades\Session;
use App\Models\AdminModel\TagName;
use DB;
use Illuminate\Cache\TagSet;
use Illuminate\Database\Eloquent\Model;

class ControllerDevice extends Controller
{

    public function index(Request $request)
    {


        $query = ModelDevice::query();

        $device = $query->get();
        //  $tagname = TagName::find($id);

        foreach ($device as $item) {
            $tag[] = $item->tags1;
            // $id[] = $tag->id;
            // foreach ($item->tags1 as $i) {
            //     $name[] = $i;
            // }
        }
        // dd($tag);
        // if ($name == $tag->id) {
        //     $tag[] = $item->tags1;
        // }
        // dd($tag);
        if ($request->ajax()) {

            if (($request->connection) == "") {
                $devicestatus = $query->get();
            } else {

                $devicestatus = $query->where(['connectionstatus' => $request->connection])->get();
            }
            if (($request->statusid) == "") {
                $devicestatus = $query->get();
            } else {


                $devicestatus = $query->where(['activestatus' => $request->statusid])->get();
            }
            return response()->json([
                'devicestatus' => $devicestatus,
                // 'device' => $tag,
            ]);
        }

        if ($keyword = $request->search) {
            $device = ModelDevice::where('devicename', 'like', '%' . $keyword . '%')
                ->orWhere('devicecode', 'LIKE', '%' . $keyword . '%')
                ->orWhere('addressip', 'LIKE', '%' . $keyword . '%')
                ->get();
        }

        return view('device.device', compact('device'));
    }


    public function create()
    {

        return view('device.create');
    }


    public function store(Request $request)
    {
        $device = ModelDevice::create([
            'devicecode' => $request->devicecode,
            'devicename' => $request->devicename,
            'devicetype' => $request->devicetype,
            'username' => $request->username,
            'addressip' => $request->addressip,
            'password' => $request->password,

        ]);

        foreach ($request->tags as $tagitem) {

            $tagInstance = TagName::firstOrCreate(
                [
                    'devicename' => $tagitem
                ]
            );

            // TagId::create([
            //     'user_id' => $device->id,
            //     'tag_id' => $tagInstance->id
            // ]);
            $tagId[] = $tagInstance->id;
        }

        $device->tags1()->attach($tagId);
    }



    public function show($id)
    {
        $devishow = ModelDevice::find($id);

        return view('device.detail', compact('devishow'));
    }




    public function edit($id)
    {
        $getData =  ModelDevice::find($id);

        foreach ($getData->tags1 as $role) {

            $name[] = $role;
        }

        return view('device.edit', compact('getData', 'name'));
    }


    public function update(Request $request, $id)
    {
        $updateData = ModelDevice::find($id);

        $updateData->update([
            'devicecode' => $request->devicecode,
            'devicename' => $request->devicename,
            'devicetype' => $request->devicetype,
            'username' => $request->username,
            'addressip' => $request->addressip,
            'password' => $request->password,
        ]);
        foreach ($request->tags as $tagitem) {

            $tagInstance = TagName::firstOrCreate(
                [
                    'devicename' => $tagitem
                ]
            );

            $tagId[] = $tagInstance->id;
        }

        $updateData->tags1()->sync($tagId);;
    }


    public function destroy($id)
    {
    }
}