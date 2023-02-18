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

<h3>Thêm Sản Phẩm</h3>

<div>
  <form action={{route('store.product')}} method="POST">
    @csrf
    <label for="">Tên Sản Phẩm</label>
    <input type="text" name="tensp" placeholder="">

    <label for="">Gía Sản Phẩm</label>
    <input type="text"  name="giasp" placeholder="">

    <label for="">Số Lượng</label>
    <input type="text"  name="soluong" placeholder="">

  
    <input type="submit" value="Thêm Sản Phẩm">
  </form>
</div>

</body>
</html>


