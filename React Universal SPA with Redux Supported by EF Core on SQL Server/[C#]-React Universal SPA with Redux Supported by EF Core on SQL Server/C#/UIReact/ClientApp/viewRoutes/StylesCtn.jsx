import { connect } from 'react-redux';
import { push } from 'connected-react-router'

import getSelectedCategoryId from '../reduxStore/actions/getSelectedCategoryId';
import Styles from '../viewComponents/styles/styles';
import { getStylesFilteredIfNeeded } from '../reduxStore/actions/filterStyles';

const getQueryStr = (location) => {
    return location.search.substring(1);
}

const mapStateToProps = (state, ownProps) => {
    return {
        selectedId: state.selectedCategoryId,
        results: state.resultsFiltered[getQueryStr(ownProps.location)]
    };
};

const mapDispatchToProps = (dispatch, ownProps) => (
    {
        selectItem: (categoryId) => {
            dispatch(getSelectedCategoryId(categoryId));
        },
        fetchData: () => {
            dispatch(getStylesFilteredIfNeeded(getQueryStr(ownProps.location)));
        },
        updateQuery: (location) => {
            dispatch(push(location));
        }
    }
);

const StylesCtn = connect(mapStateToProps, mapDispatchToProps)(Styles);

export default StylesCtn;
