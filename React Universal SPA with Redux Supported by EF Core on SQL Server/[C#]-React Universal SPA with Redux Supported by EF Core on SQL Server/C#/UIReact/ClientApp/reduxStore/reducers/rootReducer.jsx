import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';

import categories from './categories';
import { populars, clearances } from './stylesHome';
import styles from './styles';
import styleStates from './styleStates';
import selectedCategoryId from './selectedCategoryId';
import styleExtras from './styleExtras';
import skus from './skus';
import descriptions from './descriptions';
import specs from './specs';
import reviews from './reviews';
import cart from './cart';
import provinces from './provinces';
import skusOverStock from './skusOverStock';
import ordersByUsers from './ordersByUsers';
import orderDetails from './orderDetails';
import orderFound from './orderFound';
import resultsFiltered from './resultsFiltered';
import isLoggedInInitiallyChecked from './isLoggedInInitiallyChecked';
import user from './user';

export default function rootReducer(history) {
    return combineReducers({
        categories,
        populars,
        clearances,
        styles,
        styleStates,
        styleExtras,
        selectedCategoryId,
        skus,
        descriptions,
        specs,
        reviews,
        cart,
        provinces,
        skusOverStock,
        ordersByUsers,
        orderDetails,
        orderFound,
        resultsFiltered,
        isLoggedInInitiallyChecked,
        user,
        router: connectRouter(history)
    });
}