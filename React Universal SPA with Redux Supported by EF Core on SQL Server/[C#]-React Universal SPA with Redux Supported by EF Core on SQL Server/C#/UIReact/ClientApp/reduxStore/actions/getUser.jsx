import { fetch } from 'domain-task';
import '@babel/polyfill';

import { getClientApiUrl, apiPaths } from '../../constants/webApi';
import types from '../../constants/actionTypes';
import getOptions from '../helpers/postOptions';

function fetchLogin(loginModel) {
    return fetch(getClientApiUrl(apiPaths.logIn), getOptions(loginModel));
}

function fetchLogout() {
    return fetch(getClientApiUrl(apiPaths.logOut), getOptions(false));
}

export const login = (user) => ({
    type: types.LOGIN,
    user
});

export const logout = () => ({
    type: types.LOGOUT,
    user: {}
});

export const updateLoginStatus = (user) => ({
    type: types.UPDATE_USER,
    user
});

function fetchLoginAsync(loginModel) {
    return async (dispatch) => {
        try {
            const response = await fetchLogin(loginModel);
            const data = await response.json();

            dispatch(login(data));

        } catch (e) {
            console.log('failed to log in.');
            console.log(e);
        }
    };
}

function fetchLogoutAsync( ) {
    return async (dispatch) => {
        try {
            const response = await fetchLogout();
            const data = await response.json();

            if (data) dispatch(logout());

        } catch (e) {
            console.log('failed to log out.');
            console.log(e);
        } 
    }
}

const shouldLogin = (state) => {
    const { user } = state;

    return (!user) || (user !== {}) || (user.userId < 0);
}

const shouldLogout = (state) => {
    const { user } = state;

    return (user) && (user.userId > 0);

}

export function loginIfNeeded(loginModel) {
    return (dispatch, getState) => {
        return shouldLogin(getState()) ? dispatch(fetchLoginAsync(loginModel)) : Promise.resolve();
    }
}

export function logoutIfNeeded() {
    return (dispatch, getState) => {
        return shouldLogout(getState()) ? dispatch(fetchLogoutAsync()) : Promise.resolve();
    }
}