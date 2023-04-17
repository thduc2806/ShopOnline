import React, { Suspense } from "react";
import { Route } from "react-router-dom";

// import InLineLoader from "../components/InlineLoader";
// import LayoutLogin from "../containers/LayoutLogin";

export default function PublicRoute({ children, ...rest }) {
    return (
        <Route {...rest}>
            <Suspense fallback={null}>
                {/* <LayoutLogin>
                    {children}
                </LayoutLogin> */}
            </Suspense>
        </Route>
    );
}