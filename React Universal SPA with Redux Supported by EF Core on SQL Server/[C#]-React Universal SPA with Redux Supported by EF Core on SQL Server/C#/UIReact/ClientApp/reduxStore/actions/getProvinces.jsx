import { fetch } from 'domain-task';
import '@babel/polyfill';

import { getClientApiUrl, apiPaths } from '../../constants/webApi';
import types from '../../constants/actionTypes';

function fetchProvinces() {
    return fetch(`${getClientApiUrl(apiPaths.getProvinces)}`);
}

export const getProvinces = (provinces) => ({
    type: types.GET_PROVINCES,
    provinces
});

function getProvincesAsync() {
    return async (dispatch) => {
        try {
            const response = await fetchProvinces();
            const data = await response.json();

            dispatch(getProvinces(data));

        } catch (e) {
            console.log('failed to get provinces.');
            console.log(e);
        } 
    }
}

const shouldFetchProvinces = (state) => {
    const { provinces } = state;

    return !provinces || provinces.length === 0;
}

export default function getProvincesIfNeeded() {
    return (dispatch, getState) => {
        return shouldFetchProvinces(getState()) ? dispatch(getProvincesAsync()) : Promise.resolve();
    }
}