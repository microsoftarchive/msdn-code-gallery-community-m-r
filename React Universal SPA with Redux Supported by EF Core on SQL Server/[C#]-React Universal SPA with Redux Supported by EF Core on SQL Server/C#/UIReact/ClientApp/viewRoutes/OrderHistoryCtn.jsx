import { connect } from 'react-redux';
import { push } from 'connected-react-router'

import OrderHistory from '../viewComponents/orderHistory/OrderHistory';
import getOrdersByUser from '../reduxStore/selectors/getOrdersByUserId';
import { getOrdersByUserIdIfNeeded } from '../reduxStore/actions/handleOrders';
import getSelectedCategoryId from '../reduxStore/actions/getSelectedCategoryId';
import routePaths from '../constants/routes';

const mapStateToProps = (state) => (
    {
        selectedCategoryId: state.selectedCategoryId,
        user: state.user,
        orders: getOrdersByUser(state)
    }
);

const mapDispatchToProps = (dispatch) => ({
    getOrders: (userId) => {
        dispatch(getOrdersByUserIdIfNeeded(userId));
    }, 
    selectCategory: (categoryId) => {
        dispatch(getSelectedCategoryId(categoryId));
    }, 
    navToOrderInquiry: () => {
        dispatch(push(routePaths.orderInquiry));
    }
});

const OrderHistoryCtn = connect(mapStateToProps, mapDispatchToProps)(OrderHistory);

export default OrderHistoryCtn;