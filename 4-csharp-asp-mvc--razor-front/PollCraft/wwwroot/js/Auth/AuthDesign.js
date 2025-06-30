// Auth Page JavaScript
$(document).ready(function() {
    
    // Form toggle functionality
    $('.toggle-btn').click(function() {
        const targetForm = $(this).data('form');
        
        // Update toggle buttons
        $('.toggle-btn').removeClass('active');
        $(this).addClass('active');
        
        // Move slider
        $('.toggle-slider').toggleClass('register', targetForm === 'register');
        
        // Show/hide forms with animation
        $('.form-container').removeClass('active');
        setTimeout(() => {
            $(`#${targetForm}Form`).addClass('active');
        }, 150);
        
        // Clear all errors when switching
        clearAllErrors();
    });

    // Password toggle functionality
    $('.password-toggle').click(function() {
        const targetId = $(this).data('target');
        const passwordInput = $(`#${targetId}`);
        const toggleIcon = $(this).find('.toggle-icon');
        
        if (passwordInput.attr('type') === 'password') {
            passwordInput.attr('type', 'text');
            toggleIcon.text('🙈');
        } else {
            passwordInput.attr('type', 'password');
            toggleIcon.text('👁️');
        }
    });

    // Real-time validation
    setupRealTimeValidation();
    
    // Form submission with loading
    $('.auth-form').submit(function(e) {
        e.preventDefault();
        
        const form = $(this);
        const submitBtn = form.find('.submit-btn');
        const btnText = submitBtn.find('.btn-text');
        const btnLoader = submitBtn.find('.btn-loader');
        
        // Clear previous errors
        clearAllErrors();
        
        // Validate form
        const isValid = validateForm(form);
        
        if (isValid) {
            // Show loading state
            submitBtn.prop('disabled', true);
            btnText.css('opacity', '0');
            btnLoader.show();
            
            // Simulate API call (replace with actual form submission)
            setTimeout(() => {
                // Hide loading state
                submitBtn.prop('disabled', false);
                btnText.css('opacity', '1');
                btnLoader.hide();
                
                // Simulate response (replace with actual response handling)
                simulateResponse(form);
            }, 2000);
        }
    });

    // Input focus effects
    $('.form-input').focus(function() {
        $(this).closest('.input-wrapper').addClass('focused');
    }).blur(function() {
        $(this).closest('.input-wrapper').removeClass('focused');
    });

    // Clear errors on input
    $('.form-input').on('input', function() {
        const fieldName = $(this).attr('name');
        hideFieldError(fieldName);
        hideGeneralError();
    });
});

// Validation functions
function setupRealTimeValidation() {
    // Email validation
    $('input[type="email"]').on('blur', function() {
        const email = $(this).val();
        const fieldName = $(this).attr('name');
        
        if (email && !isValidEmail(email)) {
            showFieldError(fieldName, 'Please enter a valid email address');
        }
    });

    // Password confirmation
    $('#registerConfirmPassword').on('blur', function() {
        const password = $('#registerPassword').val();
        const confirmPassword = $(this).val();
        
        if (confirmPassword && password !== confirmPassword) {
            showFieldError('ConfirmPassword', 'Passwords do not match');
        }
    });

    // Username validation
    $('#registerUsername').on('blur', function() {
        const username = $(this).val();
        
        if (username && username.length < 3) {
            showFieldError('Username', 'Username must be at least 3 characters long');
        } else if (username && !/^[a-zA-Z0-9_]+$/.test(username)) {
            showFieldError('Username', 'Username can only contain letters, numbers, and underscores');
        }
    });

    // Password strength
    $('#registerPassword').on('input', function() {
        const password = $(this).val();
        
        if (password && password.length < 6) {
            showFieldError('Password', 'Password must be at least 6 characters long');
        } else if (password && !hasStrongPassword(password)) {
            showFieldError('Password', 'Password should contain at least one uppercase letter, one lowercase letter, and one number');
        }
    });
}

function validateForm(form) {
    let isValid = true;
    const formId = form.attr('id');
    
    // Get all required inputs
    const requiredInputs = form.find('input[required]');
    
    requiredInputs.each(function() {
        const input = $(this);
        const value = input.val().trim();
        const fieldName = input.attr('name');
        
        if (!value) {
            showFieldError(fieldName, 'This field is required');
            isValid = false;
        }
    });

    // Form-specific validations
    if (formId === 'loginForm') {
        isValid = validateLoginForm(form) && isValid;
    } else if (formId === 'registerForm') {
        isValid = validateRegisterForm(form) && isValid;
    }
    
    return isValid;
}

function validateLoginForm(form) {
    let isValid = true;
    
    const email = form.find('#loginEmail').val();
    const password = form.find('#loginPassword').val();
    
    if (email && !isValidEmail(email)) {
        showFieldError('Email', 'Please enter a valid email address');
        isValid = false;
    }
    
    if (password && password.length < 6) {
        showFieldError('Password', 'Password must be at least 6 characters long');
        isValid = false;
    }
    
    return isValid;
}

