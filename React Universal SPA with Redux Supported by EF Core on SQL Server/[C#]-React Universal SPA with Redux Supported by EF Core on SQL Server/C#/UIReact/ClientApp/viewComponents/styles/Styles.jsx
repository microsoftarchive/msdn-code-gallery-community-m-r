import React from 'react';
import { parse, stringify } from 'qs';

import BtnNavbarToggler from '../btnNavbarToggler/BtnNavbarToggler';
import StylesFilteredCtn from '../../viewContainers/StylesFilteredCtn';
import BtnFilter from '../btnFilter/BtnFilter';
import NavPages from '../navPages/NavPages';

import '../../styles/app.scss';

class Styles extends React.Component {
    constructor(props) {
        super(props);

        this.sortStyles = this.sortStyles.bind(this);
        this.pageStyles = this.pageStyles.bind(this);
        this.filterStyles = this.filterStyles.bind(this);
    }

    updateSelectedCategory = () => {
        const { selectedId, selectItem, location } = this.props;
        const categoryId = Number(parse(location.search.substring(1)).categoryId);

        if (selectedId !== categoryId) selectItem(categoryId);
    }

    componentDidMount() {
        this.updateSelectedCategory();

        this.props.fetchData();
    }

    componentDidUpdate() {
        this.updateSelectedCategory();

        this.props.fetchData();
    }

    getQueryObj = () => {
        return parse(this.props.location.search.substring(1));
    }

    sortStyles = (event) => {
        const queryObj = this.getQueryObj();
        queryObj.sort = event.target.value;
        queryObj.pageNumber = 1;

        this.updateUrl(queryObj);
    }

    pageStyles = (event) => {
        const queryObj = this.getQueryObj();
        queryObj.pageNumber = event.target.value;

        this.updateUrl(queryObj);
    }

    filterStyles = (event, propName) => {
        const queryObj = this.getQueryObj();
        const index = event.target.value;

        if (Object.keys(queryObj).includes(propName)) {
            const indexQ = queryObj[propName].indexOf(index);
            if (indexQ < 0) {
                queryObj[propName].push(index);
            } else {
                queryObj[propName].splice(indexQ, 1);

                if (queryObj[propName].length < 1) delete queryObj[propName];
            }
        } else {
            queryObj[propName] = [index];
        }

        queryObj.pageNumber = 1;

        this.updateUrl(queryObj);
    }

    updateUrl = (queryObj) => {
        const { location, updateQuery } = this.props;

        const locationToUpdate = {
            pathName: location.pathname,
            search: stringify(queryObj)
        };

        updateQuery(locationToUpdate);
    }

    render() {
        const queryObj = this.getQueryObj();

        if (!this.props.results) return null;

        const { pageNumber, pageSize } = queryObj;
        const { brandCounts, genderCounts, idealForCounts, totalCount, stylesFiltered } = this.props.results;

        const sortClasses = (index, propName) =>
            `px-1 ml-1 border-0 text-left w-100 ${Object.keys(queryObj).includes(propName) && Number(queryObj[propName]) === index
                ? 'bg-info text-white'
                : 'bg-light'}`;

        const filterClasses = (index, propName) => 
            `px-1 ml-1 border-0 text-left w-100 ${Object.keys(queryObj).includes(propName) && queryObj[propName].includes(index.toString())
                ? 'bg-info text-white'
                : 'bg-light'}`;

        const classesBtnGroup = 'btn-group-vertical btn-group-sm cus-font-xxs';

        return (
            <div className="container-fluid">
                <div className="row">
                    <div className="col-md-4 col-lg-3 col-xl-3 bg-light pl-0 cus-font-xxs">
                        <nav className="navbar navbar-expand-md navbar-light flex-row flex-nowrap">
                            <BtnNavbarToggler dataTarget={idSideBar} ariaControls="sidebarStylesFiltered" ariaLabel="Toggle sidebar of styles filtered" />

                            <div className="collapse navbar-collapse" id={idSideBar}>
                                <div className="nav flex-column">
                                    <label className="font-weight-bold mb-0">Sort</label>
                                    <div className={classesBtnGroup}>
                                        {Object.keys(sortValues).map(key => {
                                            const { label, index } = sortValues[key];
                                            return (
                                                <BtnSort key={index} itemIndex={index} itemName={label}
                                                    getClasses={sortClasses}
                                                    sortStyles={this.sortStyles} />
                                            );
                                        })}
                                    </div>

                                    <label className="mt-2 mb-0 font-weight-bold">Filter</label>
                                    <label className="ml-1 mb-0 font-weight-bold">Brands</label>
                                    <div className={classesBtnGroup}>
                                        {brandCounts.map(brand => (
                                            <BtnFilter key={brand.brandId} itemId={brand.brandId} itemName={brand.brandName}
                                                itemCount={brand.brandCount}
                                                getClasses={filterClasses(brand.brandId, queryProps.brandIds)}
                                                processStyles={() => this.filterStyles(event, queryProps.brandIds)} />
                                        ))}
                                    </div>

                                    <label className="ml-1 mb-0 font-weight-bold">Genders</label>
                                    <div className={classesBtnGroup}>
                                        {genderCounts.map(gender => (
                                            <BtnFilter key={gender.genderId} itemId={gender.genderId} itemName={gender.genderName}
                                                itemCount={gender.genderCount}
                                                getClasses={filterClasses(gender.genderId, queryProps.genderIds)}
                                                processStyles={() => this.filterStyles(event, queryProps.genderIds)} />
                                        ))}
                                    </div>

                                    <label className="ml-1 mb-0 font-weight-bold">Ideal For</label>
                                    <div className={classesBtnGroup}>
                                        {idealForCounts.map(idealFor => (
                                            <BtnFilter key={idealFor.idealForId} itemId={idealFor.idealForId}
                                                itemName={idealFor.idealForSpec} itemCount={idealFor.idealForCount}
                                                getClasses={filterClasses(idealFor.idealForId, queryProps.idealForIds)}
                                                processStyles={() => this.filterStyles(event, queryProps.idealForIds)} />
                                        ))}
                                    </div>
                                </div>
                            </div>
                        </nav>
                    </div>
                    <div className="col py-2">
                        <StylesFilteredCtn styleIds={stylesFiltered} className="mt-5" />
                        <NavPages totalCount={totalCount} currentPage={Number(pageNumber)} pageSize={pageSize}
                            pageStyles={this.pageStyles}/>
                    </div>
                </div>
            </div>
        );
    }
}

const idSideBar = 'stylesByCategorySideBar';

const sortValues = {
    priceLowToHigh: {
        label: 'Price Low to High',
        index: 1
    },
    priceHighToLow: {
        label: 'Price High to Low',
        index: 2
    }
};

const BtnSort = ({ itemIndex, itemName, getClasses, sortStyles}) => (
    <button value={itemIndex} className={getClasses(itemIndex, queryProps.sort)}
        onClick={sortStyles}>
        {itemName}
    </button>
);

const queryProps = {
    category: 'category',
    pageNumber: 'pageNumber',
    pageSize: 'pageSize',
    sort: 'sort',
    brandIds: 'brandIds',
    genderIds: 'genderIds',
    idealForIds: 'idealForIds'
};

export default Styles;