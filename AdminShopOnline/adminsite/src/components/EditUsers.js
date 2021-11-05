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
import { GET_USERS_ID, PUT_EDIT_USERS } from '../api/apiService';
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
export default function EditUsers({ match, location }) {
    const classes = useStyles();
    const [checkUpdate, setCheckUpdate] = useState(false);
    const [id, setId] = useState({});
    const [fullName, setFullName] = useState(null)
    const [phoneNumber, setPhoneNumber] = useState(null)

    useEffect(() => {
        console.log(location)
        console.log(match.params.id)
        GET_USERS_ID(`users`, match.params.id).then(users => {
            setId(users.data.id)
            setFullName(users.data.fullName);
            setPhoneNumber(users.data.phoneNumber);
        })

    }, [])

    const handleChangeName = (event) => {
        setFullName(event.target.value)
    }
    const handleChangePhoneNumber = (event) => {
        setPhoneNumber(event.target.value)
    }

    const EditUsers = (event) => {
        event.preventDefault();
        if (fullName !== "" && phoneNumber !== "") {
            let users = {
                FullName: fullName,
                PhoneNumber: phoneNumber,
            }
            PUT_EDIT_USERS(`Users/${id}`, users).then(item => {
                if (item.data === 1) {
                    setCheckUpdate(true);
                }
            })
        }
        else {
            alert("You have not entered enough information!");
        }
    }

    /* CHECK setAdd, if true redirect to Home component */
    if (checkUpdate) {
        return <Redirect to="/users" />
    }

    return (
        <div className={classes.root}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Paper className={classes.paper}>
                        <Typography className={classes.title} variant="h4">
                            Edit Product
                        </Typography>
                        <Grid item xs={12} sm container>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    Full Name
                                </Typography>
                                <TextField id="FullName" onChange={handleChangeName} value={fullName} name="FullName" variant="outlined" className={classes.txtInput} size="small" />
                            </Grid>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    Phone Number
                                </Typography>
                                <TextField id="PhoneNumber" onChange={handleChangePhoneNumber} value={phoneNumber} name="PhoneNumber" variant="outlined" className={classes.txtInput} size="small" />
                            </Grid>
                            <Grid item xs={12}>
                                <Button type="button" onClick={EditUsers} fullWidth variant="contained" color="primary" className={classes.submit} >
                                    Update User
                                </Button>
                            </Grid>
                        </Grid>
                    </Paper>
                </Grid>
            </Grid>
        </div>
    )
}
