import { createSelector } from 'reselect';

const categoryObj = (state) => state.categories;

const getCategoryArrFromObj = (obj) => Object.keys(obj).map(id => obj[id]);

const getCategoryArr = createSelector(
    [categoryObj],
    getCategoryArrFromObj
);

export default getCategoryArr;
