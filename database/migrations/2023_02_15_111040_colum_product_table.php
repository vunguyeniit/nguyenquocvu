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
    //Sửa tên cột
    public function up()
    {
        Schema::table('tbl_Product', function (Blueprint $table) {
            //
            $table->renameColumn('mota', 'title');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('tbl_Product', function (Blueprint $table) {
            //
        });
    }
};
