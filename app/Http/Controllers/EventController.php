<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Models\EventModel;

class EventController extends Controller
{
    public function index()
    {
        $getEvent = EventModel::all();

        return view('Event.Event', compact('getEvent'));
    }
    public function detail($id)
    {
        $detailEvent = EventModel::find($id);
        return view('Event.Detail', compact('detailEvent'));
    }
}
