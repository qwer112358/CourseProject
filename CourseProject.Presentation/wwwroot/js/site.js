// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener('DOMContentLoaded', function () {
    const themeToggleBtn = document.getElementById('themeToggle');
    const currentTheme = localStorage.getItem('theme') || 'light';

    // Apply saved theme
    if (currentTheme === 'dark') {
        document.body.classList.add('dark-theme');
        document.querySelector('.navbar').classList.add('dark-theme');
        document.querySelector('.footer').classList.add('dark-theme');
        themeToggleBtn.innerHTML = '<i class="fas fa-sun"></i> Light Theme';
    }

    themeToggleBtn.addEventListener('click', function () {
        const isDarkTheme = document.body.classList.toggle('dark-theme');
        document.querySelector('.navbar').classList.toggle('dark-theme');
        document.querySelector('.footer').classList.toggle('dark-theme');

        if (isDarkTheme) {
            localStorage.setItem('theme', 'dark');
            themeToggleBtn.innerHTML = '<i class="fas fa-sun"></i> Light Theme';
        } else {
            localStorage.setItem('theme', 'light');
            themeToggleBtn.innerHTML = '<i class="fas fa-moon"></i> Dark Theme';
        }
    });
});
