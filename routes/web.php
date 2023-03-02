<?php

use App\Http\Controllers\Admin\ControllerAdmin;
use App\Http\Controllers\Admin\ControllerDevice;
use App\Http\Controllers\Admin\ControllerNubLevel;
use App\Http\Controllers\Admin\ControllerReport;
use App\Http\Controllers\Admin\ControllerService;
use App\Http\Controllers\AuthLogin\CheckLogin;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::prefix('/admin')->group(function () {
    Route::get('/login', [CheckLogin::class, 'getLogin'])->name('admin.login');
    Route::post('/login', [CheckLogin::class, 'handleLogin'])->name('admin.handlelogin');
    Route::get('/forgot', [CheckLogin::class, 'getForgot'])->name('admin.forgot');
    Route::post('/forgot', [CheckLogin::class, 'handleforgot'])->name('admin.handleforgot');
    Route::get('/reset-pass', [CheckLogin::class, 'getResPass'])->name('admin.reset');
    Route::post('/reset-pass', [CheckLogin::class, 'handlerestpas'])->name('admin.handlerest');
    Route::get('/index', [ControllerAdmin::class, 'index'])->name('admin.account-user');
    Route::resource('/device', ControllerDevice::class);
    Route::resource('/service', ControllerService::class);
    Route::resource('/nublevel', ControllerNubLevel::class);
    Route::resource('/report', ControllerReport::class);
});
