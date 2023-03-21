<?php

namespace App\Http\Controllers\Admin;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class ControllerDashboard extends Controller
{

    public function index(Request $request)
    {
        $data = [
            'count_sum' => DB::table('number_print')->count(),
            'count_device' => DB::table('device')->count(),
            'count_service' => DB::table('service')->count(),
            'count1' => DB::table('number_print')->where('status', 1)->count(),
            'count2' => DB::table('number_print')->where('status', 0)->count(),
            'count3' => DB::table('number_print')->where('status', 2)->count(),
            'count4' => DB::table('device')->where('activestatus', 0)->count(),
            'count5' => DB::table('device')->where('activestatus', 1)->count(),
            'count6' => DB::table('service')->where('status', 0)->count(),
            'count7' => DB::table('service')->where('status', 1)->count(),

            // 'count4' => DB::table('device')->whereIn('activestatus', [0, 1])->countBy('activestatus'),
            // 'count6' => DB::table('service')->whereIn('status', [0, 1])->countBy('status')

        ];

        // dd($data);
        return view("Dashboard.dashboard", compact('data'));
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