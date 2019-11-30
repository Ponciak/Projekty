// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
let modalId = $('#image-gallery');


$('.dropdown').click(function () {
    $('.dropdown-menu').toggleClass('show');
});


$(document)
    .ready(function () {

        loadGallery(true, 'a.thumbnail');

        //This function disables buttons when needed
        function disableButtons(counter_max, counter_current) {
            $('#show-previous-image, #show-next-image')
                .show();
            if (counter_max === counter_current) {
                $('#show-next-image')
                    .hide();
            } else if (counter_current === 1) {
                $('#show-previous-image')
                    .hide();
            }
        }

        /**
         *
         * @param setIDs        Sets IDs when DOM is loaded. If using a PHP counter, set to false.
         * @param setClickAttr  Sets the attribute for the click handler.
         */

        function loadGallery(setIDs, setClickAttr) {
            let current_image,
                selector,
                counter = 0;

            $('#show-next-image, #show-previous-image')
                .click(function () {
                    if ($(this)
                        .attr('id') === 'show-previous-image') {
                        current_image--;
                    } else {
                        current_image++;
                    }

                    selector = $('[data-image-id="' + current_image + '"]');
                    updateGallery(selector);
                });

            function updateGallery(selector) {
                let $sel = selector;
                current_image = $sel.data('image-id');
                $('#image-gallery-title')
                    .text($sel.data('title'));
                $('#image-gallery-autor')
                    .text($sel.data('autor'));
                $('#image-gallery-status')
                    .text($sel.data('status'));
                $('#image-gallery-year')
                    .text($sel.data('year'));
                $('#image-gallery-image')
                    .attr('src', $sel.data('image'));
                disableButtons(counter, $sel.data('image-id'));
            }

            if (setIDs == true) {
                $('[data-image-id]')
                    .each(function () {
                        counter++;
                        $(this)
                            .attr('data-image-id', counter);
                    });
            }
            $(setClickAttr)
                .on('click', function () {
                    updateGallery($(this));
                });
        }
    });

// build key actions
$(document)
    .keydown(function (e) {
        switch (e.which) {
            case 37: // left
                if ((modalId.data('bs.modal') || {})._isShown && $('#show-previous-image').is(":visible")) {
                    $('#show-previous-image')
                        .click();
                }
                break;

            case 39: // right
                if ((modalId.data('bs.modal') || {})._isShown && $('#show-next-image').is(":visible")) {
                    $('#show-next-image')
                        .click();
                }
                break;

            default:
                return; // exit this handler for other keys
        }
        e.preventDefault(); // prevent the default action (scroll / move caret)
    });


    $( document ).ready( function () {
        $('.dropdown').on('click', function (e) {
            var $el = $(this);
            var $parent = $(this).offsetParent(".dropdown-menu");
            if (!$(this).next().hasClass('show')) {
                $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
            }
            var $subMenu = $(this).next(".dropdown-menu");
            $subMenu.toggleClass('show');

            $(this).parent("li").toggleClass('show');

            $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
                $('.dropdown-menu .show').removeClass("show");
            });

            if (!$parent.parent().hasClass('navbar-nav')) {
                $el.next().css({ "top": $el[0].offsetTop, "left": $parent.outerWidth() - 4 });
            }

            return false;
        });
    });

///////////////////
var i = 0;
$(document).ready(function () {
    $('#add_more').on('click', function () {
        var colorR = Math.floor((Math.random() * 256));
        var colorG = Math.floor((Math.random() * 256));
        var colorB = Math.floor((Math.random() * 256));
        i++;
        var html = '<div id="append_no_' + i + '" class="animated bounceInLeft">' +
            '<div class="input-group mt-3">' +
            '<div class="input-group-prepend">' +
            '<span class="input-group-text br-15" style="color:rgb(' + colorR + ',' + colorG + ',' + colorB + '">' +
            '<i class="fas fa-user-graduate"></i></span>' +
            '</div>' +
            '<input type="text" placeholder="Student Name"  class="form-control"/>' +
            '</div>' +
            '<div class="input-group mt-3">' +
            '<div class="input-group-prepend">' +
            '<span class="input-group-text br-15" style="color:rgb(' + colorR + ',' + colorG + ',' + colorB + '">' +
            '<i class="fas fa-phone-square"></i></span>' +
            '</div>' +
            '<input type="text" placeholder="Student Phone" class="form-control"/>' +
            '</div>' +
            '<div class="input-group mt-3">' +
            '<div class="input-group-prepend">' +
            '<span class="input-group-text br-15" style="color:rgb(' + colorR + ',' + colorG + ',' + colorB + '">' +
            '<i class="fas fa-at"></i></span>' +
            '</div>' +
            '<input type="email" placeholder="Student Email" class="form-control"/>' +
            '</div></div>';

        $('#dynamic_container').append(html);
        $('#remove_more').fadeIn(function () {
            $(this).show();
        });
    });

    $('#remove_more').on('click', function () {

        $('#append_no_' + i).removeClass('bounceInLeft').addClass('bounceOutRight')
            .fadeOut(function () {
                $(this).remove();
            });
        i--;
        if (i == 0) {
            $('#remove_more').fadeOut(function () {
                $(this).hide()
            });;
        }

    });
});