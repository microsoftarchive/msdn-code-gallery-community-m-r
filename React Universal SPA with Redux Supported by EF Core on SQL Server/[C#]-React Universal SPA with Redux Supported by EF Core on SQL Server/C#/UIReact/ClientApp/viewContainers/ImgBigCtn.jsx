import { connect } from 'react-redux';

import ImgBig from '../viewComponents/imgBig/ImgBig';

const mapStateToProps = (state, ownProps) => {
    const styleExtra = state.styleExtras[ownProps.styleId];

    return {
        source: (styleExtra) ? styleExtra.imageBig : null
    };
};

const ImgBigCtn = connect(mapStateToProps)(ImgBig);

export default ImgBigCtn;

