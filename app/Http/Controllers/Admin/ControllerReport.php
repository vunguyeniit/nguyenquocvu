<?php

namespace App\Http\Controllers\Admin;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class ControllerReport extends Controller
{
    public function index(Request $request)
    {
        $report = DB::table('number_print')
            ->join('ordinal', 'ordinal.service_id', '=', 'number_print.id_print')
            ->join('service', 'service.id', '=', 'ordinal.service_id')
            ->where('number_print.id_print', '=', DB::raw('CAST(ordinal.service_id AS CHAR)'))
            ->orderBy('number_print.number_print', 'asc')
            ->select(
                'number_print.number_print',
                'service.servicename',
                'number_print.grant_time',
                'number_print.status',
                'number_print.supply'
            )
            ->distinct()
            ->paginate(8);
        if ($request->ajax()) {

            if (isset($request->start_date) && isset($request->end_date)) {
                $start_date = date('Y-m-d H:i:s', strtotime($request->start_date . ' 00:00:00'));
                $end_date = date('Y-m-d H:i:s', strtotime($request->end_date . ' 23:59:59'));
                $report = DB::table('number_print')
                    ->join('ordinal', 'ordinal.service_id', '=', 'number_print.id_print')
                    ->join('service', 'service.id', '=', 'ordinal.service_id')
                    ->where('number_print.id_print', '=', DB::raw('CAST(ordinal.service_id AS CHAR)'))
                    ->whereBetween('number_print.created_at', [$start_date, $end_date])
                    ->orderBy('number_print.number_print', 'asc')
                    ->select(
                        'number_print.number_print',
                        'service.servicename',
                        'number_print.grant_time',
                        'number_print.status',
                        'number_print.supply'
                    )
                    ->distinct()
                    ->get();
            }
            return response()->json([
                'report' => $report,
            ]);
        }
        return view('report.report', compact('report'));
    }
    public function create()
    {
    }

    public function store(Request $request)
    {
    }

    public function show($id)
    {
    }


    public function edit($id)
    {
    }


    public function update(Request $request, $id)
    {
    }


    public function destroy($id)
    {
    }
}
