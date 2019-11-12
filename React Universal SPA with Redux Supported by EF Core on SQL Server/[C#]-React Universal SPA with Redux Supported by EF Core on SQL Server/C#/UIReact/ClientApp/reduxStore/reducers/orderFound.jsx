import types from '../../constants/actionTypes';

const orderFound = (state = {}, action) => {
    switch (action.type) {
        case types.UPDATE_FIND_ORDER:
            return { ...action.orderFound };
        default:
            return state;
    }
}

export default orderFound;