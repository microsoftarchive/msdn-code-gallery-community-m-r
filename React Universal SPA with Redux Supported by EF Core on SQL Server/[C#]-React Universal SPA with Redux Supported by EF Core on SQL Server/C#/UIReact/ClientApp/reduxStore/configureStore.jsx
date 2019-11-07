/* global module: false, require: false */

import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import { routerMiddleware } from 'connected-react-router'

import rootReducer from './reducers/rootReducer';

export default function configureStore(initialState, history, composeEnhancers) {

    const historyMiddleware = routerMiddleware(history);

    const middlewares = [historyMiddleware, thunk];

    const middlewareEnhancer = applyMiddleware(...middlewares);
    const enhancer = ( !composeEnhancers )
        ? middlewareEnhancer
        : composeEnhancers(middlewareEnhancer);

    const store = createStore(rootReducer(history), initialState,
        enhancer);

    if (module.hot) {
        module.hot.accept('./reducers/rootReducer',
            () => {
                store.replaceReducer(rootReducer(history));
            }
        );
    }

    return store;
}

