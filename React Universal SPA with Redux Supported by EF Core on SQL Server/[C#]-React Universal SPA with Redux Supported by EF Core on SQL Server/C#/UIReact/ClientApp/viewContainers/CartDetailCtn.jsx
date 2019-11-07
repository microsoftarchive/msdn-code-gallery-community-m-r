import { connect } from 'react-redux';

import { getCartLineArr } from '../reduxStore/selectors/getCartLineArray';
import { updateQuantity, removeLine, clearCart } from '../reduxStore/actions/handleCart';
import CartDetail from '../viewComponents/cartDetail/CartDetail';

const mapStateToProps = (state) => {
    return {
        totalValue: state.cart.totalValue,
        lines: getCartLineArr(state)
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        updateQuantity: (skuId, quantity) => {
            dispatch(updateQuantity(skuId, quantity));
        },
        removeLine: (skuId) => {
            dispatch(removeLine(skuId));
        },
        clearCart: () => {
            dispatch(clearCart());
        }
    };
};

const CartDetailCtn = connect(mapStateToProps, mapDispatchToProps)(CartDetail);

export default CartDetailCtn;