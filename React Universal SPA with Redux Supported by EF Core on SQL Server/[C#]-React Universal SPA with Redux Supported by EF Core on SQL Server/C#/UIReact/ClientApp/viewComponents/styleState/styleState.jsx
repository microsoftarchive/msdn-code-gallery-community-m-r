import React from 'react';

import ReviewRating from '../reviewRating/ReviewRating';

const StyleState = ({ state }) => {
    const { averageRatings, reviewCount, soldOut } = state;

    return (
        <div className="mt-0 pt-0">
            {reviewCount > 0 &&
                <ReviewRating averageRatings={averageRatings} reviewCount={reviewCount} />
            }
            {soldOut === true &&
                <p className="text-danger font-weight-bold">Sold Out</p>
            }
        </div>
    );
}


export default StyleState;