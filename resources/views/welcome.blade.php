
<!DOCTYPE html>
<html>
<head>
    <title>Crawl Data</title>
</head>
<style>
  h1{
    text-align: center;
  }
  .content-right a{
    display: inline-block;
    text-align: center;
    text-decoration: none;
    font-size: 25px;
    color: #000;
    font-weight: bold;
    padding: 10px;
     white-space: nowrap;
  }
  .content-right a:hover{
    color: blue;
  }
  .content{
   padding: 0 25px 0 90px;
   margin: 30px;

  }
  .row{
    display: flex;
  }
  .row img{
    width: 30%;
    border-radius: 5px;
  }
   .content-right{
    margin: 0px 0 0 20px
   }
   .content-right p{
    font-size: 18px;
  
  }
  .content-right h2{
    margin: 0;
  }
</style>
<body>
    <h1>Crawled Data</h1>

    @foreach ($articles as $article)
        <div class="content">
            <div class="row">
                <img src="{{ $article['thumbnail'] }}" alt="Thumbnail">
                <div class="content-right">
                      <a href="{{ $article['url'] }}">{{ $article['title'] }}</a>
                      <p>{{ $article['description'] }}</p>
                </div>
            </div>
        </div>
    @endforeach
</body>
</html>