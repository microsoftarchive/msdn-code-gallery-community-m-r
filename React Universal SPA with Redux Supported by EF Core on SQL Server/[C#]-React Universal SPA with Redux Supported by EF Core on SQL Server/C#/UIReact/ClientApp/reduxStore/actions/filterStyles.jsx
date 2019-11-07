// @flow

import { fetch } from 'domain-task';
import '@babel/polyfill';

import { getClientApiUrl, apiPaths } from '../../constants/webApi';
import types from '../../constants/actionTypes';
import { dispatchStylesOnly } from '../helpers/stylesHelper';
import { getStyleStates } from './getStyleStates';

function fetchStylesFiltered(queryStr: string) {
    return fetch(`${getClientApiUrl(apiPaths.getStylesFiltered)}?${queryStr}`);
}

export const getStylesFiltered = (queryStr: string, result: Object) => ({
    type: types.GET_STYLES_FILTERED,
    queryStr,
    result
});

function getStylesFilteredAsync(queryStr: string) {
    return async (dispatch: any) => {
        try {
            const response = await fetchStylesFiltered(queryStr);
            const data = await response.json();

            const result = { ...data };

            if (result.totalCount > 0) {
                const stylesFiltered = data.stylesFiltered.map(style => style.styleId);
                result.stylesFiltered = [...stylesFiltered];
                dispatchStylesOnly(dispatch, getStyleStates, data.stylesFiltered);
            }

            dispatch(getStylesFiltered(queryStr, result));

        } catch (e) {
            console.log('failed to get filtered styles');
            console.log(e);
        } 
    }
}

const shouldFetchStylesFiltered = (state: any, queryStr: string) => {
    const { resultsFiltered } = state;

    return !Object.keys(resultsFiltered).includes(queryStr);
} 

export function getStylesFilteredIfNeeded(queryStr: string) {
    return (dispatch: any, getState: any) => {
        return shouldFetchStylesFiltered(getState(), queryStr)
            ? dispatch(getStylesFilteredAsync(queryStr)) 
            : Promise.resolve();
    }
}

