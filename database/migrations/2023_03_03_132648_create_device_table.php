<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('device', function (Blueprint $table) {
            $table->id();
            $table->string('devicecode');
            $table->string('devicename');
            $table->string('devicetype');
            $table->string('username');
            $table->string('addressip');
            $table->string('password');
            $table->string('deviceuse');
            $table->boolean('activestatus')->default(0);
            $table->boolean('connectionstatus')->default(0);
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('device');
    }
};
