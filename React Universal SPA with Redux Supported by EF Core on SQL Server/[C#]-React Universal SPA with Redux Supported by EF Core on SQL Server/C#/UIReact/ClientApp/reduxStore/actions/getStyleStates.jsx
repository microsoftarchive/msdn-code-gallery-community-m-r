import types from '../../constants/actionTypes';

export const getStyleStates = (styleStates) => ({
    type: types.ADD_STYLESTATES,
    styleStates
});

export const updateStyleState = (styleId, styleState) => ({
    type: types.UPDATE_STYLESTATE,
    styleId,
    styleState
});



