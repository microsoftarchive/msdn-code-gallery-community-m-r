import types from '../../constants/actionTypes';

const resultsFiltered = (state = {}, action) => {
    switch (action.type) {
        case types.GET_STYLES_FILTERED:
        {
            const { queryStr, result } = action;

            return { ...state, [queryStr]: result };
        }
        
        default:
            return state;
    }
}

export default resultsFiltered;