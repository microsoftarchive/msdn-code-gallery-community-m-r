import React from 'react';

import Price from '../price/Price';
import ReviewRating from '../reviewRating/ReviewRating';
import SkisForm from '../../viewContainers/skisForm/SkisForm';

import './skisGeneral.scss';

class SkisGeneral extends React.Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const { selectedCategoryId, selectCategory, getStyle, style, styleId } = this.props;

        if (!style) getStyle(styleId);

        if (style && selectedCategoryId !== style.categoryId) selectCategory(style.categoryId);
    }

    render() {
        if (!this.props.style) return null;
        
        const { style, addToCart } = this.props;
        const { brandName, styleName, genderName, priceCurrent, priceRegular } = this.props.style;
        const { averageRatings, reviewCount, soldOut } = this.props.styleState;

        return (
            <div>
                <h5 className="text-muted" name="skisGeneral">{brandName.toUpperCase()}</h5>
                <h4 name="skisGeneral">{styleName} - {genderName}</h4>
                <h5 name="skisGeneral">
                    <Price current={priceCurrent} regular={priceRegular}/>
                </h5>
                <ReviewRating averageRatings={averageRatings} reviewCount={reviewCount} />
                { soldOut && <div className="text-danger" name="skisGeneral">Sold Out</div> }  

                <SkisForm style={style} soldOut={soldOut} addToCart={addToCart}/>
            </div >
        );
    }
}

export default SkisGeneral;