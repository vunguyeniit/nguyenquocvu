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
    //Tạo 1 bảng mới
    public function up()
    {
        Schema::create('tbl_Product', function (Blueprint $table) {
            $table->id();
            $table->string('tensp');
            $table->string('giasp');
            $table->string('soluong');
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
        Schema::dropIfExists('tbl_Product');
    }
};
