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
import { POST_ADD_USERS } from '../api/apiService';
import { Description } from '@material-ui/icons';
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


export default function Users() {
    const classes = useStyles();
    const [checkAdd, setCheckAdd] = useState(false);
    const [fullName, setFullName] = useState(null)
    const [dob, setDOB] = useState(null)
    const [email, setEmail] = useState(null)
    const [phone, setPhone] = useState(null)
    const [username, setUsername] = useState(null)
    const [password, setPassword] = useState(null)
    const [confirmPassword, setConfirmPassword] = useState(null)



    const handleChangeFullName = (event) => {
        setFullName(event.target.value)
    }
    const handleChangeDOB = (event) => {
        setDOB(event.target.value)
    }
    const handleChangeEmail = (event) => {
        setEmail(event.target.value)
    }
    const handleChangePhone = (event) => {
        setPhone(event.target.value)
    }
    const handleChangeUserName = (event) => {
        setUsername(event.target.value)
    }
    const handleChangePassword = (event) => {
        setPassword(event.target.value)
    }
    const handleChangeComfirmPassword = (event) => {
        setConfirmPassword(event.target.value)
    }


    const addUsers = (event) => {
        event.preventDefault();
        if (fullName !== "" && dob !== "", email !== "" && phone !== "" && username !== "" && password !== "" && confirmPassword == password) {
            let users = {
                FullName: fullName,
                DOB: dob,
                Email: email,
                Phone: phone,
                UserName: username,
                Password: password,
                ConfirmPassword: confirmPassword
            }
            POST_ADD_USERS(`users`, users).then(item => {
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
        return <Redirect to="/users" />
    }



    return (
        <div className={classes.root}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Paper className={classes.paper}>
                        <Typography className={classes.title} variant="h4">
                            Add Users
                        </Typography>
                        <Grid item xs={12} sm container>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    FullName
                                </Typography>
                                <TextField id="FullName" onChange={handleChangeFullName} name="FullName" label="FullName" variant="outlined" className={classes.txtInput} size="small" />
                            </Grid>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    DOB
                                </Typography>
                                <TextField type="datetime-local" id="DOB" onChange={handleChangeDOB} name="DOB" label="DOB" variant="outlined" className={classes.txtInput} size="small" />
                            </Grid>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    Email
                                </Typography>
                                <TextField id="Email" onChange={handleChangeEmail} name="Email" label="Email" variant="outlined" className={classes.txtInput} size="small" />
                            </Grid>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    Phone
                                </Typography>
                                <TextField id="Phone" onChange={handleChangePhone} name="Phone" label="Phone" variant="outlined" className={classes.txtInput} size="small" />
                            </Grid>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    UserName
                                </Typography>
                                <TextField id="UserName" onChange={handleChangeUserName} name="UserName" label="UserName" variant="outlined" className={classes.txtInput} size="small" />
                            </Grid>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    Password
                                </Typography>
                                <TextField type="password" id="Password" onChange={handleChangePassword} name="Password" label="Password" variant="outlined" className={classes.txtInput} size="small" />
                            </Grid>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    ConfirmPassword
                                </Typography>
                                <TextField type="password" id="ConfirmPassword" onChange={handleChangeComfirmPassword} name="ConfirmPassword" label="ConfirmPassword" variant="outlined" className={classes.txtInput} size="small" />
                            </Grid>
                            <Grid item xs={12}>
                                <Button type="button" onClick={addUsers} fullWidth variant="contained" color="primary" className={classes.submit} >
                                    Add Users
                                </Button>
                            </Grid>
                        </Grid>
                    </Paper>
                </Grid>
            </Grid>
        </div>
    )
}
