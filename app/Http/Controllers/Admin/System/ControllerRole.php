<?php

namespace App\Http\Controllers\Admin\System;

use App\Http\Controllers\Controller;
use App\Models\System\role;
use Illuminate\Http\Request;

class ControllerRole extends Controller
{

    public function index(Request $request)

    {
        $role = role::all();

        if ($keyword = $request->search) {
            $role = role::where('rolename', 'like', '%' . $keyword . '%')
                ->orWhere('description', 'LIKE', '%' . $keyword . '%')
                ->get();
        }
        return view('system.role.role', compact('role'));
    }


    public function create()
    {
        return view('system.role.create');
    }


    public function store(Request $request)
    {
        if ($key = $request->checkbox_a == true) {
            //Nhóm Chức năng A
            $key = 0;
        } else if ($key = $request->checkbox_b == true) {
            //Nhóm Chức năng B
            $key = 1;
        }
        role::create([
            'rolename' => $request->rolename,
            'member' => 6,
            'description' => $request->description,
            'permission' => $key,


        ]);
        return redirect()->route('role.create');
    }


    public function show($id)
    {
        //
    }


    public function edit($id)
    {
        //
        $role = role::find($id);
        return view('system.role.edit', compact('role'));
    }


    public function update(Request $request, $id)
    {
        //
        $role =  role::find($id);
        if ($key = $request->checkbox_a == true) {
            //Nhóm Chức năng A
            $key = 0;
        } else if ($key = $request->checkbox_b == true) {
            //Nhóm Chức năng B
            $key = 1;
        }

        $role->update([
            'rolename' => $request->rolename,
            'member' => 6,
            'description' => $request->description,
            'permission' => $key,
        ]);





        return redirect()->route('role.create');
    }


    public function destroy($id)
    {
        //
    }
}
