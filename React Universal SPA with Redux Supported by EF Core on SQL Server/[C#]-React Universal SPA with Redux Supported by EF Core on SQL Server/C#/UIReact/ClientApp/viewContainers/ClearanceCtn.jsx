import { connect } from 'react-redux';

import StyleList from '../viewComponents/styleList/StyleList';
import { getClearanceArr, getClearanceStateArr } from '../reduxStore/selectors/getStyleArr';

const mapStateToProps = (state) => {
    return {
        styles: getClearanceArr(state),
        states: getClearanceStateArr(state)
    };
};

const ClearanceCtn = connect(mapStateToProps)(StyleList);

export default ClearanceCtn;