<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;

class DepartmentSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {

         $data = [
            ['department_name' => 'Buying Department'],
            ['department_name' => 'Finance Department'],
            ['department_name' => 'IT Department'],
            ['department_name' => 'Marketing and Advertising Department'],
            ['department_name' => 'Research and Development Department'],
            ['department_name' => 'Project Management Department'],
            ['department_name' => 'Product Development Department'],
            ['department_name' => 'Human Resources Department'],
            ['department_name' => 'Social Impact Department'],
            ['department_name' => 'Corporate Communications Department'],
            ['department_name' => 'Sales and Distribution Division Department'],
            // Thêm dòng dữ liệu khác theo mẫu của bạn
        ];
         DB::table('department')->insert($data);
    }
}
