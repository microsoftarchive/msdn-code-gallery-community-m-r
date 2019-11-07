import React from 'react';

import ImgSide from '../imgSide/ImgSide';
import PopularCtn from '../../viewContainers/PopularCtn';
import ClearanceCtn from '../../viewContainers/ClearanceCtn';
import selectDefaultCategory from '../../reduxStore/helpers/selectDefaultCategory';

import './home.scss';

class Home extends React.Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const { selectedCategoryId, selectCategory } = this.props;
        selectDefaultCategory(selectedCategoryId, selectCategory);
    }

    render() {
        return (
            <div className="container-fluid">
                <div className="row">
                    <div className="col-md-3 p-0 d-none d-md-block">
                        <ImgSide source="image/left.jpg" />
                    </div>
                    <div className="col-md-9 col-sm-12">
                        <p name="homeStyles">Most Popular Non-clearance</p>
                        <PopularCtn />
                        <p name="homeStyles">Most Popular Clearance</p>
                        <ClearanceCtn />
                    </div>
                </div>
            </div>
        );
    }
}

export default Home;