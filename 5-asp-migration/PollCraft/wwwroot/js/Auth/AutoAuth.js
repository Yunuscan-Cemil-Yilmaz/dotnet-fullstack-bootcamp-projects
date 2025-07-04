window.onload = function () {
    autoLogin();
}

function autoLogin() { 
    const token = localStorage.getItem("authToken") ? localStorage.getItem("authToken") : null;
    const userStr = localStorage.getItem("user") ? localStorage.getItem("user") : null;
    try{
        const userObj = JSON.parse(userStr);
        const userId = userObj.id;
        if(userId && token) window.location.href = `http://localhost:5182/application/News/${token}/${userId}`;
        else return;
    } catch {
        return;
    }
} 