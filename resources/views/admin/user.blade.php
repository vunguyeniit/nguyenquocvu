
<div class="img_content">
                     
  <img srcset="{{ asset('./assets/images/user.png 2x') }}">
  <div class="user-content">
      <p>Xin Chào</p>
      @php
      $data =  Session::get('key');
      @endphp
       <h3>{{$data->username}}</h3>
     
  </div>
</div>