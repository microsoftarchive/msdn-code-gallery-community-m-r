import { fetch } from 'domain-task';
import '@babel/polyfill';

import { getClientApiUrl, apiPaths } from '../../constants/webApi';
import types from '../../constants/actionTypes';
import { updateLoginStatus } from './getUser';

function fetchLoginStatus() {
    return fetch(getClientApiUrl(apiPaths.getLogInStatus), {
        credentials: 'include'
    });
};

export const getIsLoggedInInitiallyChecked = (isChecked) => ({
    type: types.UPDATE_ISLOGGEDINCHECHED,
    isChecked
});

function fetchLoginStatusAsync() {
    return async (dispatch) => {
        try {
            const response = await fetchLoginStatus();
            const data = await response.json();

            dispatch(getIsLoggedInInitiallyChecked(true));
            dispatch(updateLoginStatus(data));

        } catch (e) {
            console.log('failed to check login status.');
            console.log(e);
        } 
    }
}

const shouldFetchLoginStatus = (state) => {
    return !state.isLoggedInInitiallyChecked;
}

export default function checkLoginStatusIfNeeded() {
    return (dispatch, getState) => {
        return shouldFetchLoginStatus(getState()) ? dispatch(fetchLoginStatusAsync()) : Promise.resolve();
    }
} 