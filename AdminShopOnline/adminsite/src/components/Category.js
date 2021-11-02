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
import { GET_ALL_CATEGORY, DELETE_CATEGORY_ID } from '../api/apiService';
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
    const [category, setCategory] = useState({});
    const [checkDeleteCategory, setCheckDeleteCategory] = useState(false);
    const [close, setClose] = React.useState(false);
    useEffect(() => {
        GET_ALL_CATEGORY(`category`).then(item => setCategory(item.data))

    }, [])
    const RawHTML = (description, className) =>
        <div className={className} dangerouslySetInnerHTML={{ __html: description.replace(/\n/g, '<br />') }} />

    const deleteCategoryId = (id) => {

        DELETE_CATEGORY_ID(`category/${id}`).then(item => {
            console.log(item)
            if (item.data === 1) {
                setCheckDeleteCategory(true);
                setCategory(category.filter(key => key.id !== id))
            }
        })
    }

    return (
        <div className={classes.root}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Paper className={classes.paper}>
                        {checkDeleteCategory && <Alert
                            action={
                                <IconButton
                                    aria-label="close"
                                    color="inherit"
                                    size="small"
                                    onClick={() => {
                                        setClose(true);
                                        setCheckDeleteCategory(false)
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
                                        <TableCell>Id</TableCell>
                                        <TableCell align="center">Name</TableCell>
                                        <TableCell align="center">Description</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {category.length > 0 && category.map((row) => (
                                        <TableRow key={row.Id}>
                                            <TableCell component="th" scope="row">{row.id}</TableCell>
                                            <TableCell align="center">{row.name}</TableCell>
                                            <TableCell align="center">{row.description}</TableCell>
                                            <TableCell align="center">
                                                <Link to={`/edit/category/${row.id}`} className={classes.removeLink}>
                                                    <Button size="small" variant="contained" color="primary">Edit</Button></Link>
                                            </TableCell>
                                            <TableCell align="center">

                                                <Button size="small" variant="contained" color="secondary" onClick={() => deleteCategoryId(row.id)}>Remove</Button>

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