function validateRegisterForm(form) {
    let isValid = true;
    
    const firstName = form.find('#registerName').val();
    const lastName = form.find('#registerSurname').val();
    const username = form.find('#registerUsername').val();
    const email = form.find('#registerEmail').val();
    const password = form.find('#registerPassword').val();
    const confirmPassword = form.find('#registerConfirmPassword').val();
    
    // Name validations
    if (firstName && firstName.length < 2) {
        showFieldError('FirstName', 'First name must be at least 2 characters long');
        isValid = false;
    }
    
    if (lastName && lastName.length < 2) {
        showFieldError('LastName', 'Last name must be at least 2 characters long');
        isValid = false;
    }
    
    // Username validation
    if (username && username.length < 3) {
        showFieldError('Username', 'Username must be at least 3 characters long');
        isValid = false;
    } else if (username && !/^[a-zA-Z0-9_]+$/.test(username)) {
        showFieldError('Username', 'Username can only contain letters, numbers, and underscores');
        isValid = false;
    }
    
    // Email validation
    if (email && !isValidEmail(email)) {
        showFieldError('Email', 'Please enter a valid email address');
        isValid = false;
    }
    
    // Password validation
    if (password && password.length < 6) {
        showFieldError('Password', 'Password must be at least 6 characters long');
        isValid = false;
    }
    
    // Password confirmation
    if (password && confirmPassword && password !== confirmPassword) {
        showFieldError('ConfirmPassword', 'Passwords do not match');
        isValid = false;
    }
    
    return isValid;
}

// Helper functions
function isValidEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}

function hasStrongPassword(password) {
    const hasUpper = /[A-Z]/.test(password);
    const hasLower = /[a-z]/.test(password);
    const hasNumber = /\d/.test(password);
    return hasUpper && hasLower && hasNumber;
}

function showFieldError(fieldName, message) {
    const errorId = getErrorId(fieldName);
    const errorElement = $(`#${errorId}`);
    
    if (errorElement.length) {
        errorElement.find('.error-message').text(message);
        errorElement.show();
        
        // Add error class to input wrapper
        const input = $(`input[name="${fieldName}"]`);
        input.closest('.input-wrapper').addClass('error');
        
        // Animate error message
        errorElement.hide().fadeIn(300);
    }
}

function hideFieldError(fieldName) {
    const errorId = getErrorId(fieldName);
    const errorElement = $(`#${errorId}`);
    
    if (errorElement.length) {
        errorElement.fadeOut(200);
        
        // Remove error class from input wrapper
        const input = $(`input[name="${fieldName}"]`);
        input.closest('.input-wrapper').removeClass('error');
    }
}

function showGeneralError(message) {
    const generalError = $('#generalError');
    const generalErrorMessage = $('#generalErrorMessage');
    
    generalErrorMessage.text(message);
    generalError.hide().fadeIn(300);
}

function hideGeneralError() {
    const generalError = $('#generalError');
    generalError.fadeOut(200);
}

function clearAllErrors() {
    // Hide all field errors
    $('.field-error').fadeOut(200);
    
    // Hide general error
    hideGeneralError();
    
    // Remove error classes from input wrappers
    $('.input-wrapper').removeClass('error');
}

function getErrorId(fieldName) {
    const currentForm = $('.form-container.active').attr('id');
    const prefix = currentForm === 'loginForm' ? 'login' : 'register';
    
    // Handle special cases
    const fieldMap = {
        'Email': 'Email',
        'Password': 'Password',
        'FirstName': 'Name',
        'LastName': 'Surname',
        'Username': 'Username',
        'ConfirmPassword': 'ConfirmPassword'
    };
    
    const mappedField = fieldMap[fieldName] || fieldName;
    return `${prefix}${mappedField}Error`;
}

// Animation and visual effects
function initFloatingShapes() {
    $('.floating-shape').each(function(index) {
        const shape = $(this);
        const delay = index * 0.5;
        const duration = 3 + Math.random() * 2;
        
        shape.css({
            'animation-delay': `${delay}s`,
            'animation-duration': `${duration}s`
        });
    });
}

// Initialize floating shapes animation
$(document).ready(function() {
    initFloatingShapes();
});

// Add some nice particle effect (optional)
function createParticle() {
    const particle = $('<div class="particle"></div>');
    const startX = Math.random() * window.innerWidth;
    const endX = startX + (Math.random() - 0.5) * 200;
    const duration = 3000 + Math.random() * 2000;
    
    particle.css({
        left: startX + 'px',
        bottom: '-10px',
        position: 'fixed',
        width: '4px',
        height: '4px',
        backgroundColor: 'rgba(255, 255, 255, 0.3)',
        borderRadius: '50%',
        pointerEvents: 'none',
        zIndex: 1
    });
    
    $('body').append(particle);
    
    particle.animate({
        left: endX + 'px',
        bottom: window.innerHeight + 'px',
        opacity: 0
    }, duration, 'linear', function() {
        particle.remove();
    });
}

// Create particles periodically (optional - can be disabled)
setInterval(createParticle, 2000);