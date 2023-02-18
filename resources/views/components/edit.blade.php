<!DOCTYPE html>
<html>
<style>
input[type=text], select {
  width: 100%;
  padding: 12px 20px;
  margin: 8px 0;
  display: inline-block;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
}

input[type=submit] {
  width: 100%;
  background-color: #4CAF50;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

input[type=submit]:hover {
  background-color: #45a049;
}

div {
  border-radius: 5px;
  background-color: #f2f2f2;
  padding: 20px;
}
</style>
<body>

<h3>Sua Sản Phẩm</h3>
<h3></h3>
<div>
  <form action={{route('update.product',$product->id)}} method="POST">
    @csrf
    @method('put')
    <label for="">Tên Sản Phẩm</label>
    <input type="text" name="tensp" value={{$product->tensp}}>

    <label for="">Gía Sản Phẩm</label>
    <input type="text"  name="giasp" value={{$product->giasp}}>

    <label for="">Số Lượng</label>
    <input type="text"  name="soluong" value={{$product->soluong}}>

  
    <input type="submit" value="Sửa Sản Phẩm">
  </form>
</div>

</body>
</html>


