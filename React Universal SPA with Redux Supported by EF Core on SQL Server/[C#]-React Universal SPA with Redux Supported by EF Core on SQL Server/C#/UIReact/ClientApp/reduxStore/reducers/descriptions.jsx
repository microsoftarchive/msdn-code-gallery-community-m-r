import types from '../../constants/actionTypes';

const descriptions = (state = {}, action) => {
    const { type, styleId, descriptions: descs } = action;

    switch (type) {
        case types.ADD_DESCRIPTIONS:
            return { ...state, [styleId]: descs };
        default:
            return state;
    }
}

export default descriptions;