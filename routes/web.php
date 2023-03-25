<?php

use App\Http\Controllers\Admin\ControllerAdmin;
use App\Http\Controllers\Admin\ControllerDashboard;
use App\Http\Controllers\Admin\ControllerDevice;
use App\Http\Controllers\Admin\ControllerNubLevel;
use App\Http\Controllers\Admin\ControllerReport;
use App\Http\Controllers\Admin\ControllerService;
use App\Http\Controllers\Admin\PDFController;
use App\Http\Controllers\Admin\System\ControllerAccount;
use App\Http\Controllers\Admin\System\ControllerDiary;
use App\Http\Controllers\Admin\System\ControllerRole;
use App\Http\Controllers\AuthLogin\CheckLogin;
use Illuminate\Support\Facades\Route;


Route::prefix('/admin')->group(function () {

    Route::get('/logout', [CheckLogin::class, 'logout'])
        ->name('admin.logout');
    Route::get('/login', [CheckLogin::class, 'getLogin'])
        ->middleware('checklogout')
        ->name('admin.login');

    Route::get('/index', [CheckLogin::class, 'indexLogin'])
        ->middleware('checkuser')
        ->name('admin.indexLogin');

    Route::post('/login', [CheckLogin::class, 'handleLogin'])
        ->name('admin.handlelogin');
});

Route::get('/forgot', [CheckLogin::class, 'getForgot'])->name('admin.forgot');
Route::post('/forgot', [CheckLogin::class, 'handleforgot'])->name('admin.handleforgot');
Route::get('/reset-pass', [CheckLogin::class, 'getResPass'])->name('admin.reset');
Route::post('/reset-pass', [CheckLogin::class, 'handlerestpas'])->name('admin.handlerest');
Route::get('/generate-pdf', [PDFController::class, 'generatePDF'])->name('download.generate-pdf');
Route::prefix('/admin')->middleware('checkuser')->group(function () {
    Route::resource('/das', ControllerDashboard::class);
    Route::resource('/device', ControllerDevice::class);
    Route::resource('/service', ControllerService::class);
    Route::resource('/nublevel', ControllerNubLevel::class);
    Route::resource('/report', ControllerReport::class);
    Route::resource('/role', ControllerRole::class);
    Route::resource('/account', ControllerAccount::class);
    Route::resource('/diary', ControllerDiary::class);
});
