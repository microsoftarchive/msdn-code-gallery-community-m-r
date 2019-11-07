/* global $: false */

import React from 'react';

import SkisReview from '../../viewComponents/skisReview/SkisReview';
import ModalReview from '../modalReview/ModalReview';
import BrMultiLines from '../../viewComponents/brMultiLines/BrMultiLines';

class SkisReviews extends React.Component {
    constructor(props) {
        super(props);

        this.closeReviewForm = this.closeReviewForm.bind(this);
    }

    styleId = () => {
        return this.props.styleId;
    }

    componentDidMount() {
        this.props.getReviews(this.styleId());
    }

    closeReviewForm() {
        $('#formReview').modal('hide');
    }

    render() {
        const { reviews, user, styleId, addReview } = this.props;

        return (
            <div>
                {typeof reviews === 'undefined' && <BrMultiLines />}

                {(reviews &&
                    reviews.length < 1) &&
                    <p>Please be the first to write a review.</p>
                }

                {reviews &&
                    reviews.length > 0 &&
                    reviews.map(review => (
                        <SkisReview key={review.reviewId} review={review} />
                    ))
                }

                <button className="btn btn-primary mb-5" data-toggle="modal" data-target="#formReview">
                    Write a review
                </button>

                <ModalReview styleId={styleId} user={user} addReview={addReview}
                             closeReviewForm={this.closeReviewForm} />
            </div>
        );
    }
}

export default SkisReviews;