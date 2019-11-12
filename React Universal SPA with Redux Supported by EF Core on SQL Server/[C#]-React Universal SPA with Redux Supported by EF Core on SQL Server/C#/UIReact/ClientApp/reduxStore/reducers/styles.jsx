import types from '../../constants/actionTypes';

const styles = (state = {}, action) => {

    switch (action.type) {
        case types.ADD_STYLES:
        {
            const { styles: styleObj } = action;

            return { ...styleObj, ...state };
        }
        case types.ADD_STYLE:
        {
            const { style } = action;

            return { ...state, [style.styleId]: style };
        }
            
    default:
        return state;
    }
}

export default styles;