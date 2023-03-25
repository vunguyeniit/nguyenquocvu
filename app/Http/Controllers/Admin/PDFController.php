<?php

namespace App\Http\Controllers\Admin;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use PDF;
use DB;

class PDFController extends Controller
{
    public function generatePDF()
    {
        $report = DB::table('number_print')
            ->join('ordinal', 'ordinal.service_id', '=', 'number_print.id_print')
            ->join('service', 'service.id', '=', 'ordinal.service_id')
            ->where('number_print.id_print', '=', DB::raw('CAST(ordinal.service_id AS CHAR)'))
            ->orderBy('number_print.number_print', 'asc')
            ->select(
                'number_print.id',
                'number_print.number_print',
                'service.servicename',
                'number_print.grant_time',
                'number_print.status',
                'number_print.supply'
            )
            ->distinct()
            ->get();

        $data = [
            'title' => 'Danh Sách Báo Cáo',
            'date' => date('d/m/Y'),
            'reports' => $report
        ];

        $pdf = PDF::loadView('report.myPDF', $data);
        return $pdf->download('baocao.pdf');
    }
}
