import types from '../../constants/actionTypes';

const styleStates = (state = {}, action) => {

    switch (action.type) {
        case types.ADD_STYLESTATES: 
            return { ...state, ...action.styleStates };
        case types.UPDATE_STYLESTATE:
            {
                const { styleId, styleState } = action;
                const styleIdNum = Number(styleId);
                
                return (shouldUpdate(state[styleIdNum], styleState))
                    ? { ...state, [styleIdNum]: updatedState(styleIdNum, styleState) }
                    : state;
            }
        default:
            return state;
    }
}

const shouldUpdate = (fromState, fromAction) => {
    return (fromState.averageRatings === fromAction.averageRatings &&
            fromState.reviewCount === fromAction.reviewCount &&
            fromState.soldOut === fromAction.soldOut)
        ? false
        : true;
};

const updatedState = (styleId, stateFromAction) => {
    return {
        styleId: styleId,
        averageRatings: stateFromAction.averageRatings,
        reviewCount: stateFromAction.reviewCount,
        soldOut: stateFromAction.soldOut
    };
};

export default styleStates;
