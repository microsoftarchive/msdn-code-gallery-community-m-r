import React from 'react';

import routePaths from '../../constants/routes';
import BtnNavbarToggler from '../btnNavbarToggler/BtnNavbarToggler';
import NavbarBrandCtn from '../../viewContainers/NavbarBrandCtn';
import NavbarListCtn from '../../viewContainers/NavbarListCtn';
import NavbarUser from '../../viewComponents/navbarUser/NavbarUser';

const idCategories = 'navbarShopHeader';

const ShopHeader = () => (
    <nav className="navbar navbar-expand-md navbar-dark bg-primary">
        <NavbarBrandCtn homeUrl={routePaths.home} />
        <BtnNavbarToggler dataTarget={idCategories} ariaControls="navbarShopHeader" ariaLabel="Toggle shop header"/>
        
        <div className="collapse navbar-collapse justify-content-md-between" id={idCategories}>
            <NavbarListCtn />
            <NavbarUser />
        </div>
    </nav>
);

export default  ShopHeader;