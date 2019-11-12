import React from 'react';
import { Link } from 'react-router-dom';

import routePaths from '../../constants/routes';

import './loginOut.scss';

class LoginOut extends React.Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
       
        if (typeof window === 'undefined') return;

        if (!this.props.initiallyChecked) this.props.check();
    }

    render() {
        const user = this.props.user;
        
        return (
            <li className="nav-item">
                {user && user.userId > 0
                    ? <div className="nav-link text-white text-right cus-hand " onClick={() => this.props.logout()}>Log Out</div>
                    : <Link to={routePaths.login} className="nav-link text-white text-right">Log In </Link>
                } 
            </li>
        );
    }
}

export default LoginOut;
