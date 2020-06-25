!(function ($) {
    'use strict'

    const setActiveClassName = (pathname) => {
        let selector = `[data-id='${pathname.replace('/', '')}']`;
        $(selector).addClass('active');
    };

    $(() => {
        setActiveClassName(location.pathname);
    })

}(window.jQuery))