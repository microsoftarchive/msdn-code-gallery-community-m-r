import types from '../../constants/actionTypes';

const provinces = (state = [], action) => {
    const { type, provinces: pr } = action;

    switch (type) {
        case types.GET_PROVINCES:
            return [...state, ...pr];
        default:
            return state;
    }
}

export default provinces;