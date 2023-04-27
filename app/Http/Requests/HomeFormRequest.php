<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class HomeFormRequest extends FormRequest
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
     * @return array<string, \Illuminate\Contracts\Validation\Rule|array|string>
     */
    public function rules(): array
    {
        return [
            'name_ticket' => 'required',
            'price_ticket' => 'required||integer',
            'date' => 'required',
            'username' => 'required',
            'phone' => 'required||integer||min:10',
            'email' => 'required||email',
        ];
    }


    public function messages()
    {
        return [
            'name_ticket.required' => ':attribute Vui lòng không để trường chống',
            'price_ticket.required' => ':attribute Vui lòng không để trường chống',
            'price_ticket.integer' => ':attribute Trường bắt buộc là số',
            'username.required' => ':attribute Vui lòng không để trường chống',
            'phone.required' => ':attribute Vui lòng không để trường chống',
            'phone.integer' => ':attribute Trường bắt buộc là số',
            'phone.min' => ':attribute Trường bắt buộc là 10 kí tự',
            'email.required' => ':attribute Vui lòng không để trường chống',
            'email.email' => ':attributeTrường bắt buộc là email',
        ];
    }
    public function attributes()
    {
        return [
            'name_ticket' => 'Gói vé',
            'price_ticket' => 'Số lượng vé',
            'date' => 'Thời gian sử dụng',
            'username' => 'Tên người dùng',
            'phone' => 'Số ddienj thoại',
            'email' => 'Email',
        ];
    }
}
