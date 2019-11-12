import React from 'react';
import Loadable from 'react-loadable';

const Loading = (props) => {
    if (props.error) {
        console.log(props.error);
        return <div>Error!<button onClick={props.retry}>Retry</button></div>;
    } else if (props.pastDelay) {
        return <div>Loading...</div>;
    } else {
        return null;
    }
}

const lazyLoad = opts => Loadable({
    ...opts,
    loading: Loading,
    delay: 300
    });

export const AsyncCheckoutCtn = lazyLoad({
    loader: () => import('./CheckoutCtn' /*webpackChunkName: "checkout"*/)
});

export const AsyncCartCtn = lazyLoad({
    loader: () => import('./CartCtn' /*webpackChunkName: "cart"*/)
});

export const AsyncOrderHistoryCtn = lazyLoad({
    loader: () => import('./OrderHistoryCtn' /*webpackChunkName: "orderHistory"*/)
});

export const AsyncOrderInquiryCtn = lazyLoad({
    loader: () => import('./OrderInquiryCtn' /*webpackChunkName: "orderInquiry"*/)
});

export const AsyncOrderDetailCtn = lazyLoad({
    loader: () => import('./OrderDetailCtn' /*webpackChunkName: "orderDetail"*/)
});

export const AsyncLogin = lazyLoad({
    loader: () => import('./LoginCtn' /*webpackChunkName: "login"*/)
});

export const Async404 = lazyLoad({
    loader: () => import('./notFound/NotFound' /*webpackChunkName: "notFound"*/)
});

