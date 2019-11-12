import React from 'react';
import { Link } from 'react-router-dom';

import routePaths from '../../constants/routes';

class CartIcon extends React.Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const keyCartLocal = 'skiShopReactReduxEFCoreCart';

        const cartLocal = JSON.parse(localStorage.getItem(keyCartLocal));
        if (cartLocal) this.props.updateCart(cartLocal);
        
        window.addEventListener("beforeunload", () => {
            localStorage.setItem(keyCartLocal, JSON.stringify(this.props.cart));
        });
    }

    render() {
        const countStyle = {
            top: '-0.6em',
            left: '0.6em'
        };

        const circleStyle = {
            ...countStyle,
            color: 'gold'
        }

        const { cart } = this.props;

        return (
            <li className="nav-item">
                <Link to={routePaths.cart} className="nav-link text-right">
                    <span className="fa-stack">
                        <i className="fas fa-shopping-cart text-white fa-stack-2x"></i>
                        { cart.itemCount > 0 &&
                            <i className="fas fa-circle fa-stack-1x" style={circleStyle}></i>
                        }
                        { cart.itemCount > 0 &&
                            <strong className="fa-stack-1x text-dark" style={countStyle}>{cart.itemCount}</strong>
                        }
                    </span>
                </Link>
            </li>
        );
    }
} 
    
export default CartIcon;