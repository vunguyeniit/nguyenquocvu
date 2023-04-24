<?php

namespace App\Http\Controllers;

use App\Models\ContactModel;
use Illuminate\Http\Request;

class ContactController extends Controller
{
    public function index()
    {
        return view('Contact.Contact');
    }

    public function handleContact(Request $request)
    {
        // dd($request->all());
        $contact = ContactModel::create($request->all());
        if ($contact) {
            return redirect()->back()->with('success', 'successfully');
        }
    }
}
