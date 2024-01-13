<?php

use Illuminate\Support\Facades\Route;

use App\Http\Controllers\Auth\LoginController;
use App\Http\Controllers\Auth\ForgotController;
use App\Http\Controllers\Auth\RegisterController;
use App\Http\Controllers\Admin\Role\RoleController;
use App\Http\Controllers\Admin\User\UserController;
use App\Http\Controllers\Admin\Asset\AssetController;
use App\Http\Controllers\Auth\VerificationController;
use App\Http\Controllers\Admin\Dasboard\DasboardController;
use App\Http\Controllers\Admin\Location\LocationController;



/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "web" middleware group. Make something great!
|
*/
//Sign

Route::get('/logout', [LoginController::class, 'logout'])->name('logout');
Route::middleware('auth.check')->group(function () {

Route::controller(RegisterController::class)->group(function () {
Route::get('/sign','create')->name('sign');
Route::post('/sign','store')->name('store.sign');
});

Route::controller(VerificationController::class)->group(function () {
Route::get('/verify','verifyAccount')->name('verify.account');
Route::post('/verify','userActivate')->name('verify.activate');
});

Route::controller(LoginController::class)->group(function () {
Route::get('/login','index')->name('login');
Route::post('/login','storeLogin')->name('store.login');
});

Route::controller(ForgotController::class)->group(function () {
Route::get('/forgot','forgotPassword')->name('forgot.password');
Route::post('/forgot','forgotSendMail')->name('forgot.sendmail');
Route::get('/reset-password','checkUrlResetPass')->name('forgot.resetpass');
Route::post('/reset-password','handleResetPassword')->name('forgot.handepassword');

   });
});

Route::middleware('auth')->group(function () {

Route::get('/dasboard', [DasboardController::class, 'index'])->name('dasboard');

Route::controller(LocationController::class)->group(function () {
Route::get('/search','index')->name('search.location');
Route::get('/location','index')->name('location.list')->middleware('check.permission:location.list');
Route::get('/create-location','create')->name('location.create')->middleware('check.permission:location.create');
Route::post('/store-location','store')->name('location.store');
Route::get('/edit-location/{id?}','edit')->name('location.edit')->middleware('check.permission:location.edit');
Route::put('/edit-location/{id?}','update')->name('location.update');
Route::delete('/delete-location/{id?}','destroy')->name('location.delete')->middleware('check.permission:location.delete');
Route::post('/copy-location/{id?}','copy')->name('location.copy');
Route::post('/import-location','importLocation')->name('import.location');
Route::get('/export-location/{param?}','exportLocation')->name('export.location');
});

Route::controller(UserController::class)->group(function () {
Route::get('/user','index')->name('user');
Route::get('/create-user','create')->name('user.create');
Route::post('/store-user','store')->name('user.store');
});

Route::controller(RoleController::class)->group(function () {
Route::get('/role','index')->name('role.list')->middleware('check.permission:role.list');
Route::get('/create-role','create')->name('role.create')->middleware('check.permission:role.create');
Route::post('/store-role','store')->name('role.store');
Route::get('/edit-role/{id?}','edit')->name('role.edit')->middleware('check.permission:role.edit');
Route::put('/edit-role/{id?}','update')->name('role.update');
Route::delete('/delete-role/{id?}','destroy')->name('role.delete')->middleware('check.permission:role.delete');
Route::post('/copy-role/{id?}','copy')->name('role.copy');

});

Route::controller(AssetController::class)->group(function () {
Route::get('/asset','index')->name('asset');

});

});