<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\DBAL\TimestampType;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Crypt;
use Carbon\Carbon;

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
        // $data = [
        //     'username' => 'Nguyễn Quốc Vũ',
        //     'loginname' => 'nguyenvu2002',
        //     'password' => bcrypt('123456789'),
        //     'phone' => '0125349999',
        //     'email' => 'nguyenquocvu2002814@gmail.com',
        //     'role' => 'Kế Toán',

        // ];
        // DB::table('user')->insert($data);

        $startOfDayFormatted =  Carbon::now()->format('d-m-Y H:i:s ');
        $data = [
            'username' => 'tuyetnguyen@15',
            'usetime' => $startOfDayFormatted,
            'ip' => '192.168.3.1',
            'perform' => 'Cập nhật thông tin dịch vụ DV_01',
            'created_at' => now(),
            'updated_at' => now(),


        ];

        DB::table('diary')->insert($data);
    }
}