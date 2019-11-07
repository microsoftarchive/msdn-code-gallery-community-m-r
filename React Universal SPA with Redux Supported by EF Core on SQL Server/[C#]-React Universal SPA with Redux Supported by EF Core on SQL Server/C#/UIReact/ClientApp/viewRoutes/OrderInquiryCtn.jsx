import { connect } from 'react-redux';
import { push } from 'connected-react-router'
import { stringify } from 'qs';

import OrderInquiry from '../viewContainers/orderInquiry/OrderInquiry';
import routePaths from '../constants/routes';
import getSelectedCategoryId from '../reduxStore/actions/getSelectedCategoryId';
import { updateOrderFound, getOrderDetailByIdEmailIfNeeded } from '../reduxStore/actions/handleOrders';

const mapStateToProps = (state) => ({
    selectedCategoryId: state.selectedCategoryId,
    user: state.user,
    orderFound: state.orderFound
});

const mapDispatchToProps = (dispatch) => ({
    navToOrderHistory: () => {
        dispatch(push(routePaths.orderHistory));
    },
    selectCategory: (categoryId) => {
        dispatch(getSelectedCategoryId(categoryId));
    }, 
    updateIfFoundOrder: (orderFound) => {
        dispatch(updateOrderFound(orderFound));
    },
    getOrderDetail: (customerOrderId, email) => {
        dispatch(getOrderDetailByIdEmailIfNeeded(customerOrderId, email));
    },
    navToDetail: (customerOrderId, email) => {
        const navTo = {
            pathname: routePaths.orderDetail,
            search: stringify({
                customerOrderId: customerOrderId,
                email: email
            })
        };

        dispatch(push(navTo));
    }
});

const OrderInquiryCtn = connect(mapStateToProps, mapDispatchToProps)(OrderInquiry);

export default OrderInquiryCtn;