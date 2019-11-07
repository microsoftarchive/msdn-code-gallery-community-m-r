/* global Promise: false */

import { fetch } from 'domain-task';
import '@babel/polyfill';

import { getClientApiUrl, apiPaths } from '../../constants/webApi';
import types from '../../constants/actionTypes';

function fetchCategories() {
    return fetch(getClientApiUrl(apiPaths.getCategories));
}

export const getCategories = (categories) => ({
    type: types.GET_CATEGORIES,
    categories
});

export function getCategoriesAsync() {
    return async (dispatch) => {
        try {
            const response = await fetchCategories();
            const categories = await response.json();

            dispatch(getCategories(categories));

        } catch (e) {
            console.log('failed to get categories: ');
            console.log(e);
        } 

    };
}

function shouldFetchCategories(state) {
    const { categories } = state;

    return !categories || (typeof (categories.keys) === 'undefined');
}

export default function getCategoriesIfNeeded()
{
    return (dispatch, getState) => {
        return (shouldFetchCategories(getState())) ? dispatch(getCategoriesAsync) : Promise.resolve();
    }
}
