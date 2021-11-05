import logo from './logo.svg';
import './App.css';
import { Route } from 'react-router';
import React, { Component, lazy } from 'react';
import Container from '@material-ui/core/Container';
import CssBaseline from '@material-ui/core/CssBaseline';
import MenuTop from './components/MenuTop';
import Home from './components/Home';
import Product from './components/Product';
import EditProduct from './components/EditProduct';
import Category from './components/Category';
import EditCategory from './components/EditCategory';
import AddCategory from './components/AddCateogry'
import AddProduct from './components/AddProduct'
import { AUTH } from './Constants/pages';
import Auth from './components/Auth'
import Users from './components/Users';
import EditUsers from './components/EditUsers';
import addUsers from './components/AddUsers'

// const Auth = lazy(() => import('./components/Auth'));

export default class App extends Component {
  displayName = App.name;
  render() {
    return (
      <React.Fragment>
        <CssBaseline />
        <MenuTop />
        <Container maxWidth="md">
          <Route exact path='/' component={Home} />
          <Route path='/home' component={Home} />
          <Route path='/category' component={Category} />
          <Route path='/users' component={Users} />
          <Route path='/edit/users/:id' component={EditUsers} />
          <Route path='/addusers' component={addUsers} />
          <Route path={AUTH}>
            <Auth />
          </Route>
          <Route path='/edit/category/:id' component={EditCategory} />
          <Route path='/addproduct' component={AddProduct} />
          <Route path='/edit/product/:id' component={EditProduct} />
          <Route path='/addcategory' component={AddCategory} />
        </Container>
      </React.Fragment>
    );
  }
}
