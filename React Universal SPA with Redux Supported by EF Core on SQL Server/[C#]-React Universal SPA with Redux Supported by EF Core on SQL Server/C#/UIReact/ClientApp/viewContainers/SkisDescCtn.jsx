import { connect } from 'react-redux';

import SkisDesc from '../viewComponents/skisDesc/SkisDesc';

const mapStateToProps = (state, ownProps) => {
    const styleId = ownProps.styleId;

    return {
        descriptions: state.descriptions[styleId]
    }
}

const SkisDescCtn = connect(mapStateToProps)(SkisDesc);

export default SkisDescCtn;