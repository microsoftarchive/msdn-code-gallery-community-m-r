import { connect } from 'react-redux';

import checkLoginStatusIfNeeded from '../reduxStore/actions/getIsLoggedInInitiallyChecked';
import { logoutIfNeeded } from '../reduxStore/actions/getUser';
import LoginOut from '../viewRoutes/loginOut/LoginOut';

const mapStateToProps = (state) => {
    return {
        initiallyChecked: state.isLoggedInInitiallyChecked,
        user: state.user
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        check: () => {
            dispatch(checkLoginStatusIfNeeded());
        },
        logout: () => {
            dispatch(logoutIfNeeded());
        }
    }
}

const LoginOutCtn = connect(mapStateToProps, mapDispatchToProps)(LoginOut);

export default LoginOutCtn;
