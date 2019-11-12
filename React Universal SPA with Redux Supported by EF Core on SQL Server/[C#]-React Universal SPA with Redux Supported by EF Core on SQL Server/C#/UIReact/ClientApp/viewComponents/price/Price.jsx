import React from 'react';
import numeral from 'numeral';

import './price.scss';

const priceFormat = '$0,0.00';

const showPrice = (price) => {
    return numeral(price).format(priceFormat);
} 

const Price = ({ current, regular }) => {
    
    return (current < regular)
        ? (
            <p name="price">
                <del>
                    {showPrice(regular)}
                </del>
                <span>
                    &nbsp;{showPrice(current)}
                </span>
            </p>
        )
        : (
            <p name="price">
                {showPrice(current)}
            </p>
        );
}

export default Price;