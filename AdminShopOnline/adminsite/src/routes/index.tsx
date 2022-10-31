import React, { lazy, Suspense, useEffect } from "react";
import { Route, Switch } from "react-router-dom";
import {LOGIN } from '../constants/pages';
import { useAppDispatch, useAppSelector } from "../hook/redux";
import LayoutRoute from "./LayoutRoute";
import LayoutRouteHome from "./LayoutRouteHome";
// import Roles from "src/constants/roles";
import { me } from "../container/Authorize/reducer";
import PrivateRoute from "./PrivateRoute";

// const Home = lazy(() => import('../containers/Home'));
// const Login = lazy(() => import('../container/Authorize'));
// const NotFound = lazy(() => import("../containers/NotFound"));

const SusspenseLoading = ({ children }) => (
  <Suspense fallback={null}>
    {children}
  </Suspense>
);

const Routes = () => {
  const { isAuth, account } = useAppSelector(state => state.authReducer);
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(me());
  }, []);

  return (
    <SusspenseLoading>
      <Switch>
        <LayoutRouteHome exact path={LOGIN}>
          {/* <Login /> */}
        </LayoutRouteHome>
        {/* <PrivateRoute exact path={HOME}>
          <Home />
        </PrivateRoute> */}
        {/* <PrivateRoute exact path={CHANGEPASSWORD}>
          <ChangePassword />
        </PrivateRoute> */}
      </Switch>
    </SusspenseLoading>
  )
};

export default Routes;
