import React from 'react';

import CartLine from '../../viewContainers/cartLine/CartLine';

const CartLines = ({ lines, skusOverStock, updateQuantity, removeLine }) => (
    <tbody>
        {lines.map(line => {

            return (<CartLine key={line.skuId} line={line} skusOverStock={skusOverStock} updateQuantity={updateQuantity}
                                  removeLine={() => removeLine(line.skuId)}/>);
            }
        )}
    </tbody>
);

export default CartLines;