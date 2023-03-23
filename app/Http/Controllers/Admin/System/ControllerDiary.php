<?php

namespace App\Http\Controllers\Admin\System;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use DB;

class ControllerDiary extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index(Request $request)
    {
        $diary = DB::table('diary')
            ->select('*')
            ->paginate(6);
        if ($request->ajax()) {

            if ($request->start_date && $request->end_date) {
                $start_date = date('Y-m-d H:i:s', strtotime($request->start_date . ' 00:00:00'));
                $end_date = date('Y-m-d H:i:s', strtotime($request->end_date . ' 23:59:59'));
                $diary = DB::table('diary')
                    ->select('*')
                    ->whereBetween('created_at', [$start_date, $end_date])
                    ->get();
            }

            return response()->json([
                'diary' => $diary
            ]);
        }
        return view('system.diary.diary', compact('diary'));
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        //
    }

    /**
     * Display the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function show($id)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function edit($id)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, $id)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy($id)
    {
        //
    }
}
