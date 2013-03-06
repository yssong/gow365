/*
* jQuery Infinite Carousel
* @author admin@catchmyfame.com - http://www.catchmyfame.com
* @version 2.0.2
* @date June 12, 2010
* @category jQuery plugin
* @copyright (c) 2009 admin@catchmyfame.com (www.catchmyfame.com)
* @license CC Attribution-Share Alike 3.0 - http://creativecommons.org/licenses/by-sa/3.0/

* It has been modified by (c)HiCompass * 
*/

(function ($) {
    $.fn.extend({
        infiniteCarousel: function (options) {
            var defaults =
			{
			    transitionSpeed: 800,
			    displayTime: 3000,
			    easeLeft: 'linear',
			    easeRight: 'linear',
			    imagePath: '',
			    inView: 1,
			    padding: '0px',
			    advance: 1,
			    showControls: true,
			    autoStart: false, //true,
			    prevNextInternal: true,
			    enableKeyboardNav: true,
			    onSlideStart: function () { },
			    onSlideEnd: function () { },
			    onPauseClick: function () { }
			};
            var options = $.extend(defaults, options);

            return this.each(function () {
                var randID = Math.round(Math.random() * 100000000);
                var o = options;
                var obj = $(this);
                var autopilot = o.autoStart;
                var viewCount = o.advance;

                var numImages = $('img', obj).length;
                var imgHeight = o.imgHeight;
                var imgWidth = o.imgWidth * viewCount;

                if (o.inView > numImages - 1) o.inView = numImages - 1;
                $(obj).css({ 'position': 'relative', 'overflow': 'hidden' }).width((imgWidth * o.inView) + (o.inView * parseInt(o.padding) * 2)).height(imgHeight + (parseInt(o.padding) * 2));
                $('ul', obj).css({ 'list-style': 'none', 'margin': '0', 'padding': '0', 'position': 'relative' }).width(imgWidth * numImages);
                $('li', obj).css({ 'display': 'inline', 'float': 'left', 'padding': o.padding });

                $('li:last', obj).prependTo($('ul', obj));
                $('ul', obj).css('left', -(imgWidth / viewCount) - (parseInt(o.padding) * 2) + 'px').width(imgWidth * numImages);

                if (o.showControls) {
                    html = '<div id="play_pause_btn' + randID + '" style="cursor:pointer;position:absolute;top:3px;right:17px;border:none;width:11px;height:11px;background:url(' + o.imagePath + 'playpause.gif) no-repeat 0 0"></div>';
                    $(obj).append(html);
                    var status = 'play';
                    $('#play_pause_btn' + randID).css('opacity', .5).hover(function () { $(this).animate({ opacity: '1' }, 250) }, function () { $(this).animate({ opacity: '.5' }, 250) });
                    $('#play_pause_btn' + randID).click(function () {
                        status = (status == 'play') ? 'pause' : 'play';
                        (status == 'play') ? forceStart() : forcePause();
                    });

                    if (!o.prevNextInternal) {
                        wrapID = $(obj).attr('id') + 'Wrapper';
                        $(obj).wrap('<div id="' + wrapID + '"></div>').css('margin', '0 auto');
                        $('#' + wrapID).css('position', 'relative').width(($(obj).width() + 40 + parseInt($(obj).css('padding-left')) + parseInt($(obj).css('padding-right'))));
                    }

                    html = '<div id="btn_rt' + randID + '" style="position:absolute;right:3px;top:3px;cursor:pointer;border:none;width:11px;height:11px;background:url(' + o.imagePath + 'right.gif) no-repeat 0 0"></div>';
                    html += '<div id="btn_lt' + randID + '" style="position:absolute;right:31px;top:3px;cursor:pointer;border:none;width:11px;height:11px;background:url(' + o.imagePath + 'left.gif) no-repeat 0 0"></div>';
                    (o.prevNextInternal) ? $(obj).append(html) : $('#' + wrapID).append(html);

                    $('#btn_rt' + randID).css('opacity', .5).click(function () {
                        forcePrevNext('next');
                    }).hover(function () { $(this).animate({ opacity: '1' }, 250) }, function () { $(this).animate({ opacity: '.5' }, 250) });
                    $('#btn_lt' + randID).css('opacity', .5).click(function () {
                        forcePrevNext('prev');
                    }).hover(function () { $(this).animate({ opacity: '1' }, 250) }, function () { $(this).animate({ opacity: '.5' }, 250) });

                }

                function forcePrevNext(dir) {
                    o.onPauseClick.call(this);
                    $('#btn_rt' + randID).unbind('click');
                    $('#btn_lt' + randID).unbind('click');
                    setTimeout(function () { $('#play_pause_btn' + randID).css('background-position', '0 -11px') }, o.transitionSpeed - 1);
                    autopilot = 0;
                    status = 'pause';
                    clearTimeout(clearInt);
                    (dir == 'prev') ? moveRight() : moveLeft();
                    $('#play_pause_btn' + randID).unbind('click');
                    setTimeout(function () {
                        $('#play_pause_btn' + randID).bind('click', function () { forceStart(); });
                        $('#btn_rt' + randID).bind('click', function () { forcePrevNext('next') });
                        $('#btn_lt' + randID).bind('click', function () { forcePrevNext('prev') });
                    }, o.transitionSpeed);
                }


                function forcePause() {
                    $('#play_pause_btn' + randID).unbind('click');
                    if (autopilot) {
                        o.onPauseClick.call(this);
                        $('#play_pause_btn' + randID).fadeTo(250, 0, function () { $(this).css({ 'background-position': '0 -11px', 'opacity': '.5' }); }).animate({ opacity: .5 }, 250);
                        autopilot = 0;
                        $('#progress' + randID).stop().fadeOut();
                        clearTimeout(clearInt);
                        setTimeout(function () { $('#play_pause_btn' + randID).bind('click', function () { forceStart(); }) }, o.transitionSpeed);
                    }
                }

                function forceStart() {
                    $('#play_pause_btn' + randID).unbind('click');
                    if (!autopilot) {
                        setTimeout(function () { $('#play_pause_btn' + randID).css('background-position', '0 0') }, o.transitionSpeed - 1);
                        autopilot = 1;
                        moveLeft();
                        clearInt = setInterval(function () { moveLeft(); }, o.displayTime + o.transitionSpeed);
                        setTimeout(function () { $('#play_pause_btn' + randID).bind('click', function () { forcePause(); }) }, o.transitionSpeed);
                    }
                }

                function preMove() {
                    if (o.showControls && o.prevNextInternal) {
                        $('#play_pause_btn' + randID).fadeOut(200);
                        $('#btn_lt' + randID).fadeOut(200);
                        $('#btn_rt' + randID).fadeOut(200);
                    }
                }

                function postMove() {
                    if (o.showControls && o.prevNextInternal) {
                        $('#play_pause_btn' + randID).fadeIn(200);
                        $('#btn_lt' + randID).fadeIn(200);
                        $('#btn_rt' + randID).fadeIn(200);
                    }
                }

                function moveLeft(dist) {
                    if (dist == null) dist = o.advance;
                    preMove();
                    if (o.displayTime == 0) { clearInterval(clearInt); }
                    $('li:lt(' + dist + ')', obj).clone(true).insertAfter($('li:last', obj));
                    $('ul', obj).animate({ left: -(imgWidth / viewCount) * (dist + 1) - (parseInt(o.padding) * (dist + 1)) * 2 },
                    o.transitionSpeed, o.easeLeft, function () {
                        $('li:lt(' + dist + ')', obj).remove();
                        $(this).css({ 'left': -(imgWidth / viewCount) - parseInt(o.padding) * 2 });
                        postMove();
                        if (o.displayTime == 0) { moveLeft(); }
                    });
                }

                function moveRight(dist) {
                    if (dist == null) dist = o.advance;
                    preMove();
                    $('li:gt(' + (numImages - (dist + 1)) + ')', obj).clone(true).insertBefore($('li:first', obj));
                    $('ul', obj).css('left', -((imgWidth / viewCount) * (dist + 1)) - (parseInt(o.padding) * ((dist + 1) * 2))).animate(
                    { left: -(imgWidth / viewCount) - (parseInt(o.padding) * 2) }, o.transitionSpeed, o.easeRight, function () {
                        $('li:gt(' + (numImages - 1) + ')', obj).remove();
                        postMove();
                    });
                }

                if (autopilot) {
                    var clearInt = setInterval(function () { moveLeft(); }, o.displayTime + o.transitionSpeed);
                } else { status = 'pause'; $('#play_pause_btn' + randID).css({ 'background-position': '0 -11px' }); }
            });
        }
    });
})(jQuery);