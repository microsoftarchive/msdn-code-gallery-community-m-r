import React from 'react';

import SkisTabHeader from '../../viewComponents/skisTabHeader/SkisTabHeader';
import SkisDescCtn from '../SkisDescCtn';
import SkisSpecsCtn from '../SkisSpecsCtn';
import SkisReviewsCtn from '../SkisReviewsCtn';

import './skisDetailTabs.scss';

class SkisDetailTabs extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            currentTabIndex: 1
        };

        this.selectTab = this.selectTab.bind(this);
    }

    selectTab = (index) => {
        this.setState({
            currentTabIndex: index
        });
    }

    tabItems = () => {
        return [
            { index: 1, name: 'description' },
            { index: 2, name: 'specs' },
            { index: 3, name: 'reviews'}
        ];
    };

    render() {
        const tabItems = this.tabItems();
        const index = this.state.currentTabIndex;
        const styleId = this.props.styleId;
       
        return (
            <div>
                <SkisTabHeader currentTabIndex={index} tabItems={tabItems} selectTab={this.selectTab} />
                <hr className="mt-0 borderTopHr" />

                {index === tabItems[0].index &&
                    <SkisDescCtn styleId={styleId} />
                }

                {index === tabItems[1].index &&
                    <SkisSpecsCtn styleId={styleId} />
                }

                {index === tabItems[2].index &&
                    <SkisReviewsCtn styleId={styleId}/>
                }
            </div>
        );
    }
}

export default SkisDetailTabs;