import types from '../../constants/actionTypes';
import defaultReduxValues from '../../constants/defaultReduxValues';

const selectedCategoryId = (state = defaultReduxValues.selectedCategoryId, action) => {
    const { type, categoryId } = action;

    switch (type) {
    case types.SELECT_CATEGORY:
        return categoryId;
    default:
        return state;
    }
}

export default selectedCategoryId;