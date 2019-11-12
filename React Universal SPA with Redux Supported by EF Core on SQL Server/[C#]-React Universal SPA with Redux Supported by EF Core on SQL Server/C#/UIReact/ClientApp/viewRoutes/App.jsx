'use strict';

/* global import: false, Promise: false */

import React from 'react';
import { Route, Switch } from 'react-router-dom';

import ShopHeader from '../viewComponents/shopHeader/ShopHeader';
import StylesCtn from './StylesCtn';
import HomeCtn from './HomeCtn';
import SkisCtn from './SkisCtn';
import {
    AsyncCheckoutCtn, Async404, AsyncCartCtn, AsyncOrderHistoryCtn, AsyncOrderInquiryCtn,
    AsyncOrderDetailCtn, AsyncLogin
} from './lazyLoad';
import routePaths from '../constants/routes';


const ShopBody = () => (
        <Switch>
            <Route exact path={routePaths.home} component={HomeCtn}/>
            <Route path={routePaths.cart} component={AsyncCartCtn}/>
            <Route path={routePaths.orderHistory} component={AsyncOrderHistoryCtn} />
            <Route path={routePaths.orderInquiry} component={AsyncOrderInquiryCtn} />
            <Route path={routePaths.orderDetail} component={AsyncOrderDetailCtn} />
            <Route path={routePaths.checkout} component={AsyncCheckoutCtn}/>
            <Route path={routePaths.login} component={AsyncLogin}/>
            <Route path="/category/:categoryName" component={StylesCtn}/>
            <Route path="/skis/:styleName/:styleId" component={SkisCtn} />
            <Route component={Async404}/>
        </Switch>
);

const App = () => (
    <div>
        <ShopHeader/>

        <ShopBody />
    </div>
);

export default App;
