import { connect } from 'react-redux';

import NavbarList from '../viewComponents/navbarList/NavbarList';
import getCategoryArr from '../reduxStore/selectors/getCategoryArr';

const mapStateToProps = (state) => {
    return {
        items: getCategoryArr(state),
        selectedId: state.selectedCategoryId
    };
};

const NavbarListCtn = connect(mapStateToProps)(NavbarList);

export default NavbarListCtn;