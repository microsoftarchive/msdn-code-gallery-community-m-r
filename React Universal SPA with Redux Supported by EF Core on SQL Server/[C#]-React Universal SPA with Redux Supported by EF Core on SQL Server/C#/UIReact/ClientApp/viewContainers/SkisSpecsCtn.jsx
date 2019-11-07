import { connect } from 'react-redux';

import getSpecsIfNeeded from '../reduxStore/actions/getSpecs';
import SkisSpecs from '../viewComponents/skisSpecs/SkisSpecs';

const mapStateToProps = (state, ownProps) => {
    const styleId = ownProps.styleId;

    return {
        specs: state.specs[styleId]
    };
}

const mapDispatchToProps = (dispatch) => {
    return {
        getSpecs: (styleId) => {
            dispatch(getSpecsIfNeeded(styleId));
        }
    }
}

const SkisSpecsCtn = connect(mapStateToProps, mapDispatchToProps)(SkisSpecs);

export default SkisSpecsCtn;


