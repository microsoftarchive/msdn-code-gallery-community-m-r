import types from '../../constants/actionTypes';

const ordersByUsers = (state = {}, action) => {
    switch (action.type) {
        case types.GET_ORDERS:
        {
            const { userId, orders } = action;
            return { ...state, [userId]: orders }
            }
        default:
            return state;
    }
}

export default ordersByUsers;