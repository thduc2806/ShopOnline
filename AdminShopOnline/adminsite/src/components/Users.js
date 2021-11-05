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
import { GET_ALL_USERS, DELETE_USERS_ID } from '../api/apiService';
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
export default function Users() {
    const classes = useStyles();
    const [users, setUsers] = useState({});
    const [checkDeleteUsers, setCheckDeleteUsers] = useState(false);
    const [close, setClose] = React.useState(false);
    useEffect(() => {
        GET_ALL_USERS(`Users`).then(item => setUsers(item.data))

    }, [])

    const deleteUsersId = (id) => {

        DELETE_USERS_ID(`Users/${id}`).then(item => {
            console.log(item)
            if (item.data === 1) {
                setCheckDeleteUsers(true);
                setUsers(users.filter(key => key.id !== id))
            }
        })
    }

    return (
        <div className={classes.root}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Paper className={classes.paper}>
                        {checkDeleteUsers && <Alert
                            action={
                                <IconButton
                                    aria-label="close"
                                    color="inherit"
                                    size="small"
                                    onClick={() => {
                                        setClose(true);
                                        setCheckDeleteUsers(false)
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
                                        <TableCell>Username</TableCell>
                                        <TableCell align="center">FullName</TableCell>
                                        <TableCell align="center">DOB</TableCell>
                                        <TableCell align="center">PhoneNumber</TableCell>
                                        <TableCell align="center">Email</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {users.length > 0 && users.map((row) => (
                                        <TableRow key={row.Id}>
                                            <TableCell component="th" scope="row">{row.userName}</TableCell>
                                            <TableCell align="center">{row.fullName}</TableCell>
                                            <TableCell align="center">{row.dob}</TableCell>
                                            <TableCell align="center">{row.phoneNumber}</TableCell>
                                            <TableCell align="center">{row.email}</TableCell>
                                            <TableCell align="center">
                                                <Link to={`/edit/Users/${row.id}`} className={classes.removeLink}>
                                                    <Button size="small" variant="contained" color="primary">Edit</Button></Link>
                                            </TableCell>
                                            <TableCell align="center">

                                                <Button size="small" variant="contained" color="secondary" onClick={() => deleteUsersId(row.id)}>Remove</Button>

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
