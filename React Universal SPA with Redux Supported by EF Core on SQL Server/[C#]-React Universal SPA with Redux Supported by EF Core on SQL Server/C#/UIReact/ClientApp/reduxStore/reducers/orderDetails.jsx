import types from '../../constants/actionTypes';

const orderDetails = (state = {}, action) => {
    switch (action.type) {
        case types.GET_ORDER_DETAIL:
        {
            const { order } = action;

            if (order) {
                return { ...state, [order.customerOrderId]: order };
            }

            return state;
        }
        
        default:
            return state;
    }
}

export default orderDetails;