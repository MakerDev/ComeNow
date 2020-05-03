import React, { useContext, useEffect, Fragment } from "react";
import {
  Route,
  withRouter,
  RouteComponentProps,
  Switch,
} from "react-router-dom";
import "./App.css";
import { RootStoreContext } from "../stores/rootStore";
import { ToastContainer } from "react-toastify";
import HomePage from "../../features/Home/HomePage";
import { observer } from "mobx-react";
import ModalContainer from "../common/modals/ModalContainer";
import Dashboard from "../../features/Dashboard/Dashboard";

const App: React.FC<RouteComponentProps> = () => {
  const rootStore = useContext(RootStoreContext);
  const { getCurrentUser } = rootStore.userStore;
  const { token, setAppLoaded } = rootStore.commonStore;

  useEffect(() => {
    if (token) {
      getCurrentUser().finally(() => setAppLoaded());
    }
  }, [getCurrentUser, token]);

  return (
    <Fragment>
      <ModalContainer />
      <ToastContainer position="bottom-right" />
      <Route exact path="/" component={HomePage} />
      <Switch>
        <Route path="/dashboard">
          <Dashboard />
        </Route>
      </Switch>
    </Fragment>
  );
};

export default withRouter(observer(App));
