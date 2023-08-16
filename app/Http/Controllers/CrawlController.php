<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Weidner\Goutte\GoutteFacade;

class CrawlController extends Controller
{
    public function crawlData()
    {
        $urls = [
            'https://dantri.com.vn/giao-duc/vu-lo-de-thi-sinh-loi-dung-lo-hong-tuon-tai-lieu-on-thi-cho-8-nguoi-nha-20230619164211035.htm',
            'https://dantri.com.vn/giao-duc/xet-xu-hai-giao-vien-vu-lo-de-thi-sinh-nguoi-to-cao-thoat-an-mieng-20230618231033898.htm',
            'https://dantri.com.vn/giao-duc/tphcm-cong-bo-diem-thi-lop-10-tieng-anh-nhieu-diem-10-bat-ngo-mon-van-20230619223952297.htm',
        ];
        foreach ($urls as $url) {
            $crawler = GoutteFacade::request('GET', $url);
            $title = $crawler->filter('h1.title-page.detail')->each(function ($node) {
                return $node->text();
            })[0];

            $description = $crawler->filter('h2.singular-sapo')->each(function ($node) {
                return $node->text();
            })[0];
            $thumbnail = $crawler->filter('figure.image img')->each(function ($node) {
                return $node->attr('data-src');
            })[0];
            $pageUrl = $crawler->getUri();
            $articles[] = [
                'url' => $pageUrl,
                'title' => $title,
                'description' => $description,
                'thumbnail' => $thumbnail,
            ];
        }
        return view('welcome', compact('articles'));
    }
}
