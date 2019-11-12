import { connect } from 'react-redux';

import Cart from '../viewComponents/cart/Cart';
import getSelectedCategoryId from '../reduxStore/actions/getSelectedCategoryId';

const mapStateToProps = (state) => {
    return {
        selectedCategoryId: state.selectedCategoryId,
        itemCount: state.cart.itemCount
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        selectCategory: (categoryId) => {
            dispatch(getSelectedCategoryId(categoryId));
        }
    };
};

const CartCtn = connect(mapStateToProps, mapDispatchToProps)(Cart);

export default CartCtn;
