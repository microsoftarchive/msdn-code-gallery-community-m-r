import React from 'react';
import numeral from 'numeral';
import { parse } from 'qs';

import selectDefaultCategory from '../../reduxStore/helpers/selectDefaultCategory';
import ImgSide from '../imgSide/ImgSide';

import '../../styles/app.scss';

class OrderDetail extends React.Component {
    constructor(props) {
        super(props);
    }

    getEmail = (location) => {
        const queryObj = parse(location.search.substring(1));

        if (Object.keys(queryObj).includes('email')) {
            return queryObj.email;
        }

        return null;
    }

    componentDidMount() {
        const { selectedCategoryId, selectCategory, user, getOrderDetailById, getOrderDetailByIdEmail } = this.props;

        selectDefaultCategory(selectedCategoryId, selectCategory);

        if (user && Object.keys(user).length > 0 && user.userId > 0) getOrderDetailById();

        if (!user || Object.keys(user).length < 1 || user.userId < 1) getOrderDetailByIdEmail();
    }

    componentDidUpdate() {
        const { user, getOrderDetailById, getOrderDetailByIdEmail, orderDetail } = this.props;

        if (user && Object.keys(user).length > 0 && user.userId > 0 && !orderDetail) getOrderDetailById();

        if (!user || Object.keys(user).length < 1 || user.userId < 1 && !orderDetail) getOrderDetailByIdEmail();
    }

    render() {
        const { orderDetail, user, location } = this.props;

        if (!orderDetail) return null;

        const queryEmail = this.getEmail(location);
        if (queryEmail && user && Object.keys(user).length > 0 && user.userId > 0 && queryEmail !== user.email)
            return (
                <div className="alert alert-warning">No order detail for you.</div>
            );

        return (
            <div className="container-fluid">
                <div className="row">
                    <div className="col-md-3 p-0 d-none d-md-block">
                        <ImgSide source="image/left_orderDetail.jpg" />
                    </div>
                    <div className="col-md-9 col-sm-12">
                        <h6 className="my-3">
                            Order {orderDetail.customerOrderId}
                        </h6>
                        <OrderDetailTable orderDetail={orderDetail} />
                        <ShipTo orderDetail={orderDetail}/>
                    </div>
                </div>
            </div>
        );
    }
}

export default OrderDetail;

const OrderDetailTable = ({ orderDetail }) => (
    <table className="table table-sm table-responsive-sm cus-font-sm">
        <thead>
            <tr>
                <th className="text-center">Skis</th>
                <th className="text-right">Price</th>
                <th className="text-right">Quantity</th>
                <th className="text-right">Subtotal</th>
            </tr>
        </thead>
        <tbody>
            {orderDetail.orderItems.map(item => (
                <tr key={item.skuId}>
                    <td>{item.skis}</td>
                    <td className="text-right">{`${numeral(item.price).format('$0,0.00')}`}</td>
                    <td className="text-center">{item.quantity}</td>
                    <td className="text-right">{`${numeral(item.subTotal).format('$0,0.00')}`}</td>
                </tr>
            ))}
        </tbody>
        <tfoot>
        <tr className="font-weight-bold">
            <td colSpan="3" className="text-right">Total:</td>
            <td className="text-right">
                {`${numeral(orderDetail.totalValue).format('$0,0.00')}`}
            </td>
        </tr>
        </tfoot>
    </table>
);

const ShipTo = ({ orderDetail }) => (
    <div className="cus-font-sm">
        <div className="font-weight-bold">Ship to:</div>
        <div>{orderDetail.fullName}</div>
        <div>{orderDetail.addressLine}</div>
        <div>{`${orderDetail.city} ${orderDetail.provinceName} ${orderDetail.postalCode}`}</div>
    </div>
);
