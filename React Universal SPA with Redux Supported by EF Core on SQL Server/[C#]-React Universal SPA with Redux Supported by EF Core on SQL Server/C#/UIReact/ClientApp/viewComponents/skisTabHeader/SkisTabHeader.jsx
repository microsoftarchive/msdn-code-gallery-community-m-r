import React from 'react';

const SkisTabHeader = ({ currentTabIndex, tabItems, selectTab }) => (
    <div className="btn-group-sm btn-group-toggle flex-wrap" data-toggle="button">
        {tabItems.map(item => {
            const classes = `btn rounded-0 shadow-none ${currentTabIndex === item.index
                ? 'bg-secondary text-white '
                : 'bg-light'}`;

            return (
                <button key={item.index} className={classes} onClick={() => selectTab(item.index)}>
                    {item.name}
                </button>
            );
        })}
    </div>
);

export default SkisTabHeader;