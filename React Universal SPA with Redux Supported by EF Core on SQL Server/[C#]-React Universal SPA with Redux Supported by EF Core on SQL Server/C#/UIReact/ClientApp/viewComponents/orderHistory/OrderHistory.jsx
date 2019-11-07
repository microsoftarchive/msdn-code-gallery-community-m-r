import React from 'react';

import ImgSide from '../imgSide/ImgSide';
import OrderHistoryTable from '../orderHistoryTable/OrderHistoryTable';
import selectDefaultCategory from '../../reduxStore/helpers/selectDefaultCategory';

class OrderHistory extends React.Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const { selectedCategoryId, selectCategory, user, getOrders, navToOrderInquiry } = this.props;

        selectDefaultCategory(selectedCategoryId, selectCategory);

        if (user && Object.keys(user).length > 0 && user.userId > 0) {
            getOrders(user.userId);
        }

        if (!user || Object.keys(user).length < 1 || user.userId < 1) {
            navToOrderInquiry();
        }
    }

    componentDidUpdate() {
        const { user, orders, getOrders, navToOrderInquiry } = this.props;

        if (user && Object.keys(user).length > 0 && user.userId > 0 && !orders ) {
            getOrders(user.userId);
        }

        if (!user || Object.keys(user).length < 1 || user.userId < 1) {
            navToOrderInquiry();
        }
    }

    render() {
        const { user, orders } = this.props;
        
        return (
            <div className="container-fluid">
                <div className="row">
                    <div className="col-md-3 p-0 d-none d-md-block">
                        <ImgSide source="image/left_order.jpeg" />
                    </div>
                    <div className="col-md-9 col-sm-12">
                        <Orders user={user} orders={orders} /> 
                    </div>
                </div>
            </div>
            );
    }
}

const Orders = ({ user, orders }) => {

    if (!user || Object.keys(user).length < 1 || user.userId < 1) {
        return null;
    }

    if (orders && orders.length > 0)
        return (
            <OrderHistoryTable orders={orders} className="m-1" />
        );

    if (orders && orders.length === 0)
        return (
            <div className="m-1 alert alert-warning">You have not placed any orders yet.</div>
        );

    return null;
}

export default OrderHistory;
