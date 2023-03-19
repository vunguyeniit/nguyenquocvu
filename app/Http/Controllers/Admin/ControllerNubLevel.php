<?php

namespace App\Http\Controllers\Admin;

use App\Models\ServiceMode\Service;
use App\Http\Controllers\Controller;
use App\Models\NubModel\Customer;
use App\Models\NubModel\custom_service;
use App\Models\NubModel\Number_Print;
use App\Models\ServiceMode\Ordinal;
use Illuminate\Http\Request;
use Carbon\Carbon;
use DB;

class ControllerNubLevel extends Controller
{
    public function index(Request $request)
    {
        $number =  DB::table('number_print')
            ->join('ordinal', 'ordinal.service_id', '=', 'number_print.id_print')
            ->join('service', 'service.id', '=', 'ordinal.service_id')
            ->join('customer', 'customer.id', '=', 'number_print.user_id')
            ->where('ordinal.is_printed', 1)
            ->select(
                'number_print.number_print',
                'customer.fullname',
                'service.servicename',
                'number_print.grant_time',
                'number_print.expired',
                'number_print.id',

            )
            ->distinct()
            ->get();

        if ($request->ajax()) {
            if ($request->servicename == "") {
                $servicename = $number;
            } else {
                $servicename = DB::table('number_print')
                    ->join('ordinal', 'ordinal.service_id', '=', 'number_print.id_print')
                    ->join('service', 'service.id', '=', 'ordinal.service_id')
                    ->join('customer', 'customer.id', '=', 'number_print.user_id')
                    ->where('ordinal.is_printed', 1)
                    ->where('service.id', '=', $request->servicename)
                    ->select(
                        'number_print.number_print',
                        'customer.fullname',
                        'service.servicename',
                        'number_print.grant_time',
                        'number_print.expired',
                        'number_print.id',
                    )
                    ->distinct()
                    ->get();
            }

            return response()->json([
                'servicename' => $servicename
            ]);
        }

        $service = Service::all();

        return view('nublevel.nublevel', compact('number', 'service'));
    }
    public function create()
    {
        $nub = Service::all();
        $number =  DB::table('number_print')
            ->join('ordinal', 'ordinal.service_id', '=', 'number_print.id_print')
            ->join('service', 'service.id', '=', 'ordinal.service_id')
            ->select('*')
            ->orderBy('number_print.id', 'DESC')
            ->first();
        return view('nublevel.create', compact('nub', 'number'));
    }
    public function store(Request $request)
    {

        //Lấy ra ngày giờ
        $startOfDayFormatted =  Carbon::now()->format('H:i d-m-Y');
        $expired = Carbon::now()->endOfDay()->subHours(6)->format('H:i d-m-Y');
        $select = $request->input('select-service');
        //Truy vấn lấy ra dữ liệu ordinal
        $numbers = DB::table('ordinal')
            ->join('service', 'service.id', '=', 'ordinal.service_id')
            ->where('service_id', $select)
            ->where('ordinal.is_printed', false)
            ->orderBy('ordinal.number', 'ASC')
            ->first();
        if (!$numbers) {

            return "Không còn số nào chưa được in";
        }
        //Update cột is_printed thành true là đã in số rồi  ordinal
        DB::table('ordinal')
            ->where('number', $numbers->number)
            ->update(['is_printed' => true]);
        //Truy vấn lấy ra dữ liệu cumtomer_service
        $user = DB::table('cumtomer_service')
            ->join('customer', 'customer.id', '=', 'cumtomer_service.user_id')
            ->where('ser_id', $select)
            ->where('cumtomer_service.user_print', false)
            // ->select('cumtomer_service.user_id',)
            ->first();

        if (!$user) {
            return "Không còn số nào chưa được in";
        }
        //Update cột user_print thành true là người dùng đã in số rồi
        DB::table('cumtomer_service')
            ->where('ser_id', $numbers->id)
            ->where('user_id', $user->user_id)
            ->update(['user_print' => true]);
        //Tạo ra bản   Number_Print
        Number_Print::create([
            'number_print' => $numbers->number,
            'id_print' => $numbers->id,
            'user_id' => $user->user_id,
            'grant_time' => $startOfDayFormatted,
            'expired' => $expired
        ]);

        return redirect()->back()->with('success', 'successfully');
    }


    public function show($id)
    {
        $number =  DB::table('number_print')
            ->join('ordinal', 'ordinal.service_id', '=', 'number_print.id_print')
            ->join('service', 'service.id', '=', 'ordinal.service_id')
            ->join('customer', 'customer.id', '=', 'number_print.user_id')
            ->where('ordinal.is_printed', 1)
            ->where('number_print.id', $id)
            ->select(
                'number_print.number_print',
                'customer.fullname',
                'customer.phone',
                'customer.email',
                'service.servicename',
                'number_print.grant_time',
                'number_print.expired',
            )
            ->first();
        return view('nublevel.detail', compact('number'));
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
