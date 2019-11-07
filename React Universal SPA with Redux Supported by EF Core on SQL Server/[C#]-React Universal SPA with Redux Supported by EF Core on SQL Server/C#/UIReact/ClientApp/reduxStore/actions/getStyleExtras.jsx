import { fetch } from 'domain-task';
import '@babel/polyfill';

import { getClientApiUrl, apiPaths } from '../../constants/webApi';
import types from '../../constants/actionTypes';
import { updateStyleState } from './getStyleStates';
import { addSkus } from './getSkus';
import getDescriptions from './getDescriptions';
import sortSkusBySize from '../helpers/sortSkusBySize';

function fetchSkis(styleId) {
    return fetch(`${getClientApiUrl(apiPaths.getSkis)}/${styleId}`);
}

export const addStyleExtra = (styleExtra) => ({
    type: types.ADD_STYLEEXTRA,
    styleExtra
});

function getSkisAsync(styleId) {

    return async (dispatch) => {
        try {
            const response = await fetchSkis(styleId);
            const data = await response.json();

            const skus = sortSkusBySize(data.skus);

            dispatch(addStyleExtra(data.styleExtra));
            dispatch(updateStyleState(styleId, data.state));
            dispatch(addSkus(styleId, skus));
            dispatch(getDescriptions(styleId, data.descs));

        } catch (e) {
            console.log(`failed to get skis ${styleId}`);
            console.log(e);
        }
    };
}

const shouldFetchSkis = (state, styleId) => {
    const { styleExtras, skus, descriptions, styleStates } = state;

    return (!Object.keys(styleExtras).includes(styleId) ||
        !Object.keys(skus).includes(styleId) ||
        !Object.keys(styleStates).includes(styleId) ||
        !IfSoldOutMatchSkus(skus[styleId], styleStates[styleId]) ||
        !Object.keys(descriptions).includes(styleId));
}

export default function getSkisIfNeeded(styleId) {
    return (dispatch, getState) => {
        return (shouldFetchSkis(getState(), styleId)) ? dispatch(getSkisAsync(styleId)) : Promise.resolve();
    }
}

const IfSoldOutMatchSkus = (skus, styleState) => {
    const sum = skus.reduce((accu, curr) => accu + curr.quantity,
        0);

    return (styleState.soldOut && sum === 0) || (!styleState.soldOut && sum > 0) ? true : false;
}




