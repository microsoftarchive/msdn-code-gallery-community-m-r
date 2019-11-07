import React from 'react';
import { Link } from 'react-router-dom';
import numeral from 'numeral';
import { stringify } from 'qs';

import routePaths from '../../constants/routes';

import '../../styles/app.scss';

const navTo = (customerOrderId) => ({
    pathname: routePaths.orderDetail,
    search: stringify({
        customerOrderId: customerOrderId
    })
});

const OrderHistoryTable = ({ orders }) => (
    <div className="m-1 cus-font-sm ">
        <h6 className="mt-3 mb-4">Your Orders</h6>
        <table className="table table-striped">
            <thead>
            <tr className="text-center">
                <th>Order</th>
                <th>Created On</th>
                <th>City</th>
                <th>Value</th>
            </tr>
            </thead>
            <tbody>
            {orders.map(order => (
                    <tr key={order.orderId}>
                        <td>
                            <Link to={navTo(order.customerOrderId)}>
                                {order.customerOrderId}
                            </Link>
                        </td>
                        <td>{order.createdDateTime}</td>
                        <td>{order.city}</td>
                        <td>{`${numeral(order.totalValue).format('$0,0.00')}`}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    </div>
);

export default OrderHistoryTable;
