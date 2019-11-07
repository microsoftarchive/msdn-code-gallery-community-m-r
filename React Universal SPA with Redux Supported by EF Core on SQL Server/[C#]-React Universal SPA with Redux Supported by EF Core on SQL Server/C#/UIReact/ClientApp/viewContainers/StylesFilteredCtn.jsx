import { connect } from 'react-redux';

import StyleList from '../viewComponents/styleList/StyleList';

const getFiltered = (styleIds, obj) =>
    styleIds.map(styleId => obj[styleId]);

const mapStateToProps = (state, ownProps) => {
    return {
        styles: getFiltered(ownProps.styleIds, state.styles),
        states: getFiltered(ownProps.styleIds, state.styleStates)
    };
};

const StylesFilteredCtn = connect(mapStateToProps)(StyleList);

export default StylesFilteredCtn;