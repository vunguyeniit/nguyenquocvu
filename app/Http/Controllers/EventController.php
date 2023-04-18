<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

class EventController extends Controller
{
    public function index()
    {
        return view('Event.Event');
    }
    public function detail()
    {
        return view('Event.Detail');
    }
}
