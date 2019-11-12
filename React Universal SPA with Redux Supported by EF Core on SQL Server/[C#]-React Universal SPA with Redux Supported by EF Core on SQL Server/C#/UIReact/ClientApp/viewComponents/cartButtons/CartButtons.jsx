import React from 'react';
import { Link } from 'react-router-dom';

import routePaths from '../../constants/routes';

const generalBtn = 'btn btn-sm';
const linkBtn = `${generalBtn} btn-primary mr-3`;
const clearBtn = `${generalBtn} btn-outline-primary align-content-end`;

const CartButtons = ({ clearCart }) => (
    <div className="text-center" >
        <Link to={routePaths.home} className={linkBtn}>
            Continue Shopping
        </Link>
        <Link to={routePaths.checkout} className={linkBtn} >
            Checkout
        </Link>
        <button className={clearBtn} onClick={clearCart}>Clear Cart</button>
    </div>
);

export default CartButtons;