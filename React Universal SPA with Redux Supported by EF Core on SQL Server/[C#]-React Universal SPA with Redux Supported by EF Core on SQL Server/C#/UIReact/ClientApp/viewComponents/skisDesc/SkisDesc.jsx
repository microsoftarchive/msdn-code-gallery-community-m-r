import React from 'react';

const SkisDesc = ({ descriptions }) => {
    if (!descriptions) return null;

    return (
        <div>
            {descriptions.map(desc =>
                desc.displayIndex === 0
                ? <p key={desc.displayIndex}>{desc.descText}</p>
                : <ul key={desc.displayIndex}><li>{desc.descText}</li></ul>
            )}
        </div>
    );
};

export default SkisDesc;