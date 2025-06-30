// Home Page JavaScript

$(document).ready(function() {
    // FAQ Toggle functionality
    $('.faq-question').click(function() {
        const faqItem = $(this).parent();
        const faqAnswer = faqItem.find('.faq-answer');
        
        // Close other open FAQs
        $('.faq-item').not(faqItem).removeClass('active');
        $('.faq-answer').not(faqAnswer).removeClass('active');
        
        // Toggle current FAQ
        faqItem.toggleClass('active');
        faqAnswer.toggleClass('active');
    });

    // Smooth scrolling for navigation links
    $('a[href^="#"]').click(function(e) {
        e.preventDefault();
        const target = $(this.getAttribute('href'));
        if (target.length) {
            $('html, body').animate({
                scrollTop: target.offset().top - 80 // Account for sticky header
            }, 100);
        }
    });

    // Add scroll effect to feature cards
    function animateOnScroll() {
        $('.feature-card, .step').each(function() {
            const elementTop = $(this).offset().top;
            const elementVisible = 150;
            const windowBottom = $(window).scrollTop() + $(window).height();
            
            if (elementTop < windowBottom - elementVisible) {
                $(this).addClass('animate-in');
            }
        });
    }

    // Initialize cards as hidden and add CSS for animation
    $('.feature-card, .step').css({
        'opacity': '0',
        'transform': 'translateY(30px)',
        'transition': 'all 0.6s ease-out'
    });

    // Add CSS class for animated state
    $('<style>')
        .prop('type', 'text/css')
        .html('.animate-in { opacity: 1 !important; transform: translateY(0) !important; }')
        .appendTo('head');

    // Bind scroll event
    $(window).scroll(animateOnScroll);
    
    // Run once on load
    animateOnScroll();

    // Add hover effects for CTA button
    $('.cta-button').hover(
        function() {
            $(this).css('transform', 'translateY(-3px)');
        },
        function() {
            $(this).css('transform', 'translateY(0)');
        }
    );

    // Add scroll-to-top functionality
    $(window).scroll(function() {
        if ($(this).scrollTop() > 300) {
            if ($('#scrollTop').length === 0) {
                $('body').append('<button id="scrollTop" style="position: fixed; bottom: 20px; right: 20px; background: var(--primary-color); color: white; border: none; border-radius: 50%; width: 50px; height: 50px; font-size: 20px; cursor: pointer; box-shadow: 0 4px 15px rgba(0,0,0,0.2); z-index: 1000; transition: all 0.3s ease;">↑</button>');
                
                $('#scrollTop').click(function() {
                    $('html, body').animate({scrollTop: 0}, 200);
                });
            }
            $('#scrollTop').fadeIn();
        } else {
            $('#scrollTop').fadeOut();
        }
    });
});