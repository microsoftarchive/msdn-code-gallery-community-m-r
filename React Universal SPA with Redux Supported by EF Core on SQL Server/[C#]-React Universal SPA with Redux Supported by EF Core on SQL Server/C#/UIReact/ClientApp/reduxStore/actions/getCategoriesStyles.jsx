/* global require: false */

import { fetch } from 'domain-task';

import '@babel/polyfill'; 

import { getServerApiUrl } from '../../constants/webApi';
import { getCategories } from './getCategories';
import { getPopulars, getClearances } from './getStylesHome';
import { getStyleStates } from './getStyleStates';
import { dispatchStyles } from '../helpers/stylesHelper';
import { mapArrToObj } from '../helpers/arrayToObject';

function fetchCategoriesStyles()
{
    return fetch(getServerApiUrl());
}

export default function getCategoriesStylesAsync() {
    return async (dispatch) => {
        try {
            const response = await fetchCategoriesStyles();
            const data = await response.json();

            const categories = mapArrToObj(data.categories, 'categoryId');
            dispatch(getCategories(categories));

            dispatchStyles(dispatch, getPopulars, getStyleStates, data.populars);
            dispatchStyles(dispatch, getClearances, getStyleStates, data.clearances);

        } catch (e) {
            console.log('failed to get data for categories and styles: ');
            console.log(e);
        } 
    };
}


