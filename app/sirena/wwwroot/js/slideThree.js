const displacementSlider = function(opts) {

    let vertex = `
        varying vec2 vUv;
        void main() {
          vUv = uv;
          gl_Position = projectionMatrix * modelViewMatrix * vec4( position, 1.0 );
        }
    `;

    let fragment = `
        varying vec2 vUv;

        uniform sampler2D currentImage;
        uniform sampler2D nextImage;
        uniform sampler2D disp;
		
		// uniform float time;
        // uniform float _rot;
        uniform float dispFactor;
        uniform float effectFactor;
        
        // vec2 rotate(vec2 v, float a) {
        //  float s = sin(a);
        //  float c = cos(a);
        //  mat2 m = mat2(c, -s, s, c);
        //  return m * v;
        // }

        void main() {

            vec2 uv = vUv;
            // uv -= 0.5;
            // vec2 rotUV = rotate(uv, _rot);
            // uv += 0.5;
            
            vec4 disp = texture2D(disp, uv);
            
            vec4 _currentImage;
            vec4 _nextImage;
            float intensity = -0.35;

            vec4 orig1 = texture2D(currentImage, uv);
            vec4 orig2 = texture2D(nextImage, uv);
            
            _currentImage = texture2D(currentImage, vec2(uv.x, uv.y + dispFactor * (disp.r * effectFactor)));
            _nextImage = texture2D(nextImage, vec2(uv.x, uv.y + (1.0 - dispFactor) * (disp.r * effectFactor)));

            vec4 finalTexture = mix(_currentImage, _nextImage, dispFactor);

            gl_FragColor = finalTexture;
			// gl_FragColor = disp;
        }
    `;
    
    let parent = opts.parent;
	let dispImage = opts.displacementImage || console.warn("displacement image missing");
	let intensity = opts.intensity || 0.15;
	let speed = opts.speed || 4.4;
    let images = opts.images, image, sliderImages = [];

	function calcCanvasProporsions() {
        if (2800 / 1900 > parent.offsetWidth / parent.offsetHeight) {
            return {
                proporsion: 'height',
                coef: 2800 / 1900
            };
        }
        return {
            proporsion: 'width',
            coef: 1900 / 2800
        };
    }

	let scene = new THREE.Scene();
    let camera = new THREE.OrthographicCamera(
        parent.offsetWidth / -2,
        parent.offsetWidth / 2,
        parent.offsetHeight / 2,
        parent.offsetHeight / -2,
        1,
        1000
    );

    camera.position.z = 1;				    

    let renderer = new THREE.WebGLRenderer({
        antialias: false,
    });

    renderer.setPixelRatio( window.devicePixelRatio );
    renderer.setClearColor( 0x23272A, 1.0 );
    if (calcCanvasProporsions().proporsion === 'width') {
        renderer.setSize(parent.offsetWidth, parent.offsetWidth * calcCanvasProporsions().coef);
    } else {
        renderer.setSize(parent.offsetHeight * calcCanvasProporsions().coef, parent.offsetHeight);
    }
    parent.appendChild( renderer.domElement );

    let loader = new THREE.TextureLoader();
    loader.crossOrigin = "";
 
    images.forEach( ( img ) => {

        image = loader.load( img.getAttribute( 'src' ) + '?v=' + Date.now() );
        image.magFilter = image.minFilter = THREE.LinearFilter;
        image.anisotropy = renderer.getMaxAnisotropy();
        sliderImages.push( image );

    });
    
	let disp = loader.load(dispImage);
	disp.wrapS = disp.wrapT = THREE.RepeatWrapping;

    

    let mat = new THREE.ShaderMaterial({
        uniforms: {
	        effectFactor: { type: "f", value: intensity },
            dispFactor: { type: "f", value: 0.0 },
            currentImage: { type: "t", value: sliderImages[0] },
            nextImage: { type: "t", value: sliderImages[1] },
            disp: { type: "t", value: disp }
        },
        vertexShader: vertex,
        fragmentShader: fragment,
        transparent: true,
        opacity: 1.0
    });

    let geometry = new THREE.PlaneBufferGeometry(
        parent.offsetWidth,
        parent.offsetHeight,
        1
    );
    let object = new THREE.Mesh(geometry, mat);
//    object.position.set(0, 0, 0);
    scene.add(object);

    let addEvents = function(){

        let pagButtons = Array.from(document.getElementById('pagination').querySelectorAll('button'));
        let isAnimating = false;

        pagButtons.forEach( (el) => {

            el.addEventListener('click', function() {

                if( !isAnimating ) {

                    isAnimating = true;

                    document.getElementById('pagination').querySelectorAll('.active')[0].className = '';
                    this.className = 'active';

                    let slideId = parseInt( this.dataset.slide, 10 );

                    mat.uniforms.nextImage.value = sliderImages[slideId];
                    mat.uniforms.nextImage.needsUpdate = true;

                    TweenLite.to( mat.uniforms.dispFactor, speed, {
                        value: 1,
                        ease: 'Expo.easeInOut',
                        onComplete: function () {
                            mat.uniforms.currentImage.value = sliderImages[slideId];
                            mat.uniforms.currentImage.needsUpdate = true;
                            mat.uniforms.dispFactor.value = 0.0;
                            isAnimating = false;
                        }
                    });

                    

                }
            });
        });
    };
    
    addEvents();
  
    
    //here
    setInterval(function () {
	    if($('#pagination button').last().hasClass('active')){
		    $('#pagination button').first().click();
	    } else {
		    $('#pagination .active').next().click();
	    };
    }, 6400);

    window.addEventListener("resize", function(e) {
        if (calcCanvasProporsions().proporsion === 'width') {
            renderer.setSize(parent.offsetWidth, parent.offsetWidth * calcCanvasProporsions().coef);
        } else {
            renderer.setSize(parent.offsetHeight * calcCanvasProporsions().coef, parent.offsetHeight);
        }
        
        
    });

    let animate = function() {
        requestAnimationFrame(animate);

        renderer.render(scene, camera);
    };
    animate();
};

imagesLoaded( document.querySelectorAll('img'), () => {

    document.body.classList.remove('loading');

    const el = document.getElementById('slider');
    const imgs = Array.from(el.querySelectorAll('img'));
    
    setTimeout(function () {
	    new displacementSlider({
	        parent: el,
	        images: imgs,
	        displacementImage: el.dataset.displacement
	    });
    }, 1600);

});


		







