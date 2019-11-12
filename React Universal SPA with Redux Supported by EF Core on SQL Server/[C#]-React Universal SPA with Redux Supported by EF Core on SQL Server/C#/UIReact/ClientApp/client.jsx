/* global module: false */
/* global hot: false */

import React from 'react';
import ReactDOM from 'react-dom';
import { compose } from 'redux';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'connected-react-router'
import { createBrowserHistory } from 'history';

import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import '@fortawesome/fontawesome-free/css/all.min.css';

import configureStore from './reduxStore/configureStore';
import App from './viewRoutes/App';

if (module.hot) {
    module.hot.accept('./viewRoutes/App', () => {
        render();
    });
}

const initialState = window.initialReduxState;

const history = createBrowserHistory();

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const store = configureStore(initialState, history, composeEnhancers);

const render = () => {
    ReactDOM.hydrate(
        <Provider store={store}>
            <ConnectedRouter history={history}>
                <App />
            </ConnectedRouter>
        </Provider>,
        document.getElementById('app')
    );
}

render();

