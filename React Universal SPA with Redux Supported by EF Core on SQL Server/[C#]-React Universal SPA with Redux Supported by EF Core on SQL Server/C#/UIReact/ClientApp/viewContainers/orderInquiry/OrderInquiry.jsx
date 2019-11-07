import React from 'react';
import { Formik, Form, Field } from 'formik';
import * as Yup from 'yup';

import selectDefaultCategory from '../../reduxStore/helpers/selectDefaultCategory';
import FormErrorMsg from '../../viewComponents/formErrorMsg/FormErrorMsg';

import '../../styles/app.scss';

class OrderInquiry extends React.Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const { selectedCategoryId, selectCategory, user, navToOrderHistory } = this.props;

        selectDefaultCategory(selectedCategoryId, selectCategory);

        if (user && Object.keys(user).length > 0 && user.userId > 0) {
            navToOrderHistory();
        }
    }

    componentDidUpdate() {
        const { user, navToOrderHistory, orderFound, navToDetail } = this.props;

        if (user && Object.keys(user).length > 0 && user.userId > 0) {
            navToOrderHistory();
        }

        if (orderFound && Object.keys(orderFound).length > 0 && orderFound.email.length > 0)
            navToDetail(orderFound.customerOrderId, orderFound.email);
    }

    componentWillUnmount() {
        const { updateIfFoundOrder } = this.props;

        updateIfFoundOrder({});
    }

    render() {
        const { orderFound, getOrderDetail } = this.props;
        
        return (
            <div className="container h-100 mt-5 border align-content-center">
                <h6>Guest Order Inquiry</h6>
                <Formik
                    initialValues={{
                        customerOrderId: '',
                        guestEmail: ''
                    }}
                    validationSchema={orderInquirySchema}
                    onSubmit={(values, actions) => {
                        const { customerOrderId, guestEmail } = values;

                        getOrderDetail(customerOrderId, guestEmail);

                        actions.setSubmitting(false);
                    }}
                    render={({ status, isSubmitting, handleReset }) => (
                        <Form>
                            <FieldGroup labelName="Order Id" fieldName="customerOrderId" fieldType="text" />
                            <FieldGroup labelName="Email" fieldName="guestEmail" fieldType="email" />
                            {status && status.msg && <div className="text-danger">{status.msg}</div>}
                            { orderFound && orderFound.customerOrderId === 'failed' && <div className="text-danger mb-3">Cannot find this order</div>}
                            <button type="submit" className="btn btn-primary mb-3 mr-3" disabled={isSubmitting}>
                                Submit
                            </button>
                            <button type="reset" className="btn btn-primary mb-3" onClick={handleReset}>
                                Reset
                            </button>
                        </Form>
                    )
                    }
                >
                </Formik>
            </div>
        );
    }
} 

const orderInquirySchema = Yup.object().shape({
    customerOrderId: Yup.string()
        .matches(/^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/,
            {message: 'Invalid Order Id'})
        .required('Required'),
    guestEmail: Yup.string()
        .email('Invalid email')
        .required('Required')
});

const FieldGroup = ({ labelName, fieldName, fieldType }) => (
    <div className="form-group cus-font-sm">
        <strong>{labelName}<sup>*</sup></strong>
        <Field name={fieldName} type={fieldType} className="form-control" />
        <FormErrorMsg name={fieldName} />
    </div>
);

export default OrderInquiry;