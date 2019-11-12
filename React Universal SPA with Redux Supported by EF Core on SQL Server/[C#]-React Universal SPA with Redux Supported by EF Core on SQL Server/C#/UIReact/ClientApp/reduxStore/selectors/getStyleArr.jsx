import { createSelector } from 'reselect';

const popularIds = (state) => state.populars;
const clearanceIds = (state) => state.clearances;

const styles = (state) => state.styles;
const styleStates = (state) => state.styleStates;

const getStyleArrFromObj = (ids, styleObj) => ids.map(id => styleObj[id]);

const getStyleStateArrFromObj = (ids, stateObj) => ids.map(id => stateObj[id]);

export const getPopularArr = createSelector(
    [popularIds, styles],
    getStyleArrFromObj
);

export const getClearanceArr = createSelector(
    [clearanceIds, styles],
    getStyleArrFromObj
);

export const getPopularStateArr = createSelector(
    [popularIds, styleStates],
    getStyleStateArrFromObj
);

export const getClearanceStateArr = createSelector(
    [clearanceIds, styleStates],
    getStyleStateArrFromObj
);

