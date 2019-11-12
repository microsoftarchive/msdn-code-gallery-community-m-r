import React from 'react';

import StyleItem from '../styleItem/StyleItem';

const getState = (states, styleId) => states.find(state => state.styleId === styleId);

const StyleList = ({ styles, states }) => {
    if (styles.length < 1) return null;

    return (
        <div className="row">
            {styles.map(style => (
                <StyleItem key={style.styleId} ski={style} state={getState(states, style.styleId)}/>
            ))}
        </div>
    );
}; 

export default StyleList;