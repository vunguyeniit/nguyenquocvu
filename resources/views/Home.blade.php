<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta http-equiv="X-UA-Compatible" content="ie=edge">
  <link rel="stylesheet" href={{ asset('asset/css/style.css')}}>
  <title>Home</title>
</head>
<body>
  <section id="main">
    <header>
      <div class="logo">
        <img src="{{ asset('./asset/Logo.png') }}">
      </div>
       <div class="user">
        <img src="{{ asset('./asset/User.png') }}">
      </div>
    </header>
    <div class="content">
      <div class="title">
        <div class="content-title">
           <h1>A joke a day keeps the doctor away</h1>
        <p>If you joke wrong way, your teeth have to pay</p>
        </div>
       
      </div>
      <div class="story-content">
 @if (!$post)
     <p>That's all the jokes for today! Come back another day!</p>
 @else
 <p> {{$post->content_post}} </p> 
   
 @endif
        <hr>
     <div class="button">
      <div class="left-button">
        <a href="{{route('handleEventLike',['id'  => $post->id ?? " ",'post_id'=>1])}}">This is Funny!</a>
      </div>
       <div class="right-button">
        <a href="{{route('handleEventLike',['id'  => $post->id ?? " ",'post_id'=>2])}}">This is Funny!</a>
      </div>
     </div>
      </div>
    </div>
    <hr>
    <footer>
      Lorem ipsum dolor, sit amet consectetur adipisicing elit. Provident est quo obcaecati, omnis dolores minima officiis dolor? Alias, rerum atque fugit pariatur repellat sit ducimus soluta illum modi nesciunt cupiditate!
    </footer>
  </section>
</body>
</html>