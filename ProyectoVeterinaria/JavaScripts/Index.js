const btn = document.querySelector('#menu-toggle');
const menu = document.querySelector('#menu');

window.addEventListener('resize', function () {
    const width = window.innerWidth;

    if (width >= 1000) {
        menu.classList.add('show');
    } else {
        menu.classList.remove('show');
    }
})

btn.addEventListener('click', function () {
    menu.classList.toggle('show');
})