<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Crypt;

class UserSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        //
        $data = [
            'username' => 'Nguyễn Quốc Vũ',
            'loginname' => 'nguyenvu2002',
            'password' => bcrypt('123456789'),
            'phone' => '0125349999',
            'email' => 'quocvu@gmail.com',
            'role' => 'Kế Toán',
        ];
        DB::table('user')->insert($data);
    }
}
