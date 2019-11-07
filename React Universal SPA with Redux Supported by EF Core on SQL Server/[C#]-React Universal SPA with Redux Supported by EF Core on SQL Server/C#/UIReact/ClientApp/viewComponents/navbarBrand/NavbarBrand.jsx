import React from 'react';
import { Link } from 'react-router-dom';

const NavbarBrand = ({ homeUrl, selectCategoryId }) => (
    <Link to={homeUrl} className="navbar-brand" onClick={selectCategoryId}>
        <img src="../../logo.png" alt="Ski Shop" width="40" height="40" className="rounded" />
    </Link>
);

export default NavbarBrand;