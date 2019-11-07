import React from 'react';
import { Link } from 'react-router-dom';

import Price from '../price/Price';

import './styleGeneral.scss'

const StyleGeneral = ({ ski, navTo}) => {
    const { brandName, styleName, genderName, priceCurrent, priceRegular } = ski;

    return (
        <div name="styleGeneral">
            <p name="brand" className="mb-0 text-muted">{brandName.toUpperCase()}</p>
            <Link to={navTo} >
                <strong className="text-dark">
                    {styleName} - {genderName}
                </strong>
            </Link>
            <Price current={priceCurrent} regular={priceRegular} />
        </div>
    );
};

export default StyleGeneral;