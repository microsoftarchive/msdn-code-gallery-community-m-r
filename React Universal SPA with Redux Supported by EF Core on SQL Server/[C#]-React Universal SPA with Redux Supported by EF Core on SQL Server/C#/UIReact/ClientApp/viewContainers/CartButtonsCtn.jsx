import { connect } from 'react-redux';

import { clearCart } from '../reduxStore/actions/handleCart';
import CartButtons from '../viewComponents/cartButtons/CartButtons';

const mapDispatchToProps = (dispatch) => {
    return {
        clearCart: () => {

            dispatch(clearCart());
        }
    };
};

const CartButtonsCtn = connect(null, mapDispatchToProps)(CartButtons);

export default CartButtonsCtn;