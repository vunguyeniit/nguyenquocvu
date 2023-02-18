<x-app-layout>
    <x-slot name="header">
        <h2 class="font-semibold text-xl text-gray-800 leading-tight">
            {{ __('') }}
        </h2>
    </x-slot>
    <div class="py-12">
        <div class="max-w-7xl mx-auto sm:px-6 lg:px-8">
            <div class="bg-white overflow-hidden shadow-sm sm:rounded-lg">
                <div class="p-6 text-gray-900">
                    <!DOCTYPE html>
                    <html>
                    <head>
                    <style>
                    #customers {
                      font-family: Arial, Helvetica, sans-serif;
                      border-collapse: collapse;
                      width: 100%;
                    }
                    
                    #customers td, #customers th {
                      border: 1px solid #ddd;
                      padding: 8px;
                    }
                    
                    #customers tr:nth-child(even){background-color: #f2f2f2;}
                    
                    #customers tr:hover {background-color: #ddd;}
                    
                    #customers th {
                      padding-top: 12px;
                      padding-bottom: 12px;
                      text-align: left;
                      background-color: #04AA6D;
                      color: white;
                    }
                    h1{
                      font-size: 30px;
                    }
                 
                    </style>
                    </head>
                    <body>
                        <div style="text-align:right; font-size:20px" >
                    <button><a href={{route('add.product')}}>Thêm</a></button>
                        </div>
                    <h1>Danh Sách Sản Phẩm</h1>
                    
                    <table id="customers">
                      <tr>
                        <th>Tên Sản Phẩm</th>
                        <th>Gía Sản Phẩm</th>
                        <th>Số Lượng</th>
                        <th colspan="2">Action</th>
                      </tr>
                      @foreach($product as $item)
                      <tr>
                        <td>{{$item['tensp']}}</td>
                        <td>{{$item['giasp']}}</td>
                        <td>{{$item['soluong']}}</td>
                        <td><a href={{route('edit.product',$item['id'])}}>Sửa</a></td>
                        <td><a href={{route('delete.product',$item['id'])}}>Xoá</a></td>
                      </tr>
                      @endforeach
                    </table>
                    
                    </body>
                    </html>   
                </div>
            </div>
        </div>
    </div>
</x-app-layout>



