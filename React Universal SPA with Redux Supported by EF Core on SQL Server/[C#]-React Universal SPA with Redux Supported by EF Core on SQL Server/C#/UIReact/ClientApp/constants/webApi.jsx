// @flow

const apiHost = 'https://localhost:44328';
export const apiHostServer = 'http://localhost:53482';

export const apiPaths = {
    getCategoryStyles: '/api/Home/getHomePage',
    getCategories: '/api/Home/getCategories',
    getStylesHome: '/api/Home/getStylesPolularClearance',
    getStyle: '/api/Skis/getStyleBasic',
    getSkis: '/api/Skis/getSkis',
    getSpecs: '/api/Skis/getSpecs',
    getReviews: '/api/Skis/getReviews',
    getStylesFiltered: '/api/Style/getStylesFiltered',
    getLogInStatus: '/api/Account/checkLoginStatus',
    postReview: '/api/Skis/addReview',
    getProvinces: '/api/Order/getProvinces',
    postOrder: '/api/Order/addOrder',
    getOrdersByUserId: '/api/Order/getOrdersByUserId',
    getOrderDetailById: '/api/Order/getOrderDetailById',
    getOrderDetailByEmailId: '/api/Order/getOrderDetailByIdEmail',
    logIn: '/api/Account/login',
    logOut: '/api/Account/logout'
};

export const getServerApiUrl = (): string => `${apiHostServer}${apiPaths.getCategoryStyles}`; 

export const getClientApiUrl = (apiPath: string): string => `${apiHost}${apiPath}`; 
