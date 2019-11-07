import { connect } from 'react-redux';

import { getCartLineArr } from '../reduxStore/selectors/getCartLineArray';
import { updateQuantity, removeLine, clearCart } from '../reduxStore/actions/handleCart';
import CartTable from '../viewComponents/cartTable/CartTable';

const mapStateToProps = (state) => {
    return {
        totalValue: state.cart.totalValue,
        lines: getCartLineArr(state), 
        skusOverStock: state.skusOverStock
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        updateQuantity: (skuId, quantity) => {
            dispatch(updateQuantity(skuId, quantity));
        },
        removeLine: (skuId) => {
            dispatch(removeLine(skuId));
        }
    };
};

const CartTableCtn = connect(mapStateToProps, mapDispatchToProps)(CartTable);

export default CartTableCtn;