import React from 'react';

import ImgLink from '../imgLink/ImgLink';
import StyleGeneral from '../styleGeneral/StyleGeneral';
import StyleState from '../styleState/StyleState';
import routePaths from '../../constants/routes';

import './styleItem.scss';

const StyleItem = ({ ski, state}) => {
    const { styleId, styleName, imageSmall } = ski;
    const navTo = `${routePaths.skis}/${styleName}/${styleId}`;
    const source = `..${imageSmall}`;

    return (
        <div name="styleItem" className="col-md-4">
            <ImgLink navTo={navTo} source={source} />
            <StyleGeneral ski={ski} navTo={navTo} />
            <StyleState state={state}/>
        </div>
    );
}

export default StyleItem;