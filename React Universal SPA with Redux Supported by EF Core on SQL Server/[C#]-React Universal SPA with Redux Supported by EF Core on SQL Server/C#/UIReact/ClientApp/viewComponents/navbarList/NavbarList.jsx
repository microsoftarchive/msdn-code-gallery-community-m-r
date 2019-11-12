import React from 'react';

import NavbarListItem from '../navbarListItem/NavbarListItem';

const NavbarList = ({ items, selectedId}) => (
    <ul className="navbar-nav">
        {items.map(item => (
            <NavbarListItem key={item.categoryId} item={item} selectedId={selectedId} />
        ))}
    </ul>
);

export default NavbarList;