import React, { useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import Alert from '@material-ui/lab/Alert';
import { Link, Redirect } from 'react-router-dom';

/*Import api */
import { GET_ALL_CATEGORY, POST_ADD_PRODUCT } from '../api/apiService';
import { Col, Form, Row } from 'react-bootstrap';
import { FormGroup } from '@material-ui/core';
const useStyles = makeStyles((theme) => ({
    root: {
        flexGrow: 1,
        marginTop: 20
    },
    paper: {
        padding: theme.spacing(2),
        margin: 'auto',
        maxWidth: 600,
    },
    title: {
        fontSize: 30,
        textAlign: 'center'
    },
    link: {
        padding: 10,
        display: 'inline-block'
    },
    txtInput: {
        width: '98%',
        margin: '1%'
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
}));

const Product = () => {
    const classes = useStyles();
    const [formValue, setFormValue] = useState({})
    const [imgPatch, setImgPatch] = useState(null)

    const [category, setCategory] = useState(0);
    const [categories, setCategories] = useState({});

    useEffect(() => {
        GET_ALL_CATEGORY('category').then(item => {
            setCategories(item.data);
        });

    }, [])

    const handleSubmit = (event) => {
        event.preventDefault();

        const formData = new FormData();
        formData.append('Name', formValue.Name);
        formData.append('Description', formValue.Description);
        formData.append('Price', formValue.Price);
        formData.append('CreateDate', formValue.CreateDate);
        formData.append('CategoryId', formValue.CategoryId);
        formData.append('ImagePatch', imgPatch);

        POST_ADD_PRODUCT(formData).then((response) => {
            console.log(response);
            console.log(formData);
        }).catch((erro) => {
            console.log(erro);
            console.log(formData);
        })
    }

    return (
        <div className={classes.root}>
            <Form onSubmit={handleSubmit}>
                <Row className="mb-3">
                    <Form.Group as={Col} controlId="formGridEmail">
                        <Form.Label>Name</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Name"
                            onChange={({ target }) =>
                                setFormValue((state) => ({
                                    ...state,
                                    Name: target.value,
                                }))
                            } />
                    </Form.Group>
                    <Form.Group as={Col} controlId="formGridEmail">
                        <Form.Label>Description</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Description"
                            onChange={({ target }) =>
                                setFormValue((state) => ({
                                    ...state,
                                    Description: target.value,
                                }))
                            } />
                    </Form.Group>
                    <Form.Group as={Col} controlId="formGridEmail">
                        <Form.Label>Price</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Price"
                            onChange={({ target }) =>
                                setFormValue((state) => ({
                                    ...state,
                                    Price: target.value,
                                }))
                            } />
                    </Form.Group>
                    <Form.Group as={Col} controlId="formGridDate">
                        <Form.Label>CreateDate</Form.Label>
                        <Form.Control
                            type="date"
                            placeholder="Date"
                            onChange={({ target }) =>
                                setFormValue((state) => ({
                                    ...state,
                                    Price: target.value,
                                }))
                            } />
                    </Form.Group>
                    <Form.Group as={Col} controlId="formGridEmail">
                        <Form.Label>Category</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Category"
                            onChange={({ target }) =>
                                setFormValue((state) => ({
                                    ...state,
                                    Price: target.value,
                                }))
                            } />
                    </Form.Group>
                </Row>
                <Form.Group controlId="formFile" className="mb-3">
                    <Form.Label>Image</Form.Label>
                    <Form.Control
                        type="file"
                        onChange={(event) =>
                            setImgPatch(event.target.files[0])}>
                    </Form.Control>
                </Form.Group>
                <Button variant="primary" type="submit">Add Product</Button>
                <Link to="/" type="button" className="btn btn-secondary">Back to Home</Link>
            </Form>
        </div>
    );
};

export default Product;
