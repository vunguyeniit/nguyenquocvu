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
        $device = ModelDevice::paginate(2);
        if ($request->ajax()) {
            $devicestatus = DB::table('device')
                ->join('tagid', 'device.id', '=',  DB::raw('CAST(tagid.user_id AS CHAR)'))
                ->join('tagname', 'tagname.id', '=', 'tagid.tag_id')
                ->select(
                    'device.id',
                    'device.devicecode',
                    'device.devicename',
                    'device.addressip',
                    'device.activestatus',
                    'device.connectionstatus',
                    DB::raw("GROUP_CONCAT(tagname.device_service SEPARATOR  ',') as device_service")
                )
                ->groupBy(
                    'device.id',
                    'device.devicecode',
                    'device.devicename',
                    'device.addressip',
                    'device.activestatus',
                    'device.connectionstatus'
                )
                ->distinct();
            if (isset($request->connection)) {
                $devicestatus->where('device.connectionstatus', '=', $request->connection);
            }
            if (isset($request->statusid)) {
                $devicestatus->where('device.activestatus', '=', $request->statusid);
            }
            $devicestatus = $devicestatus->get();
            return response()->json([
                'devicestatus' => $devicestatus,
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
                    'device_service' => $tagitem
                ]
            );

            // TagId::create([
            //     'user_id' => $device->id,
            //     'tag_id' => $tagInstance->id
            // ]);
            $tagId[] = $tagInstance->id;
        }

        $device->tags1()->attach($tagId);

        return redirect()->route('device.index');
    }



    public function show($id)
    {
        $devishow = ModelDevice::find($id);

        return view('device.detail', compact('devishow'));
    }




    public function edit($id)
    {
        $getData =  ModelDevice::find($id);

        // foreach ($getData->tags1 as $role) {

        //     $name[] = $role;
        // }

        return view('device.edit', compact('getData',));
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
                    'device_service' => $tagitem
                ]
            );

            $tagId[] = $tagInstance->id;
        }

        $updateData->tags1()->sync($tagId);
        return redirect()->route('device.index');
    }


    public function destroy($id)
    {
    }
}
