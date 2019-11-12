import React from 'react';

const BtnGroup = ({ skus, skuPicked, pickSize }) => {
    if (!skus || skus.length === 0) return null;
    
    return (
        <div className="btn-group-sm btn-group-toggle flex-wrap mt-3 " data-toggle="buttons">
            {skus.map(sku => {
                const classes = `btn rounded-0 shadow-none ${sku.quantity === 0
                        ? 'btn-secondary'
                    : ( Object.keys(skuPicked).length > 0 && skuPicked.skuId === sku.skuId ? 'btn-info' : 'btn-primary')}`;

                return (
                    <button key={sku.skuId}
                            className={classes}
                            disabled={sku.quantity === 0}
                            onClick={() => pickSize(sku)}>
                        <input type="radio" autoComplete="off" disabled/> {sku.size}
                    </button>
                );
                })
            }
        </div>
    );
};

export default BtnGroup;

