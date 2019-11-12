function logout() {
    window.location = '/Login.aspx';
}

function Menu() {
    var menu;
    $.ajax({
        type: "POST",
        url: "Default.aspx/CargarMenu",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var lsMenu = msg.d;
            var _menu = lsMenu;
            var _hijo;
            var div = document.getElementById('nav');
            var listItems = [];
            var str = '';
            var str2 = '';
            if (lsMenu.length > 0) {
                for (var key in lsMenu) {
                    if (lsMenu[key].intIdMenu == lsMenu[key].intIdPadre) {
                        _hijo = SubMenuVertical(_menu, lsMenu[key].intIdMenu);
                        str = '<li class="top"><a href="' + lsMenu[key].strUrl + '" onclick="event.preventDefault();" class="top_link">';
                        if (_hijo) { str = str + '<span class="down">'; } else { str = str + '<span>'; }
                        str = str + lsMenu[key].strDescripcion + '</span></a>';
                        //CargarMenuVertical
                        str = str + MenuVertical(_menu, lsMenu[key].intIdMenu, _hijo);

                        str = str + '</li>';
                        listItems.push(str);
                    }
                }
                div.innerHTML = div.innerHTML + listItems.join('');
            }
            else {
                alert("No records found");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert(textStatus);
        }
    });
}
function MenuVertical(_menu, ID, MenVertical) {
    var _str = '';
    if (MenVertical) { _str = _str + '<ul class="sub">'; } else { _str = _str + '<ul>'; }
    for (var key in _menu) {
        _MenuHo = false;
        if (_menu[key].intIdMenu != _menu[key].intIdPadre & _menu[key].intIdPadre == ID) {
            _str = _str + '<li><a href="' + _menu[key].strUrl + '" target="content" onclick="event.preventDefault();" ';
            if (SiMenuHorizotal(_menu, _menu[key].intIdMenu)) {
                _str = _str + 'class="fly"';
            }
            _str = _str + '>' + _menu[key].strDescripcion + '</a>';
            //MenuHorizontal
            _str = _str + MenuHorizontal(_menu, _menu[key].intIdMenu);

            _str = _str + '</li>';
        }
    }
    _str = _str + '</ul>';
    return _str;
}
function SiMenuHorizotal(_menu, ID_) {
    var sw = false;
    for (var key in _menu) {
        if (_menu[key].intIdMenu != _menu[key].intIdPadre & _menu[key].intIdPadre == ID_ & sw == false) {
            sw = true;
        }
    }
    return sw;
}
function MenuHorizontal(_menu, ID) {
    var str_ = '';
    var sw = false;
    for (var key in _menu) {
        if (_menu[key].intIdMenu != _menu[key].intIdPadre & _menu[key].intIdPadre == ID) {
            if (sw == false) { str_ = str_ + '<ul>'; sw = true; }
            str_ = str_ + '<li><a href="' + _menu[key].strUrl + '" target="content" onclick="event.preventDefault();" ';
            if (SiMenuHorizotal(_menu, _menu[key].intIdMenu)) {
                str_ = str_ + 'class="fly"';
            }
            str_ = str_ + '>' + _menu[key].strDescripcion + '</a>';
            //MenuHorizontal
            str_ = str_ + MenuHorizontal(_menu, _menu[key].intIdMenu);

            str_ = str_ + '</li>';
        }
    }
    if (sw) { str_ = str_ + '</ul>'; sw = false; }
    return str_;
}

function SubMenuVertical(_menu, ID) {
    var sw = false;
    var str = '';
    for (var key in _menu) {
        if (_menu[key].intIdMenu == _menu[key].intIdPadre & sw == false) {
            for (var _key in _menu) {
                if (_menu[_key].intIdMenu != _menu[_key].intIdPadre & _menu[_key].intIdPadre == ID & sw == false) {
                    sw = true;
                }

            }
        }
    }
    return sw;
}