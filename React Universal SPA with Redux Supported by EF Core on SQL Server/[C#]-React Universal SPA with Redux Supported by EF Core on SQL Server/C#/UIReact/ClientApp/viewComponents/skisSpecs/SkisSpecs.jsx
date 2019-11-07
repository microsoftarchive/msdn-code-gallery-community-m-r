import React from 'react';

import BrMultiLines from '../brMultiLines/BrMultiLines';

class SkisSpecs extends React.Component {
    constructor(props) {
        super(props);
    }

    styleId = () => {
        return this.props.styleId;
    }

    componentDidMount() {
        this.props.getSpecs(this.styleId());
    }

    render() {
        const specs = this.props.specs;

        if (!specs) return <BrMultiLines />;
        
        return (
            <table className="table table-striped">
                <tbody>
                {specs.map(spec => (
                        <tr key={spec.displayIndex}>
                            <td>{spec.specKeyName}</td>
                            <td>
                                {spec.specText.split(',').map((value, i) => (
                                    <p key={i}>{value}</p>
                                ))
                            }
                            </td>
                        </tr>
                        ))}
                </tbody>
            </table>
        );
    }
}

export default SkisSpecs;