import types from '../../constants/actionTypes';

const styleExtras = (state = {}, action) =>
{
    const { type, styleExtra } = action;

    switch (type) {
        case types.ADD_STYLEEXTRA:
            return { ...state, [styleExtra.styleId]: styleExtra }
    default:
        return state;
    }
};

export default styleExtras;

