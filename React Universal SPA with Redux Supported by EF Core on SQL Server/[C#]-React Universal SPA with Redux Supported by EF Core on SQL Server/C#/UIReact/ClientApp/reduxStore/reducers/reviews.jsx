import types from '../../constants/actionTypes';

const reviews = (state = {}, action) => {
    const { type, styleId } = action;

    switch (type) {
        case types.GET_REVIEWS:
                return { ...state, [styleId]: action.reviews };
        case types.ADD_REVIEW:
            return {...state, [styleId]: [action.review, ...state[styleId]]}
        default:
            return state;
    }
}

export default reviews;