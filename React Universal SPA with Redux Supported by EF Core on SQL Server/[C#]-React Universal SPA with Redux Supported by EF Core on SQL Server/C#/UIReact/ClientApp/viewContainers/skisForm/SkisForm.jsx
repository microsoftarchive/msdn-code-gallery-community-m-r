import React from 'react';

import BtnGroupCtn from '../BtnGroupCtn';
import ErrorMessage from '../../viewComponents/errorMessage/ErrorMessage';

import './skisForm.scss';

class SkisForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            skuPicked: {},
            isSkuPicked: true,
            isQuantityPicked: false,
            isInteger: true,
            quantityPicked: 1
        };

        this.pickSize = this.pickSize.bind(this);
        this.pickQuantity = this.pickQuantity.bind(this);
        this.addToCart = this.addToCart.bind(this);
    }

    pickSize(sku) {
        this.setState({
            skuPicked: { ...sku },
            isSkuPicked: true
        });
    }

    pickQuantity(event) {
        const value = Number(event.target.value);

        this.setState(() => {
            return {
                quantityPicked: value,
                isInteger: this.isInputInteger(value)
            };
        });
    }

    addToCart(e) {
        e.preventDefault();

        const { skuPicked, quantityPicked } = this.state;
        const { style, addToCart } = this.props;

        if (Object.keys(skuPicked).length < 1) {
            this.setState({ isSkuPicked: false });
            return;
        }

        if (!this.isInputInteger(quantityPicked)) {
            this.setState({ isInteger: false });
            return;
        } else {
            this.setState({ isInteger: true });
        }

        addToCart(style, skuPicked, quantityPicked);
    }

    isInputInteger(value) {
        return Number.isInteger(Number(value));
    }

    render() {
        const { skuPicked, isSkuPicked, isInteger } = this.state;
        const soldOut = this.props.soldOut;

        return (
            <div>
                <BtnGroupCtn styleId={this.props.style.styleId} skuPicked={skuPicked} pickSize={this.pickSize} />
                <div name="skisFormMsg" >
                    <ErrorMessage predicate={!isSkuPicked} message="Please pick a size" />
                </div>

                <div className="mt-3">
                    <label>Quantity:</label>
                    <div className="row">
                        <div className="col-6">
                            <input type="number" min="1" max="99"
                                className="form-control"
                                value={this.state.quantityPicked}
                                onChange={this.pickQuantity} />
                        </div>
                    </div>
                </div>
                <div name="skisFormMsg">
                    <ErrorMessage predicate={!isInteger} message="Please input an integer." />
                </div>

                <button className="btn btn-primary btn-lg mt-4"
                    disabled={soldOut}
                    onClick={this.addToCart}>
                    <span className="fas fa-shopping-cart"></span>
                    &nbsp; ADD TO CART
                </button>
            </div>
        );
    }
}

export default SkisForm;