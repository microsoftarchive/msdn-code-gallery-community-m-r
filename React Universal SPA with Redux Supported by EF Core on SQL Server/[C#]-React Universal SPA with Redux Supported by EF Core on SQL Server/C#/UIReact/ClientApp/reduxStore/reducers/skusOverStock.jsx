import types from '../../constants/actionTypes';

const skusOverStock = (state=[], action) => {
    switch (action.type) {
        case types.GET_SKUS_OVER_STOCKS:
            return [...state, ...action.skusOverStocks];
        default:
            return state;
    }
}

export default skusOverStock;