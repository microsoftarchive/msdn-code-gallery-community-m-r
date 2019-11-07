import types from '../../constants/actionTypes';

const getSelectedCategoryId = (categoryId) => ({
    type: types.SELECT_CATEGORY,
    categoryId
});

export default getSelectedCategoryId;