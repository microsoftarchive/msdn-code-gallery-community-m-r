import React from 'react';

import ImgBigCtn from '../../viewContainers/ImgBigCtn';
import SkisGeneralCtn from '../../viewContainers/SkisGeneralCtn';
import SkisDetailTabs from '../../viewContainers/skisDetailTabs/SkisDetailTabs';

class Skis extends React.Component {
    constructor(props) {
        super(props);
    }

    styleId = () => {
        return this.props.match.params.styleId;
    }

    componentDidMount() {
        this.props.getSkis(this.styleId());
    }

    render() {
        const styleId = this.styleId();
       
        return (
            <div className="container-fluid">
                    <div className="row">
                        <div className="col-md-1"></div>
                        <div className="col-md-4 col-sm-12 mt-3">
                            <ImgBigCtn styleId={styleId} />
                        </div>
                        <div className="col-md-1"></div>
                        <div className="col-md-6 col-sm-12 mt-3">
                            <SkisGeneralCtn styleId={styleId} />
                        </div>
                    </div>
                    <div className="row mt-5">
                        <div className="col-md-1"></div>
                        <div className="col-md-10 col-sm-12">
                            <SkisDetailTabs styleId={styleId} />
                        </div>
                        <div className="col-md-1"></div>
                    </div>
            </div >
        );
    }
}

export default Skis;