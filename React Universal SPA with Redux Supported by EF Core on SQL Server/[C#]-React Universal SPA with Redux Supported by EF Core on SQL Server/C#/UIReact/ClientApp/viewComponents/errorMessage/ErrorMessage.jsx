import React from 'react';

const ErrorMessage = ({ predicate, message }) => {
    if (!predicate) return null;

    return (
        <strong className="text-danger">{message}</strong>
    );
};

export default ErrorMessage;