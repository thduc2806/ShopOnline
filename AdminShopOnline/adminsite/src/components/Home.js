import React, { useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import Alert from '@material-ui/lab/Alert';
import { Redirect } from 'react-router-dom';
import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';
import Button from '@material-ui/core/Button';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import { Link } from 'react-router-dom'
import Url from '../Utils/Url';
import { GET_ALL_PRODUCTS, DELETE_PRODUCT_ID } from '../api/apiService';
const useStyles = makeStyles((theme) => ({
    root: {
        flexGrow: 1,
        marginTop: 20
    },
    paper: {
        width: '100%',
        margin: 'auto'
    },
    removeLink: {
        textDecoration: 'none'
    }
}));
export default function Home() {
    const classes = useStyles();
    const [products, setProducts] = useState({});
    const [checkDeleteProduct, setCheckDeleteProduct] = useState(false);
    const [close, setClose] = React.useState(false);
    useEffect(() => {
        GET_ALL_PRODUCTS(`product`).then(item => setProducts(item.data))

    }, [])
    const RawHTML = (description, className) =>
        <div className={className} dangerouslySetInnerHTML={{ __html: description.replace(/\n/g, '<br />') }} />

    const deleteProductID = (id) => {

        DELETE_PRODUCT_ID(`product/${id}`).then(item => {
            console.log(item)
            if (item.data === 1) {
                setCheckDeleteProduct(true);
                setProducts(products.filter(key => key.id !== id))
            }
        })
    }

    return (
        <div className={classes.root}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Paper className={classes.paper}>
                        {checkDeleteProduct && <Alert
                            action={
                                <IconButton
                                    aria-label="close"
                                    color="inherit"
                                    size="small"
                                    onClick={() => {
                                        setClose(true);
                                        setCheckDeleteProduct(false)
                                    }}
                                >
                                    <CloseIcon fontSize="inherit" />
                                </IconButton>
                            }
                        >Detele successfuly</Alert>}
                        <TableContainer component={Paper}>
                            <Table className={classes.table} aria-label="simple table">
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Name</TableCell>
                                        <TableCell align="center">Description</TableCell>
                                        <TableCell align="center">Price</TableCell>
                                        <TableCell align="center">CreateDate</TableCell>
                                        <TableCell align="center">Image</TableCell>
                                        <TableCell align="center">Category</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {products.length > 0 && products.map((row) => (
                                        <TableRow key={row.Id}>
                                            <TableCell component="th" scope="row">{row.name}</TableCell>
                                            <TableCell align="left">{RawHTML(row.description, "description")}</TableCell>
                                            <TableCell align="center">{row.price}$</TableCell>
                                            <TableCell align="center">{row.createDate}</TableCell>

                                            <img src={`${Url}${row.thumbImg}`} align="center" width="50" height="50" marginTop="100"></img>
                                            <TableCell align="center">{row.category}</TableCell>
                                            <TableCell align="center">
                                                <Link to={`/edit/product/${row.id}`} className={classes.removeLink}>
                                                    <Button size="small" variant="contained" color="primary">Edit</Button></Link>
                                            </TableCell>
                                            <TableCell align="center">

                                                <Button size="small" variant="contained" color="secondary" onClick={() => deleteProductID(row.id)}>Remove</Button>

                                            </TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </Paper>
                </Grid>
            </Grid>
        </div>
    )
}
