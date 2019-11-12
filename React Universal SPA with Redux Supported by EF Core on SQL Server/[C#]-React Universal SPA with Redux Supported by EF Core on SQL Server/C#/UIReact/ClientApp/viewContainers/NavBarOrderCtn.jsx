import { connect } from 'react-redux';

import NavbarOrder from '../viewComponents/navbarOrder/NavbarOrder';

const mapStateToProps = (state) => ({
    user: state.user
});

const NavbarOrderCtn = connect(mapStateToProps)(NavbarOrder);

export default NavbarOrderCtn;