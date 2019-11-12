import { connect } from 'react-redux';

import Home from '../viewComponents/home/Home';
import getSelectedCategoryId from '../reduxStore/actions/getSelectedCategoryId';

const mapStateToProps = (state) => {
    return {
        selectedCategoryId: state.selectedCategoryId
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        selectCategory: (categoryId) => {
            dispatch(getSelectedCategoryId(categoryId));
        }
    };
};

const HomeCtn = connect(mapStateToProps, mapDispatchToProps)(Home);

export default HomeCtn;

