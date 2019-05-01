(function () {
    'use strict';

    angular.module('app.core').config(config);
    config.$inject = ["toastrConfig", "$mdThemingProvider", "$mdIconProvider", '$$mdSvgRegistry'];

    function config(toastrConfig, $mdThemingProvider, $mdIconProvider, $$mdSvgRegistry) {


        toastrConfig.positionClass = 'toast-bottom-right';

        $mdIconProvider.icon('menu', '/Src/Html/svg/ic_menu_24px.svg', 24)
        $mdIconProvider.icon('add-note', '/Src/Html/svg/ic_note_add_black_24px.svg', 24)
        $mdIconProvider.icon('add', '/Src/Html/svg/ic_add_white_24px.svg', 24)
        $mdIconProvider.icon('search', '/Src/Html/svg/ic_search.svg', 24)
        $mdIconProvider.icon('edit', '/Src/Html/svg/ic_mode_edit_48px.svg', 24)
        $mdIconProvider.icon('show', '/Src/Html/svg/ic_tv_white_24px.svg', 24)
        $mdIconProvider.icon('date', '/Src/Html/svg/ic_date_range_48px.svg', 24)
        $mdIconProvider.icon('time', '/Src/Html/svg/ic_av_timer_48px.svg', 24)
        $mdIconProvider.icon('datetime', '/Src/Html/svg/ic_alarm_48px.svg', 24)
        $mdIconProvider.icon('text', '/Src/Html/svg/ic_text_format_48px.svg', 24)
        $mdIconProvider.icon('number', '/Src/Html/svg/ic_format_list_numbered_48px.svg', 24)
        $mdIconProvider.icon('image', '/Src/Html/svg/ic_photo_48px.svg', 24)
        $mdIconProvider.icon('color', '/Src/Html/svg/ic_color_lens_48px.svg', 24)
        $mdIconProvider.icon('html', '/Src/Html/svg/ic_insert_drive_file_48px.svg', 24)
        $mdIconProvider.icon('file', '/Src/Html/svg/ic_insert_drive_file_48px.svg', 24)
        $mdIconProvider.icon('delete', '/Src/Html/svg/ic_delete_48px.svg', 24)
        $mdIconProvider.icon('back-black', '/Src/Html/svg/ic_arrow_forward_black_24px.svg', 24)
        $mdIconProvider.icon('back-white', '/Src/Html/svg/ic_arrow_forward_white_24px.svg', 24)
        $mdIconProvider.icon('check-white', '/Src/Html/svg/ic_done_white_24px.svg', 24)
        $mdIconProvider.icon('check-black', '/Src/Html/svg/ic_done_black_24px.svg', 24)
        $mdIconProvider.icon('options', '/Src/Html/svg/ic_more_vert_48px.svg', 24)
        $mdIconProvider.icon('anchor', '/Src/Html/svg/ic_open_with_48px.svg', 24)
        // $mdIconProvider.icon('logout', '/Src/Html/svg/logout.svg',700)
        $mdIconProvider.icon('eye', '/Src/Html/svg/ic_remove_red_eye_48px.svg', 24)

        $mdIconProvider.icon('bell1', '/Src/Html/svg/001-ring.svg', 24)
        $mdIconProvider.icon('bell2', '/Src/Html/svg/002-music.svg', 80)
        $mdIconProvider.icon('bell3', '/Src/Html/svg/003-bell-1.svg', 24)
        $mdIconProvider.icon('bell4', '/Src/Html/svg/004-school-bell.svg', 24)
        $mdIconProvider.icon('bell5', '/Src/Html/svg/005-bell.svg', 24)
        $mdIconProvider.icon('group', '/Src/Html/svg/group.svg', 48)
        $mdIconProvider.icon('md-toggle-arrow', $$mdSvgRegistry.mdToggleArrow)

        $mdIconProvider.icon('user-locked', '/Src/Html/svg/user-locked.svg', 24)
        $mdIconProvider.icon('user-edit', '/Src/Html/svg/user-edit.svg', 24)
        $mdIconProvider.icon('users', '/Src/Html/svg/users.svg', "40")
        $mdIconProvider.icon('user', '/Src/Html/svg/man.svg', "40")

        //files
        $mdIconProvider.icon('.zip', '/Src/Html/svg/files/007-zip.svg', 24)
        $mdIconProvider.icon('.rar', '/Src/Html/svg/files/006-rar.svg', 24)
        $mdIconProvider.icon('.3gp', '/Src/Html/svg/files/001-3gp-file-format-variant.svg', 24)
        $mdIconProvider.icon('.avi', '/Src/Html/svg/files/004-avi.svg', 24)
        $mdIconProvider.icon('.psd', '/Src/Html/svg/files/001-psd.svg', 24)
        $mdIconProvider.icon('.m4a', '/Src/Html/svg/files/003-m4a-open-file-format.svg', 24)
        $mdIconProvider.icon('.mp3', '/Src/Html/svg/files/005-mp3.svg', 24)
        $mdIconProvider.icon('.srt', '/Src/Html/svg/files/002-srt-file-format-variant.svg', 24)
        $mdIconProvider.icon('.pdf', '/Src/Html/svg/files/010-pdf.svg', 24)
        $mdIconProvider.icon('.jpg', '/Src/Html/svg/files/008-jpg.svg', 24)
        $mdIconProvider.icon('.jpeg', '/Src/Html/svg/files/008-jpg.svg', 24)
        $mdIconProvider.icon('.docx', '/Src/Html/svg/files/005-text-lines.svg', 24)
        $mdIconProvider.icon('.file', '/Src/Html/svg/files/004-file.svg', 24)
        $mdIconProvider.icon('.mkv', '/Src/Html/svg/files/002-mkv.svg', 24)
        $mdIconProvider.icon('.mp4', '/Src/Html/svg/files/003-mp4.svg', 24)
        $mdIconProvider.icon('.png', '/Src/Html/svg/files/009-png.svg', 24)
        $mdIconProvider.icon('download', '/Src/Html/svg/files/ic_file_download_48px.svg', 24)
        $mdIconProvider.icon('select', '/Src/Html/svg/files/select.svg', 24)
        $mdIconProvider.icon('copy', '/Src/Html/svg/files/papers.svg', 24)
        $mdIconProvider.icon('close', '/Src/Html/svg/close.svg', 24)
        $mdIconProvider.icon('upload', '/Src/Html/svg/files/cloud-computing.svg', 24)
        $mdIconProvider.icon('up-btn', '/Src/Html/svg/files/ic_file_upload_48px.svg', 24)
        $mdIconProvider.icon('dropdown', '/Src/Html/svg/ic_playlist_add_check_48px.svg', 24)
        $mdIconProvider.icon('box', '/Src/Html/svg/eshop/001-box.svg', 24)
        $mdIconProvider.icon('gift-box', '/Src/Html/svg/eshop/002-gift-box.svg', 24)
        $mdIconProvider.icon('notepad', '/Src/Html/svg/eshop/002-notepad.svg', 24)
        $mdIconProvider.icon('checklist', '/Src/Html/svg/eshop/001-checklist.svg', 24)
        $mdIconProvider.icon('truck', '/Src/Html/svg/eshop/pickup-truck.svg', 24)
        $mdIconProvider.icon('cash', '/Src/Html/svg/eshop/cash.svg', 24)
        $mdIconProvider.icon('payment-status', '/Src/Html/svg/eshop/payment-status.svg', 24)
        $mdIconProvider.icon('discount', '/Src/Html/svg/eshop/sales.svg', 24)
        $mdIconProvider.icon('formSchemes', '/Src/Html/svg/forms.svg', 24)
        $mdIconProvider.icon('menu1', '/Src/Html/svg/eshop/006-menu.svg', 24)
        $mdIconProvider.icon('widget', '/Src/Html/svg/eshop/003-network.svg', 24)
        $mdIconProvider.icon('pages', '/Src/Html/svg/eshop/001-coding.svg', 24)
        $mdIconProvider.icon('users', '/Src/Html/svg/eshop/002-group.svg', 24)
        $mdIconProvider.icon('category-product', '/Src/Html/svg/eshop/004-three.svg', 24)
        $mdIconProvider.icon('entities', '/Src/Html/svg/eshop/001-browser.svg', 24)
        $mdIconProvider.icon('shop-entities', '/Src/Html/svg/eshop/002-online-shop.svg', 24)
        $mdIconProvider.icon('messages', '/Src/Html/svg/eshop/003-send-mail.svg', 24)
        $mdIconProvider.icon('setting', '/Src/Html/svg/eshop/002-cogwheel.svg', 24)
        $mdIconProvider.icon('html', '/Src/Html/svg/eshop/001-html.svg', 24)
        $mdIconProvider.icon('shaparak', '/Src/Html/svg/eshop/shaparak.svg', 24)
        $mdIconProvider.icon('shipment', '/Src/Html/svg/eshop/002-delivery-truck.svg', 24)
        $mdIconProvider.icon('gift', '/Src/Html/svg/gift.svg', 24)
        $mdIconProvider.icon('dashboard', '/Src/Html/svg/eshop/003-panel.svg', 24)
        $mdIconProvider.icon('inventory', '/Src/Html/svg/eshop/warehouse.svg', 24)
        $mdIconProvider.icon('politics', '/Src/Html/svg/003-peace.svg', 24)
        $mdIconProvider.icon('info', '/Src/Html/svg/002-information.svg', 24)
        $mdIconProvider.icon('calendar-date', '/Src/Html/svg/wall-calendar.svg', 24)
        $mdIconProvider.icon('comment', '/Src/Html/svg/chat.svg', 24)
        $mdIconProvider.icon('connection', '/Src/Html/svg/connection.svg', 24)
        $mdIconProvider.icon('money', '/Src/Html/svg/money.svg', 24)
        $mdIconProvider.icon('download-1', '/Src/Html/svg/technology.svg', 24)
        $mdIconProvider.icon('sad', '/Src/Html/svg/dashboard/001-sad.svg', 24)
        $mdIconProvider.icon('best-slling', '/Src/Html/svg/dashboard/002-coupon.svg', 24)
        $mdIconProvider.icon('statistics', '/Src/Html/svg/dashboard/006-presentation.svg', 24)
        $mdIconProvider.icon('stats', '/Src/Html/svg/dashboard/003-stats.svg', 24)
        $mdIconProvider.icon('growth', '/Src/Html/svg/dashboard/005-growth.svg', 24)
        $mdIconProvider.icon('barchart', '/Src/Html/svg/dashboard/004-bar-chart.svg', 24)
        $mdIconProvider.icon('unseen', '/Src/Html/svg/dashboard/medical.svg', 24)
        $mdIconProvider.icon('order', '/Src/Html/svg/dashboard/online-shop.svg', 24)


        $mdThemingProvider.theme('default')
            .primaryPalette('blue')
            .accentPalette('red')
            .warnPalette('red');
        $mdThemingProvider.enableBrowserColor({
            theme: 'default', // Default is 'default'
            hue: '200' // Default is '800'
        });
    }
})();