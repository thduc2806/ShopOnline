import React, { useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import Alert from '@material-ui/lab/Alert';
import { Redirect } from 'react-router-dom';

import { POST_ADD_PRODUCT } from '../api/apiService';
import { Form } from 'react-bootstrap';
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


export default function Product() {
    const classes = useStyles();
    /* SET ADD PRODUCT */
    const [checkAdd, setCheckAdd] = useState(false);
    const [name, setName] = useState(null)
    const [description, setDescription] = useState(null)
    const [price, setPrice] = useState(null)
    const [thumbImg, setThumbImg] = useState(null)
    const [categoryId, setCategoryId] = useState(null)
    const [categories, setCategories] = useState({});


    /* EVENT CHANGE TEXTFIELD IN FORM */
    const handleChangeName = (event) => {
        setName(event.target.value)
    }
    const handleChangeDescription = (event) => {
        setDescription(event.target.value)
    }
    const handleChangePrice = (event) => {
        setPrice(event.target.value)
    }
    const handleChangImagePatch = (event) => {
        setThumbImg(event.target.value)
    }
    const handleChangeCategory = (event) => {
        setCategoryId(event.target.value);
    };

    /* EVENT BUTTON SUBMIT FORM ADD PRODUCT */
    const addProduct = (event) => {
        event.preventDefault();
        if (name !== "" && description !== "" && price !== "" && thumbImg !== "" && categoryId != "") {
            const product = new FormData();
            product.append('Name', name);
            product.append('Description', description);
            product.append('Price', price);
            product.append('CategoryId', categoryId);
            product.append('ThumbImg', thumbImg);
            POST_ADD_PRODUCT(`product`, product).then(item => {
                if (item.data === 1) {
                    setCheckAdd(true);
                }
            })
        }
        else {
            alert("Information wrong!");
        }
    }

    /* CHECK setAdd, if true redirect to Home */
    if (checkAdd) {
        return <Redirect to="/" />
    }

    return (
        <div className={classes.root}>
            <Form onSubmit={addProduct}>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <Paper className={classes.paper}>
                            <Typography className={classes.title} variant="h4">
                                Add Product
                            </Typography>
                            <Grid item xs={12} sm container>
                                <Grid item xs={12}>
                                    <Typography gutterBottom variant="subtitle1">
                                        Name
                                    </Typography>
                                    <TextField id="Name" onChange={handleChangeName} name="Title" label="Title" variant="outlined" className={classes.txtInput} size="small" />
                                </Grid>
                                <Grid item xs={12}>
                                    <Typography gutterBottom variant="subtitle1">
                                        Description
                                    </Typography>
                                    <TextField id="outlined-multiline-static" onChange={handleChangeDescription} label="Description" name="Description" className={classes.txtInput} multiline rows={4} variant="outlined" />
                                </Grid>
                                <Grid item xs={12}>
                                    <Typography gutterBottom variant="subtitle1">
                                        Price
                                    </Typography>
                                    <TextField id="Price" onChange={handleChangePrice} name="Price" label="Price" variant="outlined" className={classes.txtInput} size="small" />
                                </Grid>
                                <Grid item xs={12}>
                                    <Typography gutterBottom variant="subtitle1">
                                        Category Id
                                    </Typography>
                                    <TextField id="CategoryId" onChange={handleChangeCategory} name="CategoryId" label="CategoryId" variant="outlined" className={classes.txtInput} size="small" />
                                </Grid>
                                <Grid item xs={12}>
                                    <Typography gutterBottom variant="subtitle1">
                                        ImagePatch
                                    </Typography>
                                    <TextField type="file" onChange={(event) => setThumbImg(event.target.files[0])} />
                                </Grid>
                                <Grid item xs={12}>
                                    <Button type="button" onClick={addProduct} fullWidth variant="contained" color="primary" className={classes.submit} >
                                        Add product
                                    </Button>
                                </Grid>
                            </Grid>
                        </Paper>
                    </Grid>
                </Grid>
            </Form>
        </div>
    )
}
