import { createSelector } from 'reselect';

const user = (state) => state.user;

const orders = (state) => state.ordersByUsers;

const getOrders = (u, os) => u && Object.keys(u).length > 0 && u.userId > 0
        ? os[u.userId]
        : [];

const getOrdersByUser = createSelector(
    [user, orders],
    getOrders
);

export default getOrdersByUser;