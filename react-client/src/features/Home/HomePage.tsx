import React, { useContext, Fragment } from "react";
import { RootStore, RootStoreContext } from "../../app/stores/rootStore";
import { Header, Button, Segment } from "semantic-ui-react";
import { Link } from "react-router-dom";
import {observer} from 'mobx-react'
import LoginForm from "../User/LoginForm";

const HomePage = () => {
  const rootStore = useContext(RootStoreContext);
  const { currentUser } = rootStore.userStore;
  const {openModal} = rootStore.modalStore;

  return (
    <Segment inverted textAlign="center" vertical className="masthead">
      {currentUser ? (
        <Fragment>
          <Header
            as="h2"
            inverted
            content={`Welcome back ${currentUser.name}`}
          />
          <Button as={Link} to="/dashboard" size="huge" inverted>
            Go to Dashboard
          </Button>
        </Fragment>
      ) : (
        <Fragment>
          <Header as="h2" inverted content={`Login`} />
          <Button size="huge" inverted onClick={()=>openModal(<LoginForm />)}>
            Login
          </Button>
          <Button size="huge" inverted>
            Register
          </Button>
        </Fragment>
      )}
    </Segment>
  );
};

export default observer(HomePage);
