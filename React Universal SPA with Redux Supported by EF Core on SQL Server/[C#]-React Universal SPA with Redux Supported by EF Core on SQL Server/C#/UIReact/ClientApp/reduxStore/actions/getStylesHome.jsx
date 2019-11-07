/* global Promise: false */

import { fetch } from 'domain-task';
import '@babel/polyfill';

import { getClientApiUrl, apiPaths} from '../../constants/webApi';
import types from '../../constants/actionTypes';
import { getStyleStates } from './getStyleStates';
import { dispatchStyles } from '../helpers/stylesHelper';

function fetchStylesHome() {
    return fetch(getClientApiUrl(apiPaths.getStylesHome));
}

function fetchStyle(styleId) {
    return fetch(`getClientApiUrl(apiPaths.getStyle)/${styleId}`);
}

export const getPopulars = (populars) => ({
    type: types.GET_POPULARS,
    populars
});

export const getClearances = (clearances) => ({
    type: types.GET_CLEARANCE,
    clearances
});

export const getStyle = (style) => ({
    type: types.ADD_STYLE,
    style
});

export function getStylesAsync() {
    return async (dispatch) => {
        try {
            const response = await fetchStylesHome();
            const data = await response.json();

            dispatchStyles(dispatch, getPopulars, getStyleStates, data.populars);
            dispatchStyles(dispatch, getClearances, getStyleStates, data.clearances);

        } catch (e) {
            console.log('failed to get styles on the homepage: ');
            console.log(e);
        } 

    };
};

export function getStyleAsync(styleId) {
    return async (dispatch) => {
        try {
            const response = await fetchStyle(styleId);
            const data = await response.json();

            dispatch(getStyle(data));

        } catch (e) {
            console.log(`failed to get style ${styleId}`);
            console.log(e);
        } 
    }
}

const shouldFetchStyles = (state) => {
    const { populars, clearances } = state;

    return (!populars || !clearances || (typeof populars.keys === 'undefined')
        || (typeof clearances.keys === 'undefined'));
}

const shouldFetchStyle = (state, styleId) => {
    const { styles } = state;

    return (!styles[styleId] || Object.keys(styles[styleId]).length === 0);
}

export default function getStylesIfNeeded() {
    return (dispatch, getState) => {
        return (shouldFetchStyles(getState())) ? dispatch(getStylesAsync) : Promise.resolve();
    }
}

export function getStyleIfNeeded(styleId) {
    return (dispatch, getState) => {
        return (shouldFetchStyle(getState(), styleId)) ? dispatch(getStyleAsync(styleId)) : Promise.resolve();
    }
}
