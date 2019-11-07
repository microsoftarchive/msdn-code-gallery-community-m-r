import React from 'react';
import { ErrorMessage } from 'formik';

const FormErrorMsg = ({ name }) => (
    <ErrorMessage name={name} component="div" className="text-danger" />
);

export default FormErrorMsg;
