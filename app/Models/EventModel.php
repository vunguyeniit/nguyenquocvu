<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class EventModel extends Model
{
    use HasFactory;
    protected $table = "event";
    protected $fillable = [
        'title_event',
        'start_day',
        'end_day',
        'price_ticket',

    ];
}
