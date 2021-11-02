import React, { useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import Alert from '@material-ui/lab/Alert';
import { Redirect } from 'react-router-dom';

/*Import api */
import { GET_ALL_CATEGORY, POST_ADD_PRODUCT } from '../api/apiService';
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
    const [checkAdd, setCheckAdd] = useState(false);
    const [name, setName] = useState(null)
    const [description, setDescription] = useState(null)
    const [price, setPrice] = useState(null)
    const [img, setImg] = useState(null)

    const [category, setCategory] = useState(0);
    const [categories, setCategories] = useState({});

    useEffect(() => {
        GET_ALL_CATEGORY('category').then(item => {
            setCategories(item.data);
        });

    }, [])

    const handleChangeName = (event) => {
        setName(event.target.value)
    }
    const handleChangeDescription = (event) => {
        setDescription(event.target.value)
    }
    const handleChangePrice = (event) => {
        setPrice(event.target.value)
    }
    const handleChangeImg = (event) => {
        setImg(event.target.value)
    }
    const handleChangeCategory = (event) => {
        setCategory(event.target.value);
    };

    const addProduct = (event) => {
        event.preventDefault();
        if (name !== "" && description !== "" && price !== "" && img !== "" && category > 0) {
            let product = {
                Name: name,
                Description: description,
                Price: price,
                ThumbImg: img,
                CategoryId: category
            }
            POST_ADD_PRODUCT(`product`, product).then(item => {
                if (item.data === 1) {
                    setCheckAdd(true);
                }
            })
        }
        else {
            alert("Information Emty");
        }
    }

    if (checkAdd) {
        return <Redirect to="/" />
    }

    return (
        <div className={classes.root}>
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
                                <TextField id="Name" onChange={handleChangeName} name="Name" label="Name" variant="outlined" className={classes.txtInput} size="small" />
                            </Grid>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    Description
                                </Typography>
                                <TextField id="outlined-multiline-static" onChange={handleChangeDescription} label="Description" name="Description" className={classes.txtInput} multiline rows={4} defaultValue="Description" variant="outlined" />
                            </Grid>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    Price
                                </Typography>
                                <TextField id="Price" onChange={handleChangePrice} name="Price" label="Price" variant="outlined" className={classes.txtInput} size="small" />
                            </Grid>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    ThumbImg
                                </Typography>
                                <TextField id="ThumbImg" onChange={handleChangeImg} name="ThumbImg" label="ThumbImg" variant="outlined" className={classes.txtInput} size="small" />
                            </Grid>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    Choose Category
                                </Typography>
                                <TextField
                                    id="outlined-select-currency-native"
                                    name="CategoryId"
                                    select
                                    value={category}
                                    onChange={handleChangeCategory}
                                    SelectProps={{
                                        native: true,
                                    }}
                                    helperText="Please select your currency"
                                    variant="outlined"
                                    className={classes.txtInput}
                                >
                                    <option value="0">Choose category</option>
                                    {categories.length > 0 && categories.map((option) => (
                                        <option key={option.CategoryId} value={option.CategoryId}>
                                            {option.name}
                                        </option>
                                    ))}
                                </TextField>
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
        </div>
    )
}
