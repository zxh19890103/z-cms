/// <reference path="jquery-1.7.2.min.js" />
$(function () {
    var nt = nt || {};
    nt.durationUnit = 20;
    nt.lastSelectedMenuItem = null;
    nt.activeOuterA = null;
    nt.activeInnerA = null;
    $('li.menu-outer-nav>a').click(function () {
        if (nt.activeOuterA)
            $(nt.activeOuterA).removeClass('menu-outer-active');
        nt.activeOuterA = this;
        $(this).addClass('menu-outer-active');
        var thisUl = $(this).siblings('ul').get(0);
        var numOfChidren = thisUl.childNodes.length;
        if (nt.lastSelectedMenuItem) {
            if (nt.lastSelectedMenuItem == thisUl) {
                $(thisUl).slideUp(50);
                nt.lastSelectedMenuItem = null;
            } else {
                $(nt.lastSelectedMenuItem).hide();
                $(thisUl).slideDown(nt.durationUnit * numOfChidren);
                nt.lastSelectedMenuItem = thisUl;
            }
        } else {
            $(thisUl).slideDown(nt.durationUnit * numOfChidren);
            nt.lastSelectedMenuItem = thisUl;
        }
    })

    $('li.menu-inner-nav>a').click(function () {
        if (nt.activeInnerA)
            $(nt.activeInnerA).removeClass('menu-inner-active');
        nt.activeInnerA = this;
        $(this).addClass('menu-inner-active');
    })

})