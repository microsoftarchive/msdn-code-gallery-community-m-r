import types from '../../constants/actionTypes';

const isLoggedInInitiallyChecked = (state = false, action) => {
    const { type, isChecked } = action;

    switch (type) {
        case types.UPDATE_ISLOGGEDINCHECHED:
            return isChecked;
        default:
            return state;
    }
};

export default isLoggedInInitiallyChecked;