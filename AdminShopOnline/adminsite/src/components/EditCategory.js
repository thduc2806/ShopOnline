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
import { GET_CATEGORY_ID, PUT_EDIT_CATEGORY } from '../api/apiService';
import { Category } from '@material-ui/icons';
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
export default function EditCategory({ match, location }) {
    const classes = useStyles();
    const [checkUpdate, setCheckUpdate] = useState(false);
    const [id, setId] = useState(0);
    const [name, setName] = useState(null)
    const [description, setDescription] = useState(null)


    useEffect(() => {
        console.log(location)
        console.log(match.params.id)
        GET_CATEGORY_ID(`Category`, match.params.id).then(categories => {
            setId(categories.data.id)
            setName(categories.data.name);
            setDescription(categories.data.description);
        })


    }, [])

    const handleChangeName = (event) => {
        setName(event.target.value)
    }
    const handleChangeDescription = (event) => {
        setDescription(event.target.value)
    }


    const EditCategory = (event) => {
        event.preventDefault();
        if (name !== "" && description !== "" && id > 0) {
            let categories = {
                Name: name,
                Description: description,
            }
            PUT_EDIT_CATEGORY(`Category/${id}`, categories).then(item => {
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
        return <Redirect to="/category" />
    }

    return (
        <div className={classes.root}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Paper className={classes.paper}>
                        <Typography className={classes.title} variant="h4">
                            Edit Cateogry
                        </Typography>
                        <Grid item xs={12} sm container>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    Name
                                </Typography>
                                <TextField id="Name" onChange={handleChangeName} value={name} name="Name" variant="outlined" className={classes.txtInput} size="small" />
                            </Grid>
                            <Grid item xs={12}>
                                <Typography gutterBottom variant="subtitle1">
                                    Description
                                </Typography>
                                <TextField id="outlined-multiline-static" onChange={handleChangeDescription} defaultValue={description} name="Description" className={classes.txtInput} multiline rows={4} variant="outlined" />
                            </Grid>
                            <Grid item xs={12}>
                                <Button type="button" onClick={EditCategory} fullWidth variant="contained" color="primary" className={classes.submit} >
                                    Update Cateogy
                                </Button>
                            </Grid>
                        </Grid>
                    </Paper>
                </Grid>
            </Grid>
        </div>
    )
}
