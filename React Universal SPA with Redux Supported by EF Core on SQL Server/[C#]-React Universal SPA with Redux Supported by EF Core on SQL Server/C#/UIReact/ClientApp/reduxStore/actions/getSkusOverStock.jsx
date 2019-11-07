import types from '../../constants/actionTypes';

const getSkusOverStocks = (skusOverStocks) => ({
    type: types.GET_SKUS_OVER_STOCKS,
    skusOverStocks
});

export default getSkusOverStocks;