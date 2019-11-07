/* global Promise: false */
/* global path: false */

import { createServerRenderer } from 'aspnet-prerendering';
import * as React from 'react';
import { renderToString } from 'react-dom/server';
import { Provider } from 'react-redux';
import { ConnectedRouter, replace } from 'connected-react-router'
import { createMemoryHistory } from 'history';

import configureStore from './reduxStore/configureStore';
import getCategoriesStylesAsync from './reduxStore/actions/getCategoriesStyles';
import App from './viewRoutes/App';

export default createServerRenderer(params => {
    return new Promise((resolve, reject) => {

        const initialState = {};

        const history = createMemoryHistory();

        const store = configureStore(initialState, history);

        store.dispatch(replace(params.location));

        store.dispatch(getCategoriesStylesAsync())
            .then(() => {
                const app = (
                    <Provider store={store}>
                        <ConnectedRouter history={history}>
                            <App />
                        </ConnectedRouter>
                    </Provider>
                );

                const html = renderToString(app);

                params.domainTasks.then(() => {
                        resolve({
                            html: html,
                            globals: { initialReduxState: store.getState() }
                        });
                    },
                    reject);
            });
    });
});