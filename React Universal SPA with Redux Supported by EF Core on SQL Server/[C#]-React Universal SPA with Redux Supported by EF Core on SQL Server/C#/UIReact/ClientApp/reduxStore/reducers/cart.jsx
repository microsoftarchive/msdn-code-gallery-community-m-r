import types from '../../constants/actionTypes';

const cart = (state = { lines: {}, itemCount: 0, totalValue: 0 }, action) => {
    const { lines, itemCount, totalValue } = state;

    switch (action.type) {
        case types.ADD_LINE:
        {
            const { style, sku, quantity } = action;
            const addedValue = style.priceCurrent * quantity;

            if (Object.keys(lines).some(skuId => skuId === sku.skuId.toString())) {
                const line = { ...lines[sku.skuId] };
                line.quantity += quantity;
                line.subTotal += addedValue;

                return {
                    lines: { ...lines, [sku.skuId]: line },
                    itemCount: itemCount + quantity,
                    totalValue: totalValue + addedValue
                };
            }

            const line = {
                skuId: sku.skuId,
                styleId: style.styleId,
                skis: `${style.brandName}-${style.styleName}-${style.genderName}-${sku.size}`,
                price: style.priceCurrent,
                quantity: quantity,
                subTotal: style.priceCurrent * quantity
            };

            return {
                lines: { [sku.skuId]: line, ...lines },
                itemCount: itemCount + quantity,
                totalValue: totalValue + line.subTotal
            };
        }
        case types.UPDATE_QUANTITY:
        {
            const { skuId, quantity } = action;
            const quantityNum = Number(quantity);

            const line = { ...lines[skuId] };
            const quantityBefore = line.quantity;
            const subTotalBefore = line.subTotal;
            line.quantity = quantityNum;
            line.subTotal = line.price * quantityNum;
                
            return {
                lines: { ...lines, [skuId]: line },
                itemCount: itemCount - quantityBefore + quantityNum,
                totalValue: totalValue - subTotalBefore + line.subTotal
            };
        }    
        case types.REMOVE_LINE:
        {
                const { skuId } = action;

                const linesCopy = { ...lines };
                const lineToRemove = { ...linesCopy[skuId] };
                delete linesCopy[skuId];

                return {
                    lines: linesCopy,
                    itemCount: itemCount - lineToRemove.quantity,
                    totalValue: totalValue - lineToRemove.subTotal
                };
        }
        case types.CLEAR_CART:
            return {
                lines: {},
                itemCount: 0,
                totalValue: 0
            };
        case types.UPDATE_CART:
        {
            return { ...action.cart };
        }
        default:
            return state;
    }
}

export default cart;