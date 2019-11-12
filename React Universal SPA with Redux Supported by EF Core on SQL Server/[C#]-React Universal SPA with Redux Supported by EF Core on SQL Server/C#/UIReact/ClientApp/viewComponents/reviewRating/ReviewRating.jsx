import React from 'react';

import RatingOnly from '../ratingOnly/RatingOnly';

const ReviewRating = ({ averageRatings, reviewCount }) => (
    <div name="reviewRating">
        <RatingOnly rating={averageRatings}/>
        <span> &nbsp;( {reviewCount} )</span>
    </div>
);

export default ReviewRating;