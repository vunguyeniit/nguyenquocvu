<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

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
            'username' => 'Nguyễn Chí Linh',
            'loginname' => 'nguyenlinh2002',
            'password' => bcrypt('123456'),
            'phone' => '0125349888',
            'email' => 'nqvgaming@gmail.com',
            'role' => 'Dev',
        ];
        DB::table('user')->insert($data);
    }
}
