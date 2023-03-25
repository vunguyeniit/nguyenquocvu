@extends('layout.Clone-Admin')
<!DOCTYPE html>
<html lang="en">
<head>
	<title>Dashboard</title>
 <link rel="stylesheet" href="{{ asset('assets/css/light.css') }}">

</head> 
<body data-theme="default" data-layout="fluid" data-sidebar-position="left" data-sidebar-layout="default">
	
    <section id="content">
    @include('sidebar.sidebar')
      <main class="p-0">

       
        <div class="data mt-0 " style="grid-gap:0;">
            <div class="content-data" style=" flex-basis: 0;  padding: 0;background: none;  border-radius: 0;  box-shadow:none;
              ">
                <div class="head">
                  <main class="content d-flex">
                    <div class="container-fluid p-0">
            
                      <div class=" mb-2 mb-xl-3">
                        <div class="col-auto d-none d-sm-block">
                          <h3 style="font-size: 3rem">Dashboard</h3>
                          <strong style="font-size: 2.5rem;color:#ff7505;margin:5rem 0 3rem 0;display:block">Biểu đồ cấp số</strong> 
                        </div>

                       
                      </div>
                    
                      <div class="row">
                        
                        <div class="col-12 " style="width:95%">
            
                          <div class="row">
                            <div class="col-sm-6 col-xl-3">
                              <div class="card">
                                <div class="card-body">
                                  <div class="row align-items-center">


                                    <div class="col-auto">
                                      <img srcset="{{ asset('./assets/images/1.png 2.5x') }}"/>
                                     
                                    </div>
                                    <div class="col mt-0">
                                      <h5 class="card-title">Số thứ tự đã cấp</h5>
                                    </div>
                
                                 
                                  </div>
                                  <div class="mb-0 d-flex justify-content-between mt-4 align-items-center">
                                    
                                    <span class="text-muted">{{$data['count_sum']}}</span>
                                    <span class="badge badge-success-light"> <i class="mdi mdi-arrow-bottom-right"></i> 3.65% </span>
                                    
                                  </div>
                                </div>
                              </div>
                            </div>
                            <div class="col-sm-6 col-xl-3">
                              <div class="card">
                                <div class="card-body">
                                  <div class="row align-items-center">


                                    <div class="col-auto">
                                      <img srcset="{{ asset('./assets/images/2.png 2.5x') }}"/>
                                    
                                    </div>
                                    <div class="col mt-0">
                                      <h5 class="card-title">Số thứ tự đã sử dụng</h5>
                                    </div>
                
                                   
                                  </div>
                               
                                  <div class="mb-0 d-flex justify-content-between mt-4 align-items-center">
                                    <span class="text-muted">{{$data['count2']}}</span>
                                    <span class="badge badge-danger-light"> <i class="mdi mdi-arrow-bottom-right"></i> -5.25% </span>
                                  
                                  </div>
                                </div>
                              </div>
                            </div>
                            <div class="col-sm-6 col-xl-3">
                              <div class="card">
                                <div class="card-body">
                                  <div class="row align-items-center">


                                    <div class="col-auto">
                                  
                                      <img srcset="{{ asset('./assets/images/3.png 2.5x') }}"/>
                                    </div>
                                    <div class="col mt-0">
                                      <h5 class="card-title">Số thứ tự đang chờ</h5>
                                    </div>
                
                                  </div>
                            
                                  <div class="mb-0 d-flex justify-content-between mt-4 align-items-center">
                                    <span class="text-muted">{{$data['count1']}}</span>
                                    
                                    <span class="badge badge-success-light"> <i class="mdi mdi-arrow-bottom-right"></i> 4.65% </span>

                                  </div>
                                </div>
                              </div>
                            </div>
                            <div class="col-sm-6 col-xl-3">
                              <div class="card">
                                <div class="card-body">
                                  <div class="row align-items-center">


                                    <div class="col-auto">
                                  
                                      <img srcset="{{ asset('./assets/images/4.png 2.5x') }}"/>
                                    </div>
                                    <div class="col mt-0">
                                      <h5 class="card-title">Số thứ tự đã bỏ qua</h5>
                                    </div>
                
                                  </div>
                            
                                  <div class="mb-0 d-flex justify-content-between mt-4 align-items-center">
                                    <span class="text-muted">{{$data['count3']}}</span>
                                    
                                    <span class="badge badge-success-light"> <i class="mdi mdi-arrow-bottom-right"></i> 4.65% </span>

                                  </div>
                                </div>
                              </div>
                            </div>
                          </div>
            
            
            
            
                          
                          <div class="card flex-fill w-100">
            
                          
                
                            <div class="card-header">
                              <div class="float-end">
                         
                                <form class="row g-2">
                                  {{-- <label for="">Xem Theo</label> --}}
                                  <div class="col-auto">
                                 
                                    <select class="form-select form-select-sm bg-light border-0">
                                      {{-- <option>Jan</option> --}}
                                      <option value="0">Ngày</option>
                                      <option value="1">Tuần</option>
                                      <option value="2">Tháng</option>
                                    </select>
                                  </div>
                              
                                </form>
                              </div>
                              <h5 class="card-title mb-0 fs-1">Bảng thống kê theo tuần</h5>
                            </div>
                            <div class="card-body pt-2 pb-3">
                              <div class="chart chart-md">
                                <canvas id="chartjs-dashboard-line"></canvas>
                              </div>
                            </div>
                          </div>
                        </div>
                       
                      </div>
                    </div>
                
            
            
            
                    <div class="col-12 col-lg-4 d-flex">
                      <div class="card flex-fill w-100">
                        <div class="card-header">

                          <nav style="margin: 0">
                            <div class="header-right">
                                <div class="header-left">
                                    
                                </div>
                                <div class="profile">
                                    <div class="icon-bell">
                                        <i class="fa-solid fa-bell"></i>
                                    </div>
                                    @include('admin.user')
                
                                </div>
                
                            </div>
                
                        
                        </nav>
                          <div class="float-end">
                           
                          </div>
                         
                        </div>

                        <div class="card-body px-4">
                          <div id="usa_map" style="height:600px;">
                           <h2 class="" >Tổng quan</h2>
                           {{-- // --}}
                  <div class="box">
                    <div class="box-container">
                      <div class="box-percent">
                        <img srcset="{{ asset('./assets/images/icon1.png 2x') }}"/>
                      </div>

                      <div class="list-nub">
                        <strong class="d-block">{{$data['count_device']}}</strong>
                        <strong class="d-block fs-2"><img  srcset="{{ asset('./assets/images/icon_1.png 1.5x') }}">  Thiết bị</strong>
                    </div>

                  </div>
                      <div class="box-content">
                     
                     
                        <div class="list-status">
                                  <p><i class="fa-solid fa-circle text-warning fs-6"></i> Đang hoạt động </p>
                                  <p><i class="fa-solid fa-circle text-secondary fs-6"></i> Ngưng hoạt động </p>
                        </div>
                    
                        <div class="list-content">
                          
                            <a class="d-block" href="http://">{{$data['count4']}}</a>
                          <a class="d-block " href="http://">{{$data['count5']}}</a> 
                    </div>
                 
                  
                    </div>
                  </div>
{{-- // --}}

<div class="box">
  <div class="box-container">
    <div class="box-percent">
      <img srcset="{{ asset('./assets/images/icon2.png 2x') }}"/>
    </div>

    <div class="list-nub">
      <strong class="d-block">{{$data['count_service']}}</strong>
      <strong class="d-block fs-2"><img srcset="{{ asset('./assets/images/icon_2.png 1.5x') }}">  Dịch vụ</strong>
  </div>
</div>
    <div class="box-content">
     
      <div class="list-status" >
                <p><i class="fa-solid fa-circle text-primary fs-6"></i> Đang hoạt động </p>
                <p><i class="fa-solid fa-circle text-secondary fs-6"></i> Ngưng hoạt động </p>
      </div>
  
      <div class="list-content">
        
          <a style="color:#4277FF" class="d-block" href="http://">{{$data['count6']}}</a>
   
        <a style="color:#4277FF" class="d-block " href="http://">{{$data['count7']}}</a>
       

  
  </div>
  </div>
</div>

    {{-- // --}}

    <div class="box">
      <div class="box-container">
        <div class="box-percent">
          <img srcset="{{ asset('./assets/images/icon3.png 2x') }}"/>
        </div>
       
        <div class="list-nub">
          <strong class="d-block">{{$data['count_sum']}}</strong>
          <strong class="d-block fs-2"><img srcset="{{ asset('./assets/images/icon_3.png 1.5x') }}">  Cấp số</strong>
      </div>
    </div>
        <div class="box-content">
          <div class="list-status">
                    <p><i class="fa-solid fa-circle text-success fs-6"></i> Đang chờ</p>
                    <p><i class="fa-solid fa-circle text-secondary fs-6"></i> Đã sử dụng</p>
                    <p ><i class="fa-solid fa-circle text-info fs-6" ></i> Bỏ qua</p>
                  
            </div>
                  <div class="list-content">
                    <a style="color:#35C75A"  class="d-block " href="http://">{{$data['count1']}}</a>
                    <a style="color:#35C75A" class="d-block " href="http://">{{$data['count2']}}</a>
                     <a style="color:#35C75A" class="d-block " href="http://">{{$data['count3']}}</a>
            
                  
                
                </div>
      </div>
    </div>
 {{-- // --}}

  

   <div id="datepicker" data-date=""></div>
   <input type="hidden" id="my_hidden_input">
  




{{-- 
 // --}}
         </div>
                           {{-- // --}}












                          </div>
                        </div>
                      </div>
                    </div>
                  </main>

                     
                    
    
    

                      
                     
//
   

                </div>
            </div>

     
  

    </main>


	
		</div>
	</div>
</section>
  <script>
    document.addEventListener("DOMContentLoaded", function() {
        var link = document.querySelector("link[href='https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css']");
       
        
        if (link) {
            link.parentNode.removeChild(link);
        }
       
    });
</script>



	<script>
		document.addEventListener("DOMContentLoaded", function() {
			var ctx = document.getElementById("chartjs-dashboard-line").getContext("2d");
			var gradientLight = ctx.createLinearGradient(0, 0, 0, 225);
			gradientLight.addColorStop(0, "rgba(215, 227, 244, 1)");
			gradientLight.addColorStop(1, "rgba(215, 227, 244, 0)");
			var gradientDark = ctx.createLinearGradient(0, 0, 0, 225);
			gradientDark.addColorStop(0, "rgba(51, 66, 84, 1)");
			gradientDark.addColorStop(1, "rgba(51, 66, 84, 0)");
			// Line chart
			new Chart(document.getElementById("chartjs-dashboard-line"), {
				type: "line",
				data: {
					labels: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"],
					datasets: [{
						label: "Sales ($)",
						fill: true,
						backgroundColor: window.theme.id === "light" ? gradientLight : gradientDark,
						borderColor: window.theme.primary,
						data: [
							2115,
							1562,
							1584,
							1892,
							1587,
							1923,
							2566,
							2448,
							2805,
							3438,
							2917,
							3327
						]
					}]
				},
				options: {
					maintainAspectRatio: false,
					legend: {
						display: false
					},
					tooltips: {
						intersect: false
					},
					hover: {
						intersect: true
					},
					plugins: {
						filler: {
							propagate: false
						}
					},
					scales: {
						xAxes: [{
							reverse: true,
							gridLines: {
								color: "rgba(0,0,0,0.0)"
							}
						}],
						yAxes: [{
							ticks: {
								stepSize: 1000
							},
							display: true,
							borderDash: [3, 3],
							gridLines: {
								color: "rgba(0,0,0,0.0)",
								fontColor: "#fff"
							}
						}]
					}
				}
			});
		});
	</script>




</body>
<script src="{{ asset('assets/js/app.js') }}"></script>
{{-- <script async="" src="{{ asset('assets/js/js.js') }}?id=UA-120946860-10"></script> --}}

</html>