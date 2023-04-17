import React, { Suspense } from "react";
import { Route } from "react-router-dom";
// import Layout from "../containers/LayoutLogin";

export default function PublicRoute({ children, ...rest }) {
    return (
        <Route {...rest}>
            <Suspense fallback={null}>
                {/* <Layout>
                    {children}
                </Layout  > */}
            </Suspense>
        </Route>
    );
}