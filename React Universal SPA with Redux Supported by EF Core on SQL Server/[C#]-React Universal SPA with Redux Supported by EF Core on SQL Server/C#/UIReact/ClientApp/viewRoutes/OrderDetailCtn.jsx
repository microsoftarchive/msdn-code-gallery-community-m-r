import { connect } from 'react-redux';
import { parse } from 'qs';

import OrderDetail from '../viewComponents/orderDetail/OrderDetail';
import { getOrderDetailByIdIfNeeded, getOrderDetailByIdEmailIfNeeded } from '../reduxStore/actions/handleOrders';
import getSelectedCategoryId from '../reduxStore/actions/getSelectedCategoryId';

const getCustomerOrderId = (location) => {
    return parse(location.search.substring(1)).customerOrderId;
}

const getEmail = (location) => {
    return parse(location.search.substring(1)).email;
}

const mapStateToProps = (state, ownProps) => ({
    selectedCategoryId: state.selectedCategoryId,
    user: state.user,
    orderDetail: state.orderDetails[getCustomerOrderId(ownProps.location)]
});

const mapDispatchToProps = (dispatch, ownProps) => ({
    getOrderDetailById: () => {
        dispatch(getOrderDetailByIdIfNeeded(getCustomerOrderId(ownProps.location)));
    },
    getOrderDetailByIdEmail: () => {
        const { location } = ownProps;
        dispatch(getOrderDetailByIdEmailIfNeeded(getCustomerOrderId(location), getEmail(location)));
    },
    selectCategory: (categoryId) => {
        dispatch(getSelectedCategoryId(categoryId));
    }
});

const OrderDetailCtn = connect(mapStateToProps, mapDispatchToProps)(OrderDetail);

export default OrderDetailCtn;