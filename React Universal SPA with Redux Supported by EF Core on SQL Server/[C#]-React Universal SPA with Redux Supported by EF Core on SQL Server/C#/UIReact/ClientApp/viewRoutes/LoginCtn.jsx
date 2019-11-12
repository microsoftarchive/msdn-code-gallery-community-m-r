import { connect } from 'react-redux';

import { loginIfNeeded } from '../reduxStore/actions/getUser';
import Login from './login/Login';
import getSelectedCategoryId from '../reduxStore/actions/getSelectedCategoryId';

const mapStateToProps = (state) => {
    return {
        selectedCategoryId: state.selectedCategoryId,
        user: state.user
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        login: (loginModel) => {
            dispatch(loginIfNeeded(loginModel));
        }, 
        selectCategory: (categoryId) => {
            dispatch(getSelectedCategoryId(categoryId));
        }
    };
};

const LoginCtn = connect(mapStateToProps, mapDispatchToProps)(Login);

export default LoginCtn;
