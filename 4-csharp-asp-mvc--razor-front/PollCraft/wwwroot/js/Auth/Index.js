function simulateResponse(form) {
    const formId = form.attr('id');
    
    // Simulate different responses for testing
    const responses = {
        success: { success: true, message: 'Operation completed successfully!' },
        emailExists: { success: false, field: 'Email', message: 'This email is already registered' },
        usernameExists: { success: false, field: 'Username', message: 'This username is already taken' },
        invalidCredentials: { success: false, general: true, message: 'Invalid email or password' },
        serverError: { success: false, general: true, message: 'Server error occurred. Please try again later.' }
    };
    
    // For demo purposes, randomly select a response
    const responseKeys = Object.keys(responses);
    const randomResponse = responses[responseKeys[Math.floor(Math.random() * responseKeys.length)]];
    
    handleResponse(randomResponse, form);
}

function handleResponse(response, form) {
    if (response.success) {
        // Success - redirect or show success message
        showGeneralError('✅ ' + response.message);
        
        // Simulate redirect after 2 seconds
        setTimeout(() => {
            // window.location.href = '/Dashboard';
            console.log('Redirecting to dashboard...');
        }, 2000);
        
    } else {
        // Error handling
        if (response.general) {
            showGeneralError(response.message);
        } else if (response.field) {
            showFieldError(response.field, response.message);
        }
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
