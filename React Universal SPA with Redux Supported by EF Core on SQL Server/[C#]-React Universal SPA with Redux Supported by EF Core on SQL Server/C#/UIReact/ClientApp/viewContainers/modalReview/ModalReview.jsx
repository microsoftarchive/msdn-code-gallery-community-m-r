import React from 'react';

import FormReview from '../formReview/FormReview';

const ModalReview = ({ styleId, user, addReview, closeReviewForm}) => (
    <div className="modal fade" id="formReview" tabIndex="-1" role="dialog"
         aria-labelledby="formReview" aria-hidden="true">
        <div className="modal-dialog" role="document">
            <div className="modal-content">
                <div className="modal-header">
                    <h5>Your Review</h5>
                    <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div className="modal-body">
                    {user &&
                        user !== {} &&
                        user.userId > 0 &&
                        <FormReview styleId={styleId} user={user} addReview={addReview}
                                    closeReviewForm={closeReviewForm}/>
                    }
                    {(!user || Object.keys(user) < 1 || user.userId < 0) &&
                        <div className="mb-3">Please log in before writing your review.</div>
                    }
                </div>
                {(!user || Object.keys(user) < 1 || user.userId < 0) &&
                    <div className="modal-footer">
                        <button type="button" className="btn btn-primary" data-dismiss="modal">
                            Close
                        </button>
                    </div>
                }
            </div>
        </div>
    </div>
);

export default ModalReview;


