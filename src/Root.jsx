import React from "react";
import { createBrowserHistory } from "history";
import { Router, Route, Switch } from "react-router-dom";

import "./styles.css";

import indexRoutes from "./routes/index.jsx";

import withAuthentication from "./application/withAuthentication";

const hist = createBrowserHistory();

const Root = () => (
  <Router history={hist}>
    <Switch>
      {indexRoutes.map((prop, key) => {
        return <Route path={prop.path} component={prop.component} key={key} />;
      })}
    </Switch>
  </Router>
);

export default withAuthentication(Root);
