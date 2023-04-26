<?php

use App\Http\Controllers\ContactController;
use App\Http\Controllers\HomeController;
use App\Http\Controllers\EventController;
use App\Http\Controllers\PaymentController;
use Illuminate\Support\Facades\Route;

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

Route::get('/', [HomeController::class, 'index'])->name('home');
Route::get('/event', [EventController::class, 'index'])->name('event');
Route::get('/detail', [EventController::class, 'detail'])->name('event-detail');
Route::get('/contact', [ContactController::class, 'index'])->name('contact');
Route::post('/contact', [ContactController::class, 'handleContact'])->name('handleContact');
Route::get('/pay', [HomeController::class, 'getPayment'])->name('pay');
Route::post('/pay-store', [PaymentController::class, 'HandelePayment'])->name('pay-store');
