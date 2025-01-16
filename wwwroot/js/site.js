// Start-Code For Active-On-Click In Nav-bar
const currentLocation = location.href;
const menuItems = document.querySelectorAll('header .navbar .navbar-nav a');
const menuItemsLength = menuItems.length;
for (let i = 0; i < menuItemsLength; i++) {
    if (menuItems[i].href == currentLocation)
        menuItems[i].className = "active";
}
// End-Code For Active-On-Click In Nav-bar

// Start-Code For Spinner 
document.addEventListener('DOMContentLoaded', function () {
    const spinnerOverlay = document.getElementById('spinner-overlay');

    function showSpinner() {
        spinnerOverlay.classList.add('visible');
    }

    function hideSpinner() {
        spinnerOverlay.classList.remove('visible');
    }

    document.querySelectorAll('a').forEach(link => {
        link.addEventListener('click', function (e) {
            if (!link.href.startsWith('#') && link.hostname === location.hostname) {
                e.preventDefault();
                showSpinner();

                setTimeout(() => {
                    window.location.href = link.href;
                }, 700);
            }
        });
    });

    hideSpinner();
});
// End-Code For Spinner




