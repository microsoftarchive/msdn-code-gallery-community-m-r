import types from '../../constants/actionTypes';

const skus = (state = {}, action) => {
    switch (action.type) {
        case types.ADD_SKUS:
            {
                const { styleId, skus: skuArr } = action;
                return { ...state, [styleId]: skuArr };
            }
        case types.ADD_SKUS_OF_STYLES:
            {
                const { skus: skusObj } = action;
                return { ...state, ...skusObj }
            }   
        default:
            return state;
    }
}

export default skus;