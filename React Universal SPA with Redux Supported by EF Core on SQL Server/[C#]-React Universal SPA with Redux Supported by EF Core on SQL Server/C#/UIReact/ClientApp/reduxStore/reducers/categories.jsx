import types from '../../constants/actionTypes';

const categories = (state = {}, action) => {
    const { type, categories: categoryObj } = action;

    switch (type) {
        case types.GET_CATEGORIES:
            return (typeof (state.keys) === 'undefined')
                ? { ...categoryObj }
                : state;
    default:
        return state;
    }
}

export default categories;