import React from 'react';
import numeral from 'numeral';

import CartLines from '../cartLines/CartLines';

import '../../styles/app.scss';
import './cartTable.scss';

const CartTable = ({ totalValue, lines, skusOverStock, updateQuantity, removeLine }) => (
    <table className="table table-sm table-responsive-sm cus-font-sm">
        <thead>
            <tr>
                <th className="text-center">Skis</th>
                <th className="text-right">Price</th>
                <th className="text-center">Quantity</th>
                <th className="text-right cus-wide-column">Subtotal</th>
            </tr>
        </thead>
        <CartLines lines={lines} skusOverStock={skusOverStock} updateQuantity={updateQuantity} removeLine={removeLine} />
        <tfoot>
            <tr className="font-weight-bold">
                <td colSpan="3" className="text-right">Total:</td>
                <td className="text-right">
                    {`${numeral(totalValue).format('$0,0.00')}`}
                </td>
            </tr>
        </tfoot>
    </table>
);

export default CartTable;