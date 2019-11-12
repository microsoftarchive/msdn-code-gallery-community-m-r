import React from 'react';
import { Link } from 'react-router-dom';

const ImgLink = ({ navTo, source}) => (
    <Link to={navTo} className="pb-3 mb-3" >
        <img src={source} alt="loading..." className="w-50"/>
    </Link>
);

export default ImgLink