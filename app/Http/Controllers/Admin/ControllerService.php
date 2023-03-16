<?php

namespace App\Http\Controllers\Admin;

use App\Models\ServiceMode\Service;
use App\Models\ServiceMode\Ordinal;
use App\Http\Controllers\Controller;
use Carbon\Carbon;
use Illuminate\Http\Request;



class ControllerService extends Controller
{
    public function index(Request $request)
    {

        $query = Service::query();
        $service = $query->paginate(4);
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



        if ($keyword = $request->search) {
            $service = Service::where('servicename', 'like', '%' . $keyword . '%')

                ->orWhere('description', 'LIKE', '%' . $keyword . '%')
                ->get();
        }



        return view('service.service', compact('service',));
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
                    'number' =>  $id,
                    'service_id' => $service->id,
                    'status' => $ran,
                    'is_printed' => 0
                ]);
            }
        } else {
            return redirect()->route('service.create');
        }
    }
    public function show($id)
    {

        $ordinal = Service::find($id);

        $paginate = $ordinal->getService()->paginate(2);



        return view('service.detail', compact('ordinal', 'paginate'));
    }

    public function edit($id)
    {


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
