import React from 'react';
import { Link } from 'react-router-dom';

import routePaths from '../../constants/routes';

const OrderLink = ({ navTo }) => (
    <li className="nav-item">
        <Link to={navTo} className="nav-link text-white text-right">
            Order
        </Link>
    </li>
);

const NavbarOrder = ({ user }) => {

    if (user && Object.keys(user).length > 0 && user.userId > 0) {
        return (
            <OrderLink navTo={routePaths.orderHistory} />
        );
    }

    return (
        <OrderLink navTo={routePaths.orderInquiry} />
    );
}

export default NavbarOrder;