import types from '../../constants/actionTypes';

export const addSkus = (styleId, skus) => ({
    type: types.ADD_SKUS,
    styleId,
    skus
});

export const addSkusOfStyles = (skus) => ({
    type: types.ADD_SKUS_OF_STYLES,
    skus
});





