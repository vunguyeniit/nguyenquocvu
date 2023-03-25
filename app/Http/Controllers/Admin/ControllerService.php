<?php

namespace App\Http\Controllers\Admin;

use App\Models\ServiceMode\Service;
use App\Models\ServiceMode\Ordinal;
use App\Http\Controllers\Controller;
use Carbon\Carbon;
use Illuminate\Http\Request;
use DB;



class ControllerService extends Controller
{
    public function index(Request $request)
    {

        $query = Service::query();
        $service = $query->paginate(3);
        if ($request->ajax()) {

            if (($request->statusid) == "") {
                $servicestatus = $query->get();
            } else {

                $servicestatus = $query->where(['status' => $request->statusid])->get();
            }


            if ($request->start_date && $request->end_date) {
                $start_date = date('Y-m-d H:i:s', strtotime($request->start_date . ' 00:00:00'));
                $end_date = date('Y-m-d H:i:s', strtotime($request->end_date . ' 23:59:59'));

                $servicestatus = DB::table('service')
                    ->select('*')
                    ->whereBetween('created_at', [$start_date, $end_date])
                    ->get();
            }
            return response()->json([
                'servicestatus' => $servicestatus,

            ]);
        }


        if ($keyword = $request->search) {
            $service = Service::where('servicename', 'like', '%' . $keyword . '%')
                ->orWhere('description', 'LIKE', '%' . $keyword . '%')
                ->paginate(3);
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
            foreach (range(0, 15) as $item) {
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
        return redirect()->route('service.index');
    }
    public function show(Request $request, $id)
    {
        $ordinal = Service::find($id);
        $paginate = $ordinal->getService()->paginate(8);
        if ($request->ajax()) {
            if (($request->statusid) == "") {
                $detail =  DB::table('ordinal')
                    ->where('service_id', $id)
                    ->select('*')
                    ->get();
            } else {
                $detail =  DB::table('ordinal')
                    ->where('service_id', $id)
                    ->where('status', $request->statusid)
                    ->select(
                        '*',
                    )
                    ->get();
            }

            if ($request->start_date && $request->end_date) {
                $start_date = date('Y-m-d H:i:s', strtotime($request->start_date . ' 00:00:00'));
                $end_date = date('Y-m-d H:i:s', strtotime($request->end_date . ' 23:59:59'));

                $detail = DB::table('ordinal')
                    ->select('*')
                    ->where('service_id', $id)
                    ->whereBetween('created_at', [$start_date, $end_date])
                    ->get();
            }
            return response()->json([
                'detail' => $detail,
            ]);
        }

        //Tìm Kiếm
        if ($keyword = $request->search_deatil) {
            $paginate =  DB::table('ordinal')
                ->where('number', 'like', '%' . $keyword . '%')
                ->where('service_id', $id)
                ->paginate(5);
        }


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
        return redirect()->route('service.index');
    }

    public function destroy($id)
    {
    }
}
