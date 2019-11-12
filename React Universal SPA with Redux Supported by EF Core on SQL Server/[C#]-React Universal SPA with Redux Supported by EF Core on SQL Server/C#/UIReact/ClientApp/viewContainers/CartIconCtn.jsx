import { connect } from 'react-redux';

import CartIcon from '../viewComponents/cartIcon/CartIcon';
import { updateCart } from '../reduxStore/actions/handleCart'

const mapStateToProps = (state) => {
    return {
        cart: state.cart
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        updateCart: (cartLocal) => {
            dispatch(updateCart(cartLocal));
        }
    }
}

const CartIconCtn = connect(mapStateToProps, mapDispatchToProps)(CartIcon);

export default CartIconCtn;