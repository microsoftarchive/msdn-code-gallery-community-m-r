import { fetch } from 'domain-task';
import '@babel/polyfill';
import { stringify } from 'qs';

import { getClientApiUrl, apiPaths } from '../../constants/webApi';
import types from '../../constants/actionTypes';
import { getOptionsNoCredentials } from '../helpers/postOptions';

function postOrder(orderModel) {
    return fetch(getClientApiUrl(apiPaths.postOrder), getOptionsNoCredentials(orderModel));
}

function fetchOrdersByUserId(userId) {
    const search = stringify({
         userId: userId
    });

    return fetch(`${getClientApiUrl(apiPaths.getOrdersByUserId)}?${search}`, { credentials: 'include' });
}

function fetchOrderDetailById(customerOrderId) {
    const search = stringify({
        customerOrderId: customerOrderId
    });

    return fetch(`${getClientApiUrl(apiPaths.getOrderDetailById)}?${search}`, { credentials: 'include' });
}

function fetchOrderDetailByIdEmail(customerOrderId, email) {
    const search = stringify({
        customerOrderId: customerOrderId,
        email: email
    });

    return fetch(`${getClientApiUrl(apiPaths.getOrderDetailByEmailId)}?${search}`);
}

export const getOrdersByUserId = (userId, orders) => ({
    type: types.GET_ORDERS,
    userId,
    orders
});

export const getOrderDetail= (order) => ({
    type: types.GET_ORDER_DETAIL,
    order
});

export const updateOrderFound = (orderFound) => ({
    type: types.UPDATE_FIND_ORDER,
    orderFound
});

 async function postOrderAsync(orderModel) {
    try {
        const response = await postOrder(orderModel);
        const data = await response.json();

        return data;

    } catch (e) {
        return e.message;
    } 
}

function getOrdersByUserIdAsync(userId) {
    return async (dispatch) => {
        try {
            const response = await fetchOrdersByUserId(userId);
            const data = await response.json();

            dispatch(getOrdersByUserId(userId, data));

        } catch (e) {
            console.log('failed to get orders for you');
            console.log(e);
        }
    }
}

function getOrderDetailByIdAsync(customerOrderId) {
    return async (dispatch) => {
        try {
            const response = await fetchOrderDetailById(customerOrderId);
            const data = await response.json();

            dispatch(getOrderDetail(data));

        } catch (e) {
            console.log(`failed to get order ${customerOrderId}`);
            console.log(e);
        } 
    }
}

function getOrderDetailByIdEmailAsync(customerOrderId, email) {
    return async (dispatch) => {
        try {
            const response = await fetchOrderDetailByIdEmail(customerOrderId, email);
            if (response.status === 204) {
                const orderFound = {
                    customerOrderId: 'failed',
                    email: ''
                }

                dispatch(updateOrderFound(orderFound));

                return;
            }

            const data = await response.json();

            const orderFound = {
                customerOrderId: customerOrderId,
                email: email
            };
            
            dispatch(updateOrderFound(orderFound));

            dispatch(getOrderDetail(data));

        } catch (e) {
            console.log(`failed to get order ${customerOrderId}`);
            console.log(e);
        } 
    }
}

const shouldFetchOrdersByUserId = (state, userId) => {
    const { ordersByUsers } = state;

    return !Object.keys(ordersByUsers).includes(userId);
}

const shouldFetchOrderDetailById = (state, customerOrderId) => {
    const { orderDetails } = state;

    return !Object.keys(orderDetails).includes(customerOrderId);
}

const shouldFetchOrderDetailByIdEmail = (state, customerOrderId, email) => {
    const { orderDetails } = state;

    return !(Object.keys(orderDetails).includes(customerOrderId) && orderDetails.email === email);
}

export function executePostOrder(orderModel) {
    return postOrderAsync(orderModel);
}

export function getOrdersByUserIdIfNeeded(userId) {
    return (dispatch, getState) => {
        return shouldFetchOrdersByUserId(getState(), userId)
            ? dispatch(getOrdersByUserIdAsync(userId))
            : Promise.resolve(); 
    }
}

export function getOrderDetailByIdIfNeeded(customerOrderId) {
    return (dispatch, getState) => {
        return shouldFetchOrderDetailById(getState(), customerOrderId)
            ? dispatch(getOrderDetailByIdAsync(customerOrderId))
            : Promise.resolve();
    }
}

export function getOrderDetailByIdEmailIfNeeded(customerOrderId, email) {
    return (dispatch, getState) => {
        return shouldFetchOrderDetailByIdEmail(getState(), customerOrderId, email)
            ? dispatch(getOrderDetailByIdEmailAsync(customerOrderId, email))
            : Promise.resolve();
    }
}

