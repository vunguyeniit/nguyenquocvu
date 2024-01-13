<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('location', function (Blueprint $table) {
           $table->id();
            $table->string('location_name',50);
            $table->text('notes',100);
            $table->foreignId('department_id')->constrained('department');
            $table->string('building',50);
            $table->string('street_address',100);
            $table->string('city',50);
            $table->boolean('state')->default(false);
            $table->string('country',50);
            $table->string('zip_code',50);
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('location');
    }
};
