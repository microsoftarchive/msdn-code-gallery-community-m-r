import { connect } from 'react-redux';

import BtnGroup from '../viewComponents/btnGroup/BtnGroup';

const mapStateToProps = (state, ownProps) => {
    return {
        skus: state.skus[ownProps.styleId]
    };
};

const BtnGroupCtn = connect(mapStateToProps)(BtnGroup);

export default BtnGroupCtn;

