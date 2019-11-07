import { createSelector } from 'reselect';

const linesObj = (state) => state.cart.lines;
const skuObj = (state) => state.skus;

const getLineArrFromObj = (obj) => Object.keys(obj).map(id => obj[id]);

const getSkuArrFromObj = (skuObject, lineArray) => {
    if (lineArray.some(line => typeof skuObject[line.styleId] === 'undefined')) return [];

    return lineArray.map(line => skuObject[line.styleId].find(sku => sku.skuId === line.skuId));
}
        
export const getCartLineArr = createSelector(
    [linesObj],
    getLineArrFromObj
);

export const getSkuArray = createSelector(
    [skuObj, getCartLineArr],
    getSkuArrFromObj
);

