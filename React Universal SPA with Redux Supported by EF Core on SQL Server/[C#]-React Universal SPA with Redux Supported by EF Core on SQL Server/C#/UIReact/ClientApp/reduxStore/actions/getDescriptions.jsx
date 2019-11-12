import types from '../../constants/actionTypes';

const getDescriptions = (styleId, descriptions) => ({
    type: types.ADD_DESCRIPTIONS,
    styleId,
    descriptions
});

export default getDescriptions;