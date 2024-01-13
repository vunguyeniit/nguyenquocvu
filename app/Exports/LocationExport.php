<?php

namespace App\Exports;

use App\Models\Location;
use Maatwebsite\Excel\Concerns\WithStyles;
use Maatwebsite\Excel\Concerns\WithHeadings;
use Maatwebsite\Excel\Concerns\FromCollection;
use PhpOffice\PhpSpreadsheet\Worksheet\Worksheet;
use PhpOffice\PhpSpreadsheet\Style\Alignment;
use PhpOffice\PhpSpreadsheet\Style\NumberFormat;
class LocationExport implements FromCollection, WithStyles,WithHeadings
{
    /**
    * @return \Illuminate\Support\Collection
    */
    public function collection()
    {
        return Location::all();
    }

     public function headings(): array
    {
        return [
            '#',
            'Location_name',
            'Notes',
            'Department_id',
            'Building',
            'Street_address',
            'City',
            'State',
            'Country',
            'Zip_code',
           
        ];
    }

    public function styles(Worksheet $sheet)
    {
        //Lấy ra cột và dòng cao nhất
        $highestColumn = $sheet->getHighestColumn();
        $highestRow = $sheet->getHighestRow();

        // Set auto size for all columns(Xác định kích thước cột tự động)
        for ($column = 'A'; $column <= $highestColumn; $column++) {
            $sheet->getColumnDimension($column)->setAutoSize(true);
        }

        // Set alignment(Định dạng căn chỉnh ra giữa của dòng đầu tiên)
        $sheet->getStyle("A1:J1") // header align center ('A1:'.$highestColumn.'1')
            ->getAlignment()
            ->setHorizontal(Alignment::HORIZONTAL_CENTER)
            ->setVertical(Alignment::VERTICAL_CENTER);

        // Định dạng kiểu chữ cho các ô dòng đầu tiên 
        $sheet->getStyle('A1:'.$highestColumn.'1')->applyFromArray([
            'font' => [
                'bold' => true,
            ],
        ]);
                //Định dạng kiểu chữ văn bản các  cột A-J
        return [
            1 => [
                'numberFormat' => [
                    'formatCode' => NumberFormat::FORMAT_TEXT,
                ],
            ],
            'A' => [
                'numberFormat' => [
                    'formatCode' => NumberFormat::FORMAT_TEXT,
                ],
            ],
            'B' => [
                'numberFormat' => [
                    'formatCode' => NumberFormat::FORMAT_TEXT,
                ],
            ],
            'C' => [
                'numberFormat' => [
                    'formatCode' => NumberFormat::FORMAT_TEXT,
                ],
            ],
            'D' => [
                'numberFormat' => [
                    'formatCode' => NumberFormat::FORMAT_TEXT,
                ],
            ],
            'E' => [
                'numberFormat' => [
                    'formatCode' => NumberFormat::FORMAT_TEXT,
                ],
            ],
             'F' => [
                'numberFormat' => [
                    'formatCode' => NumberFormat::FORMAT_TEXT,
                ],
            ],

             'G' => [
                'numberFormat' => [
                    'formatCode' => NumberFormat::FORMAT_TEXT,
                ],
            ],

             'H' => [
                'numberFormat' => [
                    'formatCode' => NumberFormat::FORMAT_TEXT,
                ],
            ],
             'I' => [
                'numberFormat' => [
                    'formatCode' => NumberFormat::FORMAT_TEXT,
                ],
            ],

             'J' => [
                'numberFormat' => [
                    'formatCode' => NumberFormat::FORMAT_TEXT,
                ],
            ],

        ];

    }
}
