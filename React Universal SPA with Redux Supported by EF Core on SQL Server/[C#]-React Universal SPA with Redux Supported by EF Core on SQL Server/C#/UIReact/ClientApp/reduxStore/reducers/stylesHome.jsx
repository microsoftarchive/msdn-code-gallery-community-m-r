import types from '../../constants/actionTypes';

export const populars = (state = [], action) => {
    const { type, populars: popularIds } = action;

    switch (type) {
    case types.GET_POPULARS:
        return [...popularIds];
    default:
        return state;
    }
}

export const clearances = (state = [], action) => {
    const { type, clearances: clearanceIds } = action;

    switch (type) {
    case types.GET_CLEARANCE:
        return [...clearanceIds];
    default:
        return state;
    }
}