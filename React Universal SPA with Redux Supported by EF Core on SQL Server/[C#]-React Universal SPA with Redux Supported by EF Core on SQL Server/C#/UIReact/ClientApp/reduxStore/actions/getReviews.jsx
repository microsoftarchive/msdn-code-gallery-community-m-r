import { fetch } from 'domain-task';
import '@babel/polyfill';

import { getClientApiUrl, apiPaths } from '../../constants/webApi';
import types from '../../constants/actionTypes';
import { updateStyleState } from './getStyleStates';
import getOptions from '../helpers/postOptions';

function fetchReviews(styleId) {
    return fetch(`${getClientApiUrl(apiPaths.getReviews)}/${styleId}`);
}

function postReview(reviewModel) {
    return fetch(getClientApiUrl(apiPaths.postReview), getOptions(reviewModel));
}

export const getReviews = (styleId, reviews) => ({
    type: types.GET_REVIEWS,
    styleId,
    reviews
});

export const addReview = (styleId, review) => ({
    type: types.ADD_REVIEW,
    styleId,
    review
});

function getReviewsAsync(styleId) {
    return async (dispatch) => {
        try {
            const response = await fetchReviews(styleId);
            const data = await response.json();

            const { styleState, reviews } = data;

            dispatch(getReviews(styleId, reviews));

            dispatch(updateStyleState(styleId, styleState));

        } catch (e) {
            console.log(`failed to get reviews for ${styleId}.`);
            console.log(e);
        } 
    }
}

const shouldFetchReviews = (state, styleId) => {
    const { reviews } = state;
    const reviewCount = state.styleStates[styleId].reviewCount;

    return (!Object.keys(reviews).includes(styleId)) || (reviews[styleId].length !== reviewCount);
};

export function postReviewAsync(reviewModel, user) {
    return async (dispatch) => {
        try {
            const response = await postReview(reviewModel);
            const data = await response.json();

            const { reviewId, styleState } = data;
            const { styleId, rating, reviewText, createdDateTime } = reviewModel; 

            dispatch(updateStyleState(styleId, styleState));

            const review = {
                reviewId: reviewId,
                screenName: user.screenName,
                rating: rating,
                reviewText: reviewText,
                createdDateTime: createdDateTime
            }

            dispatch(addReview(reviewModel.styleId, review));

        } catch (e) {
            console.log(`failed to save a reviews for ${reviewModel.styleId}.`);
            console.log(e);
        } 
        
    }
}

export default function getReviewsIfNeeded(styleId) {
    return (dispatch, getState) => {
        return shouldFetchReviews(getState(), styleId) ? dispatch(getReviewsAsync(styleId)) : Promise.resolve();
    };
}