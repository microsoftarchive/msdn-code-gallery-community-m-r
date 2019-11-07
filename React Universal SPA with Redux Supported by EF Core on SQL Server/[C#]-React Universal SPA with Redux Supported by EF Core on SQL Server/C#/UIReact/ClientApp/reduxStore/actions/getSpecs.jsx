import { fetch } from 'domain-task';
import '@babel/polyfill';

import { getClientApiUrl, apiPaths } from '../../constants/webApi';
import types from '../../constants/actionTypes';

function fetchSpecs(styleId) {
    return fetch(`${getClientApiUrl(apiPaths.getSpecs)}/${styleId}`);
}

export const getSpecs = (styleId, specs) => ({
    type: types.ADD_SPECS,
    styleId,
    specs
});

function getSpecsAsync(styleId) {
    return async (dispatch) => {
        try {
            const response = await fetchSpecs(styleId);
            const data = await response.json();

            dispatch(getSpecs(styleId, data));

        } catch (e) {
            console.log(`failed to get specs for ${styleId}.`);
            console.log(e);
        }
    };
}

const shouldFetchSpecs = (state, styleId) => {
    const { specs } = state;

    return !(Object.keys(specs).includes(styleId));
};

export default function getSpecsIfNeeded(styleId) {
    return (dispatch, getState) => {
        return (shouldFetchSpecs(getState(), styleId)) ? dispatch(getSpecsAsync(styleId)) : Promise.resolve();
    }
}
