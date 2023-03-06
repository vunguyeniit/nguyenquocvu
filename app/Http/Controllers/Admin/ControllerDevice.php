<?php

namespace App\Http\Controllers\Admin;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\AdminModel\ModelDevice;
use App\Models\AdminModel\TagId;
use App\Models\AdminModel\TagName;
use Illuminate\Cache\TagSet;

class ControllerDevice extends Controller
{

    public function index()
    {
        $device = ModelDevice::all();
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

        $device->tags()->attach($tagId);
    }



    public function show($id)
    {
        $devishow = ModelDevice::find($id);
        // dd($devishow);
        return view('device.detail', compact('devishow'));
    }

    public function edit($id)
    {
        $getData =  ModelDevice::find($id);
        return view('device.edit', compact('getData'));
    }


    public function update(Request $request, $id)
    {
        $updateData = ModelDevice::find($id);
        dd($updateData);
        $updateData->update($request->all());
    }


    public function destroy($id)
    {
    }
}
