<?php

namespace App\Http\Controllers;

use App\Models\Post;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;


class HomeController extends Controller
{
    //
    public function index()
    {
        $post = DB::table('post')
            ->select('*')
            ->where('active', 0)
            ->orderBy('id', 'DESC')
            ->first();

        return view('Home', compact('post'));
    }

    public function handleEventLike(Request $request, $id)
    {
        $postId = $request->input('post_id');
        $post = Post::find($id);

        if ($post) {
            switch ($postId) {
                case 1:
                    $post->update([
                        'like' => 'like',
                        'active' => 1
                    ]);
                    break;
                case 2:
                    $post->update([
                        'like' => 'dislike',
                        'active' => 1
                    ]);
                    break;
                default:
                    break;
            }
        }
         return redirect()->route('home');
    }
}