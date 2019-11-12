import types from '../../constants/actionTypes';

const specs = (state = {}, action) => {
    const { type, styleId, specs:sp  } = action;

    switch (type) {
    case types.ADD_SPECS:
        return { ...state, [styleId]: sp };
    default:
        return state;
    }
}

export default specs;