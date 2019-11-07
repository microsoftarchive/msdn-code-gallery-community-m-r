import React from 'react';

import NavbarOrderCtn from '../../viewContainers/NavbarOrderCtn';
import LoginOutCtn from '../../viewContainers/LoginOutCtn';
import CartIconCtn from '../../viewContainers/CartIconCtn';

const NavbarUser = () => (
    <ul className="navbar-nav">
        <NavbarOrderCtn />
        <LoginOutCtn />
        <CartIconCtn />
    </ul>
);

export default NavbarUser;