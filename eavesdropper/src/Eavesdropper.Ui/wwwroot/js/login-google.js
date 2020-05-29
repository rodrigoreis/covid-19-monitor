!(function ($, $g) {

    'use strict';
    
    window.gobject = {

        'init': function () {
            $g.signin2.render('g-login-btn', {
                'scope': 'email profile https://www.googleapis.com/auth/drive.readonly',
                'width': 228,
                'height': 40,
                'longtitle': true,
                'theme': 'light'
            });
        }

    };
    
    $(function () {
        window.gobject.init();
    });

})(window.jQuery, window.gapi);