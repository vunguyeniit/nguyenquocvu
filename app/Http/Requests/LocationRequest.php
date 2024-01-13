<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class LocationRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     */
    public function authorize(): bool
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array<string, \Illuminate\Contracts\Validation\ValidationRule|array<mixed>|string>
     */
    public function rules(): array
    {
        return [
        'location_name'=>'required',
        'notes'=>'required',
        'building'=>'required',
        'street_address'=>'required',
        'city'=>'required',
        'zip_code'=>'required',
        ];
    }


      public function messages(): array
    {
        return [
        'location_name.required'=>':attribute trường không được để trống',
        'notes.required'=>':attribute trường không được để trống',
        'building.required'=>':attribute trường không được để trống',
        'street_address.required'=>':attribute trường không được để trống',
        'city.required'=>':attribute trường không được để trống',
        'zip_code.required'=>':attribute trường không được để trống',
        ];
    }
}
