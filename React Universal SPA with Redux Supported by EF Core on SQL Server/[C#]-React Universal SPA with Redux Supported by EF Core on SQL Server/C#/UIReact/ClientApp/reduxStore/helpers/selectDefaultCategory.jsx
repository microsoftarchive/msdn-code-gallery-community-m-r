import defaultReduxValues from '../../constants/defaultReduxValues';

export default function selectDefaultCategory (selectedCategoryId, selectCategory) {
    const defaultCategoryId = defaultReduxValues.selectedCategoryId;

    if (selectedCategoryId !== defaultCategoryId) selectCategory(defaultCategoryId);
}