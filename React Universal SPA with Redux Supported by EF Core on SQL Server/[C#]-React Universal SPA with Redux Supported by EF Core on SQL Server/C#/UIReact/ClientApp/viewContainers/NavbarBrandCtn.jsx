import { connect } from 'react-redux';

import NavbarBrand from '../viewComponents/navbarBrand/NavbarBrand';
import getSelectedCategoryId from '../reduxStore/actions/getSelectedCategoryId';
import defaultReduxValues from '../constants/defaultReduxValues';

const mapDispatchToProps = (dispatch) => {
    return {
        selectCategoryId: () => {
            dispatch(getSelectedCategoryId(defaultReduxValues.selectedCategoryId));
        }
    };
};

const NavbarBrandCtn = connect(null, mapDispatchToProps)(NavbarBrand);

export default NavbarBrandCtn;
