import React from 'react';
import numeral from 'numeral';
import { Link } from 'react-router-dom';

import routePaths from '../../constants/routes';

import './cartLine.scss';

class CartLine extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            quantityUpdated: this.props.line.quantity
        }

        this.updateQuantity = this.updateQuantity.bind(this);
    }

    updateQuantity(event) {
        const quantity = event.target.value;
        const {line} = this.props;

        this.setState({
            quantityUpdated: quantity
        });

        this.props.updateQuantity(line.skuId, quantity);
    }

    render() {
        const { skuId, styleId, skis, size, price, quantity, subTotal } = this.props.line;
        const { removeLine, skusOverStock } = this.props;
        const { quantityUpdated } = this.state;

        const styleName = skis.split('-')[1];
        const navTo = `${routePaths.skis}/${styleName}/${styleId}`;

        const skuOverStock = skusOverStock.find(sku => sku.skuId === skuId);
        
        const quantityOverStock = skuOverStock
            ? skuOverStock.quantity
            : -1;

        return (
            <tr>
                <td className="text-left">
                    <Link to={navTo} >{skis}</Link>
                </td>
                <td className="text-right">{`${numeral(price).format('$0,0.00')}`}</td>
                <td style={{paddingTop: 0}} className="text-right">
                    <input type="number" min="1" max="99"
                        className="form-control-sm cus-num-input-sm"
                           value={quantityUpdated}
                        onChange={this.updateQuantity} />
                    {quantityOverStock === 0 &&
                        <div className="text-danger">Sold Out</div>
                    }
                    {quantityOverStock > 0 && quantity > quantityOverStock &&
                        <div className="text-danger">Over Stock</div>
                    }
                </td>
                <td className="text-right">
                    {`${numeral(subTotal).format('$0,0.00')}`}
                </td>
                <td className="text-center border-0">
                    <button className="btn btn-outline-primary cus-btn-xs" onClick={removeLine}>
                        Remove
                    </button>
                </td>
            </tr>
        );
    }
}

export default CartLine;

