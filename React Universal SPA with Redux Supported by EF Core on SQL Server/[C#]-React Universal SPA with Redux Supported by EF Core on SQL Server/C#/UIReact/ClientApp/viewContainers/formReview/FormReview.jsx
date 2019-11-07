import React from 'react';
import { Formik, Form, Field } from 'formik';
import * as Yup from 'yup';
import Rating from 'react-rating';
import * as moment from 'moment';

import FormErrorMsg from '../../viewComponents/formErrorMsg/FormErrorMsg';

import '../../viewComponents/ratingOnly/ratingOnly.scss';

const reviewSchema = Yup.object().shape({
    rating: Yup.number()
        .moreThan(0.1, 'Required')
        .required('Required'),
    yourReview: Yup.string()
        .min(10, 'at least 10 characters')
        .required('Required')
});

const RatingField = ({
    field: { name, value },
    form: { setFieldValue },
}) => {
    return (
        <div>
            <Rating emptySymbol="fa fa-star empty" fullSymbol="fa fa-star full"
                    initialRating={value} onClick={(rate) => setFieldValue(name, rate)} />
        </div>
    );
}

const FieldGroup = ({labelName, fieldName, component}) => (
    <div className="form-group">
        <strong>{labelName}<sup>*</sup></strong>
        <Field name={fieldName} className="form-control" component={component}/>
        <FormErrorMsg name={fieldName}/>
    </div>
);

const FormReview = ({ styleId, user, addReview, closeReviewForm }) => {

    return (
        <div className="container h-100 mt-2 border align-content-center">
            <Formik
                initialValues={{
                rating: 0,
                yourReview: ''
            }}
                validationSchema={reviewSchema}
                onSubmit={(values, actions) => {

                    const reviewModel = {
                        styleId: Number(styleId),
                        userId: user.userId,
                        rating: values.rating,
                        reviewText: values.yourReview,
                        createdDateTime: moment().format('MMM D YYYY h:mmA')
                    }

                    addReview(reviewModel, user);

                    actions.setSubmitting(false);

                    values.rating = 0;
                    values.yourReview = '';

                    closeReviewForm();
                }}
                render={({ status, isSubmitting, dirty, isValid }) => (
                <Form>
                    <FieldGroup labelName="Overall Rating" fieldName="rating" component={RatingField} />
                    <FieldGroup labelName="Review" fieldName="yourReview" component="textarea" />
                    {status && status.msg && <div className="text-danger">{status.msg}</div>}
                        <button type="submit" className="btn btn-primary mb-3 mr-3" disabled={isSubmitting}>
                    Submit
                    </button>
                    <button type="button" className="btn btn-primary mb-3" data-dismiss="modal">
                        Cancel
                    </button>
                </Form>
            )
            }>
            </Formik>
        </div>
    );
}

export default FormReview;