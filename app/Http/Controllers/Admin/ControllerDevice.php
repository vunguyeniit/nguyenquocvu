<?php

namespace App\Http\Controllers\Admin;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\AdminModel\ModelDevice;

class ControllerDevice extends Controller
{

    public function index()
    {
        $device = ModelDevice::all();
        return view('device.device', compact('device'));
    }


    public function create()
    {

        return view('device.create');
    }


    public function store(Request $request)
    {

        $input = $request->all();
        $device = $input['deviceuse'];
        $input['deviceuse'] = implode(',', $device);
        ModelDevice::create($input);
        return redirect()->route('device.index');
    }



    public function show($id)
    {

        return view('device.detail');
    }

    public function edit($id)
    {

        return view('device.edit');
    }


    public function update(Request $request, $id)
    {
    }


    public function destroy($id)
    {
    }
}
