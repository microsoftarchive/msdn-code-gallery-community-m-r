import { connect } from 'react-redux';

import StyleList from '../viewComponents/styleList/StyleList';
import { getPopularArr, getPopularStateArr } from '../reduxStore/selectors/getStyleArr';

const mapStateToProps = (state) => {
    return {
        styles: getPopularArr(state),
        states: getPopularStateArr(state)
    };
};

const PopularCtn = connect(mapStateToProps)(StyleList);

export default PopularCtn;
