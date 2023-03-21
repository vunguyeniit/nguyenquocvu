<?php

namespace App\Http\Controllers\Admin;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class ControllerReport extends Controller
{

    public function index()
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
            ->get();
        // dd($report);
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
