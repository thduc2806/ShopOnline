import React, { Component, useEffect, useState } from 'react';
import { Link } from 'react-router-dom'
import { makeStyles } from '@material-ui/core/styles';
import { AppBar, Toolbar, Typography, IconButton } from '@material-ui/core';
import MenuIcon from '@material-ui/icons/Menu';
import MenuItem from '@material-ui/core/MenuItem';
import Menu from '@material-ui/core/Menu';
import MoreVertIcon from '@material-ui/icons/MoreVert';
import { render } from '@testing-library/react';
import AuthService from '../api/authService';
import { USER_PROFILE_STORAGE_KEY } from '../Constants/oidc-config';

const useStyles = makeStyles((theme) => ({
    root: {
        width: '100%',
        flexGrow: 1,
    },
    title: {
        flexGrow: 1
    },
    linkTo: {
        textDecoration: 'none',
        color: '#000'
    },
    linkHome: {
        textDecoration: 'none',
        color: '#fff'
    }
}));
export default function MenuTop() {
    const classes = useStyles();
    const [anchorEl, setAnchorEl] = React.useState(null);
    const isMenuOpen = Boolean(anchorEl);
    const handleProfileMenuOpen = (event) => {
        setAnchorEl(event.currentTarget);
    };
    const handleMenuClose = () => {
        setAnchorEl(null);
    };
    const menuId = 'primary-search-account-menu';
    const renderMenu = (
        <Menu
            anchorEl={anchorEl}
            anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
            id={menuId}
            keepMounted
            transformOrigin={{ vertical: 'top', horizontal: 'right' }}
            open={isMenuOpen} y

            onClose={handleMenuClose}
        >
            <MenuItem onClick={handleMenuClose}><Link to="/addproduct" className={classes.linkTo}>AddProduct</Link></MenuItem>
            <MenuItem onClick={handleMenuClose}><Link to="/addcategory" className={classes.linkTo}>AddCategory</Link></MenuItem>
            <MenuItem onClick={handleMenuClose}><Link to="/addusers" className={classes.linkTo}>AddUsers</Link></MenuItem>
        </Menu>
    );
    const [userName, setUserName] = useState(undefined);
    const handleLogin = () => {
        console.log("ádasdas")
        AuthService.loginAsync();
    }
    const handleLogout = () => {
        AuthService.logoutAsync();
    }

    useEffect(() => {
        let user = JSON.parse(localStorage.getItem(USER_PROFILE_STORAGE_KEY));
        console.log('Information User: ', user);
        if (user !== undefined) {
            setUserName(user?.name)
        }
    }, []);

    return (
        <div className={classes.root}>
            <AppBar position="static" color="primary">
                <Toolbar>
                    <IconButton edge="start" color="inherit" aria-label="menu">
                        <MenuIcon />
                    </IconButton>
                    <Typography variant="h6" className={classes.title}>
                        <Link to="/" className={classes.linkHome}>Admin Site Shop Online</Link>
                    </Typography>
                    <Typography variant="h6" className={classes.title}>
                        <Link to="/category" className={classes.linkHome}>Category</Link>
                    </Typography>
                    <Typography variant="h6" className={classes.title}>
                        <Link to="/users" className={classes.linkHome}>Users</Link>
                    </Typography>
                    <IconButton edge="end" color="inherit" aria-label="MoreVert" aria-controls={menuId}
                        aria-haspopup="true"
                        onClick={handleProfileMenuOpen}>
                        <MoreVertIcon />
                    </IconButton>
                    <div>
                        {userName === undefined ? (
                            <button
                                className="btn btn-danger"
                                type="button"
                                onClick={handleLogin}>
                                Login
                            </button>
                        ) : (
                            <button
                                className="btn btn-danger"
                                type="button"
                                onClick={handleLogout}>
                                Logout
                            </button>
                        )}
                    </div>


                </Toolbar>
            </AppBar>
            {renderMenu}
        </div>
    );
}

