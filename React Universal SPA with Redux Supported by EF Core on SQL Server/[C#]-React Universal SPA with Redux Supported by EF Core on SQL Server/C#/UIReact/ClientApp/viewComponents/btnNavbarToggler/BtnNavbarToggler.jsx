import React from 'react';

const BtnNavbarToggler = ({ dataTarget, ariaControls, ariaLabel }) => (
    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target={`#${dataTarget}`}
        aria-controls={ariaControls} aria-expanded="false" aria-label={ariaLabel}>
        <span className="navbar-toggler-icon"></span>
    </button>
);

export default BtnNavbarToggler;

