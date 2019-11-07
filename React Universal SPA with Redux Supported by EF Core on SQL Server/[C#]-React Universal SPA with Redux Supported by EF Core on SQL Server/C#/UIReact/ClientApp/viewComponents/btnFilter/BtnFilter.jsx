import React from 'react';

const BtnFilter = ({ itemId, itemName, itemCount, getClasses, processStyles }) => (
    <button value={itemId} className={getClasses} onClick={processStyles}>
        {itemName} ({itemCount})
    </button>
);

export default BtnFilter;