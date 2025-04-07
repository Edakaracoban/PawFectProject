
(function ($) {
    "use strict";
    // init Chocolat light box
    var initChocolat = function () {
        Chocolat(document.querySelectorAll('.image-link'), {
            imageSize: 'contain',
            loop: true,
        })
    }

    var initSwiper = function () {

        var swiper = new Swiper(".main-swiper", {
            speed: 500,
            pagination: {
                el: ".swiper-pagination",
                clickable: true,
            },
        });

        var category_swiper = new Swiper(".category-carousel", {
            slidesPerView: 6,
            spaceBetween: 30,
            speed: 500,
            navigation: {
                nextEl: ".category-carousel-next",
                prevEl: ".category-carousel-prev",
            },
            breakpoints: {
                0: {
                    slidesPerView: 2,
                },
                768: {
                    slidesPerView: 3,
                },
                991: {
                    slidesPerView: 4,
                },
                1500: {
                    slidesPerView: 6,
                },
            }
        });

        var brand_swiper = new Swiper(".brand-carousel", {
            slidesPerView: 4,
            spaceBetween: 30,
            speed: 500,
            navigation: {
                nextEl: ".brand-carousel-next",
                prevEl: ".brand-carousel-prev",
            },
            breakpoints: {
                0: {
                    slidesPerView: 2,
                },
                768: {
                    slidesPerView: 2,
                },
                991: {
                    slidesPerView: 3,
                },
                1500: {
                    slidesPerView: 4,
                },
            }
        });

        var products_swiper = new Swiper(".products-carousel", {
            slidesPerView: 5,
            spaceBetween: 30,
            speed: 500,
            navigation: {
                nextEl: ".products-carousel-next",
                prevEl: ".products-carousel-prev",
            },
            breakpoints: {
                0: {
                    slidesPerView: 1,
                },
                768: {
                    slidesPerView: 3,
                },
                991: {
                    slidesPerView: 4,
                },
                1500: {
                    slidesPerView: 6,
                },
            }
        });
    }

    var initProductQty = function () {

        $('.product-qty').each(function () {

            var $el_product = $(this);
            var quantity = 0;

            $el_product.find('.quantity-right-plus').click(function (e) {
                e.preventDefault();
                var quantity = parseInt($el_product.find('#quantity').val());
                $el_product.find('#quantity').val(quantity + 1);
            });

            $el_product.find('.quantity-left-minus').click(function (e) {
                e.preventDefault();
                var quantity = parseInt($el_product.find('#quantity').val());
                if (quantity > 0) {
                    $el_product.find('#quantity').val(quantity - 1);
                }
            });

        });

    }

    // init jarallax parallax
    var initJarallax = function () {
        jarallax(document.querySelectorAll(".jarallax"));

        jarallax(document.querySelectorAll(".jarallax-keep-img"), {
            keepImg: true,
        });
    }

    // document ready
    $(document).ready(function () {

     
        initSwiper();
        initProductQty();
        initJarallax();
        initChocolat();

    }); // End of a document

})(jQuery);

    document.getElementById("search-icon").addEventListener("click", function() {
        document.getElementById("search-form").submit();
        });
    $(document).ready(function() {
        $('.dropdown-toggle').dropdown();
        });
        <script>
    window.onload = function() {
        var messageBox = document.getElementById("messageBox");
        if (messageBox) {
            if (window.innerWidth <= 576) {
                // Mobil ekranlarda daha geniş kutu
                messageBox.style.width = "90%";
                messageBox.style.maxWidth = "100%";
            } else {
                // Masaüstü ekranlarda normal genişlik
                messageBox.style.width = "80%";
                messageBox.style.maxWidth = "400px";
            }
        }
    }

    // Ekran boyutu değişirse CSS güncellenir
    window.onresize = function() {
        var messageBox = document.getElementById("messageBox");
        if (messageBox) {
            if (window.innerWidth <= 576) {
                messageBox.style.width = "90%";
                messageBox.style.maxWidth = "100%";
            } else {
                messageBox.style.width = "80%";
                messageBox.style.maxWidth = "400px";
            }
        }
    }
</script>

