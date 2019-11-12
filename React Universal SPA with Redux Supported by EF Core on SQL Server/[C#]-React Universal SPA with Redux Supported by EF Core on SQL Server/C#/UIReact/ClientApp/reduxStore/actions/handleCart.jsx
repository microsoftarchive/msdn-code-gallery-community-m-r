import types from '../../constants/actionTypes';

export const addLine = (style, sku, quantity) => ({
    type: types.ADD_LINE,
    style,
    sku,
    quantity
});

export const updateQuantity = (skuId, quantity) => ({
    type: types.UPDATE_QUANTITY,
    skuId,
    quantity
});

export const removeLine = (skuId) => ({
    type: types.REMOVE_LINE,
    skuId
});

export const clearCart = () => ({
    type: types.CLEAR_CART
});

export const updateCart = (cart) => ({
    type: types.UPDATE_CART,
    cart
});