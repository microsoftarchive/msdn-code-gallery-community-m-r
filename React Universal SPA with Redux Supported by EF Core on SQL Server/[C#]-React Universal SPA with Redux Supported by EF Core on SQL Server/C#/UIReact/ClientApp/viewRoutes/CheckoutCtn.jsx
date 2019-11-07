import { connect } from 'react-redux';
import { push } from 'connected-react-router'

import Checkout from './checkout/Checkout';
import { getCartLineArr } from '../reduxStore/selectors/getCartLineArray';
import getProvincesIfNeeded from '../reduxStore/actions/getProvinces';
import { mapArrToObj, mapArrToObjNoId } from '../reduxStore/helpers/arrayToObject'
import { addSkusOfStyles } from '../reduxStore/actions/getSkus';
import { getStyleStates } from '../reduxStore/actions/getStyleStates';
import getSkusOverStocks from '../reduxStore/actions/getSkusOverStock';
import { clearCart } from '../reduxStore/actions/handleCart';
import sortSkusBySize from '../reduxStore/helpers/sortSkusBySize';
import routePaths from '../constants/routes';

const mapStateToProps = (state) => {
    return {
        totalValue: state.cart.totalValue,
        lines: getCartLineArr(state),
        user: state.user,
        provinces: state.provinces
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        getProvinces: () => {
            dispatch(getProvincesIfNeeded());
        },
        updateSkus: (skus) => {
            const skusObj = mapArrToObjNoId(skus, 'styleId');

            Object.keys(skusObj).map(styleId => {
                var skusToSort = [...skusObj[styleId]];

                skusObj[styleId] = [...sortSkusBySize(skusToSort)];
            });

            dispatch(addSkusOfStyles(skusObj));
        },
        updateStyleStates: (styleStates) => {
            const styleStatesObj = mapArrToObj(styleStates, 'styleId');

            dispatch(getStyleStates(styleStatesObj));
        },
        updateSkusOverStock: (skusOverStock) => {
            dispatch(getSkusOverStocks(skusOverStock));
        },
        clearCart: () => {
            dispatch(clearCart());
        },
        navToCart: () => {
            dispatch(push(routePaths.cart));
        }
    };
};

const CheckoutCtn = connect(mapStateToProps, mapDispatchToProps)(Checkout);

export default CheckoutCtn;


