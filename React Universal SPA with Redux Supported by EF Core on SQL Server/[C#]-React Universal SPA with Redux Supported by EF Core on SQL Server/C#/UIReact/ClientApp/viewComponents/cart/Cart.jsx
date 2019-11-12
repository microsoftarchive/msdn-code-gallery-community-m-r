import React from 'react';
import { Link } from 'react-router-dom';

import selectDefaultCategory from '../../reduxStore/helpers/selectDefaultCategory';
import ImgSide from '../imgSide/ImgSide';
import routePaths from '../../constants/routes';
import CartTableCtn from '../../viewContainers/CartTableCtn';
import CartButtonsCtn from '../../viewContainers/CartButtonsCtn';

class Cart extends React.Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const { selectedCategoryId, selectCategory } = this.props;
        selectDefaultCategory(selectedCategoryId, selectCategory);
    }

    render() {
        const { itemCount } = this.props;

        return (
            <div className="container-fluid">
                <div className="row">
                    <div className="col-md-3 p-0 d-none d-md-block">
                        <ImgSide source="image/left_cart.jpg" />
                    </div>
                    <div className="col-md-9 col-sm-12">
                        <h6 className="mt-3">
                            Your Cart
                        </h6>
                        <div className="pt-3">
                            {itemCount === 0
                                ? <EmptyCart />
                                : <div>
                                    <CartTableCtn />
                                    <CartButtonsCtn />
                                  </div>
                            }
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

const EmptyCart = () => (
    <div className="alert alert-warning">
        There are no skis in your shopping cart.
        <Link to={routePaths.home} className="alert-link">
            &nbsp; Click here to go back to the Home page.
        </Link>
    </div>
);

export default Cart;