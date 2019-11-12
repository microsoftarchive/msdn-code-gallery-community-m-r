import types from '../../constants/actionTypes';

const user = (state = {}, action) => {
    const { type, user } = action;
    switch (type) {
        case types.LOGIN:
        case types.LOGOUT:
        case types.UPDATE_USER:
            return (!user) ? {} : {...user};
        default:
            return state;
    }
}

export default user;