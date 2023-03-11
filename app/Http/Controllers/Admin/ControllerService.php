<?php

namespace App\Http\Controllers\Admin;

use App\Models\ServiceMode\Service;
use App\Models\ServiceMode\Ordinal;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use Illuminate\Support\Arr;

class ControllerService extends Controller
{
    public function index(Request $request)
    {
        $query = Service::query();
        $service = $query->get();
        if ($request->ajax()) {

            if (($request->statusid) == "") {
                $servicestatus = $query->get();
            } else {

                $servicestatus = $query->where(['status' => $request->statusid])->get();
            }
            return response()->json([
                'servicestatus' => $servicestatus,

            ]);
        }

        return view('service.service', compact('service'));
    }

    public function create()
    {
        return view('service.create');
    }
    public function store(Request $request)
    {



        if ($request->checkbox == true) {
            $service = Service::create([
                'servicecode' => $request->servicecode,
                'servicename' => $request->servicename,
                'description' => $request->description,

            ]);
            foreach (range(0, 5) as $item) {
                if ($item <= 9) {
                    $id = $request->servicecode . '000' . $item;
                } else {
                    $id = $request->servicecode . '00' . $item;
                }
                $ran = rand(0, 2);
                Ordinal::create([
                    'numerical_order' =>  $id,
                    'service_id' => $service->id,
                    'status' => $ran,
                ]);
            }
        } else {
            return redirect()->route('service.create');
        }
    }
    public function show($id)
    {
        $ordinal = Service::find($id);
        foreach ($ordinal->getService as $role) {

            $name[] = $role;
        }
        return view('service.detail', compact('name', 'ordinal'));
        //
    }

    public function edit($id)
    {
        //

        $service = Service::find($id);

        return view('service.edit', compact("service"));
    }

    public function update(Request $request, $id)
    {
        $service = Service::find($id);

        if ($request->checkbox == true) {
            $service->update([
                'servicecode' => $request->servicecode,
                'servicename' => $request->servicename,
                'description' => $request->description,
            ]);

            foreach ($service->getService as $ordinal) {
                $index = substr($ordinal->numerical_order, -4);
                $id = $request->servicecode . $index;
                $ordinal->update([
                    'numerical_order' => $id,
                ]);
            }
        } else {
            return redirect()->route('service.create');
        }
    }

    public function destroy($id)
    {
    }
}