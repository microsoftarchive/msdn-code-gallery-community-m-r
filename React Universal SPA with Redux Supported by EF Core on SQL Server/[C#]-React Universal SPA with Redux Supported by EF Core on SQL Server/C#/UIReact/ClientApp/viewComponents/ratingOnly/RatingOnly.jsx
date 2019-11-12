import React from 'react';
import Rating from 'react-rating';

import './ratingOnly.scss';

const RatingOnly = ({ rating }) => (
    <Rating emptySymbol="fa fa-star empty" fullSymbol="fa fa-star full"
            initialRating={rating} readonly={true} />
);

export default RatingOnly;