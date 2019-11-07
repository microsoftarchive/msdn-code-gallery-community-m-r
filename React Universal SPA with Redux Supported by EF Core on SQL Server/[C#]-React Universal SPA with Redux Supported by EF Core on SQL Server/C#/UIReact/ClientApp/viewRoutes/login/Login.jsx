import React from 'react';
import { Formik, Form, Field } from 'formik';
import * as Yup from 'yup';

import FormErrorMsg from '../../viewComponents/formErrorMsg/FormErrorMsg';
import selectDefaultCategory from '../../reduxStore/helpers/selectDefaultCategory';

class Login extends React.Component {
    constructor(props) {
        super(props);
    }

    redirectToLastPage() {
        const { user, history } = this.props;

        if (user && user.userId > 0) history.goBack();
    }

    componentDidMount() {
        const { selectedCategoryId, selectCategory } = this.props;

        selectDefaultCategory(selectedCategoryId, selectCategory);

        this.redirectToLastPage();
    }

    componentDidUpdate() {
        this.redirectToLastPage();
    }

    render() {
        const { user, login} = this.props;

        return (
            <div className="container h-100 mt-5 border align-content-center">
                <h4 className="mb-2">Log in</h4>
                <Formik
                    initialValues={{
                        email: '',
                        password: ''
                    }}
                    validationSchema={loginSchema}
                    onSubmit={(values, actions) => {
                        const loginModel = {
                            email: values.email,
                            password: values.password,
                            rememberMe: true,
                            lockoutFailure: true
                        }

                        login(loginModel);

                        actions.setSubmitting(false);
                    }}
                    render={({ status, isSubmitting }) => (
                        <Form>
                            <FieldGroup labelName="Email" fieldName="email" fieldType="email" />
                            <FieldGroup labelName="Password" fieldName="password" fieldType="password" />
                            {status && status.msg && <div className="text-danger">{status.msg}</div>}
                            {user && user.userId === -1 && <div className="text-danger mb-3">{user.screenName}</div>}
                            <button type="submit" className="btn btn-primary mb-3" disabled={isSubmitting}>
                                Submit
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

const loginSchema = Yup.object().shape({
    email: Yup.string()
        .email('Invalid email')
        .required('Required'),
    password: Yup.string()
        .required('Required')
});

const FieldGroup = ({ labelName, fieldName, fieldType }) => (
    <div className="form-group">
        <strong>{labelName}<sup>*</sup></strong>
        <Field name={fieldName} type={fieldType} className="form-control" />
        <FormErrorMsg name={fieldName} />
    </div>
);

export default Login;
