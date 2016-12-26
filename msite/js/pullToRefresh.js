
$(function () {
    var pullTemplate = '<div class="weui-pull-to-refresh-layer">'
        + '<div class="pull-to-refresh-arrow"></div>'
        + '<!-- 上下拉动的时候显示的箭头 -->'
        + '<div class="pull-to-refresh-preloader"></div>'
        + '<!-- 正在刷新的菊花 -->'
        + '<div class="down">下拉刷新</div>'
        + '<!-- 下拉过程显示的文案 -->'
        + '<div class="up">释放刷新</div>'
        + '<!-- 下拉超过50px显示的文案 -->'
        + '<div class="refresh">正在刷新...</div>'
        + '<!-- 正在刷新时显示的文案 -->'
        + '</div>';


    var moreTemplate = '<!--上拉刷新开始 -->'
        + '<div class="weui-infinite-scroll">'
        + '<div class="infinite-preloader"></div>'
        + '正在加载...'
        + '</div>'
        + '<!--上拉刷新结束 -->';
    
    //$(".hot-pullToRefresh").children().first().before(pullTemplate);
    $(".hot-pullToRefresh").children().last().after(moreTemplate);
    //$('.hot-pullToRefresh').pullToRefresh();
    $(".weui-infinite-scroll").hide();
});
