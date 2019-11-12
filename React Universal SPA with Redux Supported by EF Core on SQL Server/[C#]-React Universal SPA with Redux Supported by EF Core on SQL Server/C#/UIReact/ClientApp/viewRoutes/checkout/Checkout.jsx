import React from 'react';
import { Formik, Form, Field } from 'formik';
import * as Yup from 'yup';
import * as moment from 'moment';
import { Link } from 'react-router-dom';

import FormErrorMsg from '../../viewComponents/formErrorMsg/FormErrorMsg';
import routePaths from '../../constants/routes';
import { executePostOrder } from '../../reduxStore/actions/handleOrders';

const checkoutSchema = Yup.object().shape({
    fullName: Yup.string()
        .required('Required'),
    emailCheckout: Yup.string()
        .email('Invalid email')
        .required('Required'),
    addressLine: Yup.string()
        .required('Required'),
    city: Yup.string()
        .required('Required'),
    provinceId: Yup.number()
        .required('Required'),
    postalCode: Yup.string()
        .required('Required')
});

const ErrorMsg = ({ name }) => (
    <div className="ml-2">
        <FormErrorMsg name={name} />
    </div>
);

const FieldGroup = ({ labelName, fieldName, fieldType }) => (
    <div className="form-group row">
        <label className="col-form-label col-md-3">{labelName}<sup>*</sup></label>
        <Field name={fieldName} type={fieldType} className="form-control col-md-9" />
        <ErrorMsg name={fieldName} />
    </div>
);

const FieldSelect = ({ fieldName, provinces }) => (
    <Field name={fieldName} component="select" className="form-control col-md-9">
        {provinces.map(p => (
            <option key={p.provinceId} value={p.provinceId}>{p.provinceName}</option>
        ))}
    </Field>
);

const SelectGroup = ({ labelName, fieldName, provinces }) => (
    <div className="form-group row">
        <label className="col-form-label col-md-3">{labelName}<sup>*</sup></label>
        <FieldSelect fieldName={fieldName} provinces={provinces} />
        <ErrorMsg name={fieldName} />
    </div>
);

const CheckoutDone = ({ customerOrderId }) => (
    <div className="m-1 text-center">
        <p>Thanks for placing your order {customerOrderId}.</p>
        <p>We will ship your order as soon as we can.</p>
        <Link to={routePaths.home} className="btn btn-primary">
            Return to Shop
        </Link>
    </div>
);

const CheckoutFailed = () => (
    <div className="m-1">
        Sorry, failed processing your order. Please try again. 
    </div>
);

class Checkout extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isSent: false,
            isSuccessful: false,
            customerOrderId: ''
        }
    }

    componentDidMount() {
        this.props.getProvinces();
    }

    getOrderModel = (values) => {
        const { user, totalValue, lines } = this.props;

        const orderModel = {
            email: values.emailCheckout,
            fullName: values.fullName,
            provinceId: values.provinceId,
            city: values.city,
            addressLine: values.addressLine,
            postalCode: values.postalCode,
            totalValue: totalValue,
            createdDateTime: moment().format('MMM D YYYY h:mmA'),
            orderItems: [...lines]
        };

        if (user && Object.keys(user).length > 0 && user.userId > 0) {
            orderModel.userId = user.userId;
        }

        return orderModel;
    }

    getUserInfo = () => {
        const { user } = this.props;

        return user && Object.keys(user).length > 0 && user.userId > 0
            ? {
                fullName: user.fullName,
                email: user.email
            }
            : {
                fullName: '',
                email: ''
            }
    }

    render() {
        const { isSent, isSuccessful } = this.state;

        if (isSent && isSuccessful) return <CheckoutDone customerOrderId={this.state.customerOrderId}/>;
        if (isSent && !isSuccessful) return <CheckoutFailed/>;

        const { provinces, lines, updateSkus, updateStyleStates, updateSkusOverStock,
            navToCart, clearCart } = this.props;

        if (lines.length < 1) return <div className="m-1">There are no skis to check out. </div>;

        const userInfo = this.getUserInfo();

        return (
            <div className="container h-100 mt-2 align-content-center">
                { userInfo.fullName === '' &&
                    <p className="text-warning font-weight-bold">You are checking out as a guest.</p>
                }
                <Formik
                    initialValues={{
                        fullName: userInfo.fullName,
                        emailCheckout: userInfo.email,
                        addressLine: '',
                        city: '',
                        provinceId: 1,
                        postalCode: ''
                    }}
                    validationSchema={checkoutSchema}
                    onSubmit={(values, actions) => {
                        const orderModel = this.getOrderModel(values);

                        executePostOrder(orderModel)
                            .then((res) => {
                                
                                const { orderId, customerOrderId, skuIdsOverStock, skus, styleStates } = res;

                                updateSkus(skus);
                                updateStyleStates(styleStates);

                                const skusOverStock = skuIdsOverStock.map(id => skus.find(sku => sku.skuId === id));
                                updateSkusOverStock(skusOverStock);

                                if (orderId > 1) {

                                    clearCart();

                                    this.setState({
                                        isSent: true,
                                        isSuccessful: true,
                                        customerOrderId: customerOrderId
                                    });

                                } else {
                                    navToCart();
                                }

                                actions.setSubmitting(false);
                            })
                            .catch(e => {
                                console.log(e);
                                this.setState({
                                    isSent: true,
                                    isSuccessful: false
                                });

                                actions.setSubmitting(false);
                            });
                        
                    }}
                    render={({ status, isSubmitting, handleReset }) => (
                        <Form>
                            <strong>Ship to:</strong>
                            <FieldGroup labelName="Full Name" fieldName="fullName" fieldType="text" />
                            <FieldGroup labelName="Email" fieldName="emailCheckout" fieldType="email" />
                            <strong>Address:</strong>
                            <FieldGroup labelName="Street" fieldName="addressLine" fieldType="text" />
                            <FieldGroup labelName="City" fieldName="city" fieldType="text" />
                            <SelectGroup labelName="Province" fieldName="provinceId" provinces={provinces}  />
                            <FieldGroup labelName="Postal Code" fieldName="postalCode" fieldType="text" />

                            {status && status.msg && <div className="text-danger">{status.msg}</div>}
                            <button type="submit" className="btn btn-primary mb-3 mr-3" disabled={isSubmitting}>
                                Submit
                            </button>
                            <button type="reset" className="btn btn-primary mb-3" onClick={handleReset}>
                                Reset
                            </button>
                        </Form>
                    )
                    }>
                </Formik>
            </div>
        );
    }
} 

export default Checkout;


