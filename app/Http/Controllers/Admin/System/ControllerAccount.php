<?php

namespace App\Http\Controllers\Admin\System;

use App\Http\Controllers\Controller;
use App\Models\System\account;
use Illuminate\Http\Request;

class ControllerAccount extends Controller
{

    public function index(Request $request)
    {
        $query = account::query();
        $account = $query->paginate(4);

        if ($request->ajax()) {
            $account_status = $query->where(['status' => $request->statusid])->get();

            return response()->json([
                'account' => $account_status
            ]);
        }

        if ($keyword = $request->search) {
            $account = account::where('username', 'like', '%' . $keyword . '%')
                ->orWhere('fullname', 'like', '%' . $keyword . '%')
                ->paginate(4);
        }
        return view('system.account.account', compact('account'));
    }


    public function create()
    {
        return view('system.account.create');
    }


    public function store(Request $request)
    {

        account::create($request->all());
        return redirect()->route('account.index');
    }


    public function show($id)
    {
    }


    public function edit($id)
    {
        $account = account::find($id);
        return view('system.account.edit', compact('account'));
    }


    public function update(Request $request, $id)
    {
        $update = account::find($id);
        $update->update($request->all());
        return redirect()->route('account.index');
    }


    public function destroy($id)
    {
    }
}
