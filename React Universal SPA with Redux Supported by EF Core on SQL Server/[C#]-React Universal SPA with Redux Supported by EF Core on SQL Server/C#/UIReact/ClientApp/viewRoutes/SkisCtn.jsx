import { connect } from 'react-redux';

import getSkisIfNeeded from '../reduxStore/actions/getStyleExtras';
import Skis from '../viewComponents/skis/Skis';

const mapDispatchToProps = (dispatch) => {
    return {
        getSkis: (styleId) => {
            dispatch(getSkisIfNeeded(styleId));
        }
    };
}

const SkisCtn = connect(null, mapDispatchToProps)(Skis);

export default SkisCtn;