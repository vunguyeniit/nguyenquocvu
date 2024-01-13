<?php

namespace App\Imports;

use App\Models\Location;

use Maatwebsite\Excel\Concerns\ToModel;
use Illuminate\Support\Collection;
use Maatwebsite\Excel\Concerns\ToCollection;
use Maatwebsite\Excel\Concerns\WithHeadingRow;

class LocationImport implements ToModel,WithHeadingRow
{
    /**
    * @param array $row
    *
    * @return \Illuminate\Database\Eloquent\Model|null
    */
    public function model(array $row)
    {
         return new Location([
         "location_name"=>$row["location_name"],
        "notes"=>$row["notes"],
        "department_id"=>$row["department_id"],
        "building"=>$row["building"],
        "street_address"=>$row["street_address"],
        "city"=>$row["city"],
        "state"=>$row["state"],
        "country"=>$row["country"],
        "zip_code"=>$row["zip_code"],
     ]);
    }

}
