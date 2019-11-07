import { connect } from 'react-redux';

import SkisGeneral from '../viewComponents/skisGeneral/skisGeneral';
import getSelectedCategoryId from '../reduxStore/actions/getSelectedCategoryId';
import { addLine } from '../reduxStore/actions/handleCart';
import { getStyleIfNeeded } from '../reduxStore/actions/getStylesHome';

const mapStateToProps = (state, ownProps) => {
    const styleId = ownProps.styleId;

    return {
        style: state.styles[styleId],
        styleState: state.styleStates[styleId],
        selectedCategoryId: state.selectedCategoryId
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        selectCategory: (categoryId) => {
            dispatch(getSelectedCategoryId(categoryId));
        },
        addToCart: (style, sku, quantity) => {
            dispatch(addLine(style, sku, quantity));
        },
        getStyle: (styleId) => {
            dispatch(getStyleIfNeeded(styleId));
        }
};
};

const SkisGeneralCtn = connect(mapStateToProps, mapDispatchToProps)(SkisGeneral);

export default SkisGeneralCtn;



