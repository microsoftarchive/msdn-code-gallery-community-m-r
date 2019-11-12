import React from 'react';

import RatingOnly from '../ratingOnly/RatingOnly';

import './skisReview.scss';

const SkisReview = ({ review }) => (
    <div key={review.reviewId}>
        <div className="font-weight-bold">{review.screenName}</div>
        <div className="smallStars"><RatingOnly rating={review.rating} /></div>
        <small>{review.createdDateTime}</small>
        <div>{review.reviewText}</div>
        <hr />
    </div>
);

export default SkisReview;