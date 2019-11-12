import { connect } from 'react-redux';

import getReviewsIfNeeded, { postReviewAsync} from '../reduxStore/actions/getReviews';
import SkisReviews from './skisReviews/SkisReviews';

const mapStateToProps = (state, ownProps) => {
    const styleId = ownProps.styleId;

    return {
        reviews: state.reviews[styleId],
        user: state.user
    };
}

const mapDispatchToProps = (dispatch) => {
    return {
        getReviews: (styleId) => {
            dispatch(getReviewsIfNeeded(styleId));
        },
        addReview: (reviewModel, user) => {
            dispatch(postReviewAsync(reviewModel, user));
        }
    }
}

const SkisReviewsCtn = connect(mapStateToProps, mapDispatchToProps)(SkisReviews);

export default SkisReviewsCtn;
