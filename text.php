<?php
function miniMaxSum($array) {
    // Sắp xếp mảng theo thứ tự tăng dần
     for ($i = 0; $i < count($array) - 1; $i++) {
        for ($j = 0; $j < count($array) - $i - 1; $j++) {
             if ($array[$j] > $array[$j + 1]) {
                $temp = $array[$j];
                $array[$j] = $array[$j + 1];
                $array[$j + 1] = $temp;
            }
        }
    }
    $minNub= array_slice($array, 0, 4);
    $maxNub = array_slice($array, -4);
    // Tính tổng của 4 phần tử nhỏ nhất và 4 phần tử lớn nhất
    $minSum= array_sum($minNub);
    $maxSum = array_sum($maxNub);
  echo "Tổng của 4 phần tử nhỏ nhất và lớn nhất là: " . $minSum ."-".$maxSum;
}
$array = [1,3,5,7,9];
miniMaxSum($array);
?>