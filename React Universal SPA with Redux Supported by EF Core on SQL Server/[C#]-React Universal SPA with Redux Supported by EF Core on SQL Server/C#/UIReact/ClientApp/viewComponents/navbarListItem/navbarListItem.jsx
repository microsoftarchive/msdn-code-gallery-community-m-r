import React from 'react';
import { Link } from 'react-router-dom';
import { stringify } from 'qs';

import routePaths from '../../constants/routes';
import defaultReduxValues from '../../constants/defaultReduxValues';

import './navbarListItem.scss';

const NavbarListItem = ({ item, selectedId }) => {
    const classForLink =
        `nav-link btn btn-primary btn-sm text-white text-left px-1 ${item.categoryId === selectedId ? 'bg-info' : ''}`;

    const name = item.categoryName;
    const id = item.categoryId;
    
    const navTo = {
        pathname: `${routePaths.stylesByCategory}/${name}`,
        search: stringify({
            categoryId: id,
            pageNumber: 1,
            pageSize: defaultReduxValues.pageSize
        })
    };

    return (
        <li className="nav-item" >
            <Link to={navTo} className={classForLink} >
                {name}
            </Link>
        </li>
    );
};

export default NavbarListItem;
