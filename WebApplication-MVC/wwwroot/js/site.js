
document.addEventListener('DOMContentLoaded', function () {
    const switchModeCheckbox = document.getElementById('switch-mode');

    const storedTheme = localStorage.getItem('theme');
    console.log('Stored Theme:', storedTheme); 

    if (storedTheme) {
        document.body.classList.toggle('dark-theme', storedTheme === 'dark');
        switchModeCheckbox.checked = storedTheme === 'dark';
    }

    switchModeCheckbox.addEventListener('change', function () {
        const isDarkMode = switchModeCheckbox.checked;
        document.body.classList.toggle('dark-theme', isDarkMode);

        localStorage.setItem('theme', isDarkMode ? 'dark' : 'light');
    });

});