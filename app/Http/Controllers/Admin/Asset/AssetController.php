<?php

namespace App\Http\Controllers\Admin\Asset;

use Illuminate\Http\Request;
use App\Http\Controllers\Controller;
use SimpleSoftwareIO\QrCode\Facades\QrCode;

class AssetController extends Controller
{
    public function index(){
        // QrCode::generate('Make me into a QrCode!');
        return view('layout.assets.asset');
    }
}
