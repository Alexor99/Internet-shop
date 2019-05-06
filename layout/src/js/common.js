'use strict';

import '../scss/common.scss';
import slick from 'slick-carousel';
$("document").ready(function($){

    let checkbox_list = $('.checkbox-container');

    $(window).on('load', function () {
        $('.loader').fadeOut()
    });

    $(window).scroll(function () {
        if ($(this).scrollTop() > 170 && $(this).width() > 470) {
            checkbox_list.addClass("fixed-checkbox");
        } else {
            checkbox_list.removeClass("fixed-checkbox");
        }
    });

    $('.slider-wr').slick({
        autoplay: true,
        autoplaySpeed: 3000,
        arrows: false,
        dots: true,
        dotsClass: 'slick-dots carousel-dots',
        fade: true,
        speed: 900,
        infinite: true,
        cssEase: 'cubic-bezier(0.7, 0, 0.3, 1)',
        touchThreshold: 100
    });

    $('.slider-for').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: true,
        fade: true,
        asNavFor: '.slider-nav'
    });

    $('.slider-nav').slick({
        slidesToShow: 6,
        slidesToScroll: 3,
        asNavFor: '.slider-for',
        // dots: true,
        arrows: false,
        centerMode: true,
        focusOnSelect: true,
        vertical: true,
        responsive: [
           {
             breakpoint: 900,
             settings: {
               vertical: false
             }
           }
        ]

    });

    $('.closebtn').click(function () {
        $('.cart_popup').css('visibility', 'hidden');
    });

    $('.closebtn').hover(function () {
        $('.cart_popup').css('visibility', 'visible');
    });

    $(".product a").mouseenter(
        function(){
            $(this).find(".first_img").animate({marginLeft:'5px'},'fast');
        });
    $(".product a").mouseleave(
        function() {
            $(this).find(".first_img").animate({marginLeft:'0px'},'fast');
        });

    $(".slider-for").bind('mouseover', function () {
        $(".slick-prev, .slick-next").css("opacity", "0.4");
    });

    $(".slider-for").bind('mouseleave', function () {
        $(".slick-prev, .slick-next").css("opacity", "0");
    });

    var allProducts = showedProduct();
    $('.countShowedProducts').text(allProducts);
    $('.countAllProducts').text(allProducts);
});


/****************************TESTS***************************/

 var arrNotChecked = ['f-1', 'f-2', 'f-3', 'f-4', 'f-5',
                     'f-6', 'f-7', 'f-8', 'f-9', 'f-10'];
 var arrChecked = [];
 
$(".boxes").click(function(event){
    var self = event.target;

    if(self.checked){
        arrChecked.push(self.className);
        arrNotChecked.splice(arrNotChecked.indexOf(self.className), 1);

        $.each(arrNotChecked, function(index, value){
            $('.catalog' + ' .' + value).fadeOut(500);
        });
               
        $.each(arrChecked, function(index, value){
            $('.catalog' + ' .' + value).fadeIn(500);
        });

        setTimeout(showedProduct, 1000);
    }
    else if(self.checked == false){
        arrChecked.splice(arrChecked.indexOf(self.className), 1);
        arrNotChecked.push(self.className);

        $.each(arrNotChecked, function(index, value){
            $('.catalog' + ' .' + value).fadeOut(500);
        });

        if(arrChecked == ''){
            $.each(arrNotChecked, function(index, value){
                $('.catalog' + ' .' + value).fadeIn(500);
            });
        }        

        setTimeout(showedProduct, 1000);
    }    
});

    
function showedProduct (){
    var i = 0, j = 0;
    $('.catalog .product').each(function(index, value){
        j++;
        if($(this).css('display') !== 'none')
            i++;        
    });
    $('.countShowedProducts').text(i);
    return i;      
 }


 