var animation = bodymovin.loadAnimation({
  container: document.getElementById('bm-service'),
  renderer: 'svg',
  loop: true,
  autoplay: true,
  path: '../bm/service/data.json'
})

var animation2 = bodymovin.loadAnimation({
  container: document.getElementById('bm-barbers'),
  renderer: 'svg',
  loop: true,
  autoplay: true,
  path: '../bm/barbers/data.json'
})

function getCoords(elem) {
  var box = elem.getBoundingClientRect();

  return {
    top: box.top + pageYOffset,
    left: box.left + pageXOffset
  };
}

function inViewport(elem, callback) {
  if (pageYOffset + (window.innerHeight * .8) > getCoords(elem).top) {
    elem.classList.add('visible');

    if (callback) callback;
  }
}

function preloader() {
  var images = document.querySelectorAll('img');
  var loaded = 0;
  var allImages = images.length;
  var percent = 0;
  var preloader = document.getElementById('preloader');
  var line = document.getElementById('preloader-line');
  var firstBlock = document.getElementById('welcome-block');
  var headerBlock = document.getElementById('header-block');
  var bmLogo = document.getElementById('bmLogo');

  function calcImageLoad() {
    loaded++;
    percent = Math.ceil(100 / allImages * loaded);
    line.style.width = percent + '%';
    if (loaded === allImages) {
      setTimeout(function () {
        preloader.classList.add('hidden');
        firstBlock.classList.add('visible');
        headerBlock.classList.add('visible');
        bmLogo.classList.add('visible');
        setTimeout(function () {
          preloader.classList.add('disable');
          bmLogo.remove();
        }, 1400);
      }, 1600);
    }
  }

  for (var i = 0; i < images.length; i++) {
    var img = new Image();
    img.src = images[i].src;

    img.onload = function () {
      calcImageLoad();
    }
    img.onerror = function () {
      calcImageLoad();
    }
  }
}

function scrollToAnchor() {
  $('a[href*="#"]').click(function (e) {
    e.preventDefault();
    var id = $(this).attr('href');
    if ($(id).length) {
      $('html, body').animate({
        scrollTop: $(id).offset().top
      }, 600);
    }
  })
}

/**
 * @param {string} className - class name of node collection
 * @param {number} coef - animation transform coeficient from 0 to 1
 */
function parallax(className, coef) {
  var elems = document.querySelectorAll(className);
  var wHeight = window.innerHeight;
  for (var i = 0; i < elems.length; i++) {
    var el = elems[i];
    var elPos = getCoords(el).top;
    if (elPos > pageYOffset && elPos < pageYOffset + wHeight) {
      var translate = (elPos - pageYOffset) * coef;
      TweenLite.to(el, 0.6, {
        transform: "translateY(" + translate + "px)"
      });
    }
  }
}

function popup() {
  
  (function open() {
    
    var elems = document.querySelectorAll('[data-popup-open]');
    
    for (var i = 0; i < elems.length; i++) {
      var el = elems[i];
      var id = el.dataset.popupOpen;
      el.addEventListener('click', function() {
        document.getElementById(id).classList.add('active');
      })
    }
  })();

  (function close() {
    var elems = document.querySelectorAll('[data-popup-close]');
    
    for (var i = 0; i < elems.length; i++) {
      var el = elems[i];
      var id = el.dataset.popupClose;
      el.addEventListener('click', function() {
        document.getElementById(id).classList.add('hide-active');
        setTimeout(function() {
          document.getElementById(id).classList.remove('active');
          document.getElementById(id).classList.remove('hide-active');
        }, 600);
      })
    }
  })();
}

preloader();

window.addEventListener('scroll', function (e) {
  inViewport(document.getElementById('services-block'));
  inViewport(document.getElementById('barbers-block'));
  inViewport(document.getElementById('products-block'));

  parallax('.ident-1', 0.1);
  parallax('.ident-2', 0.1);

  parallax('.ident-3', 0.12);
  parallax('.ident-4', 0.06);

  parallax('.insta-list', 0.05);
})

window.addEventListener('DOMContentLoaded', function () {
  scrollToAnchor();

  popup();
})