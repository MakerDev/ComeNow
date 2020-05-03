import React, { useContext } from "react";
import { RootStoreContext } from "../../app/stores/rootStore";
import { IUserForm } from "../../app/models/user";
import { Form as FinalForm, Field } from "react-final-form";
import { Form, Header, Button, Label } from "semantic-ui-react";
import TextInput from "../../app/common/TextInput";
import { FORM_ERROR } from "final-form";

const RegisterForm = () => {
  const rootStore = useContext(RootStoreContext);
  const { register } = rootStore.userStore;

  return (
    <FinalForm
      //TODO : Add validation
      onSubmit={(values: IUserForm) =>
        register(values).catch((error) => ({
          /// 암기
          [FORM_ERROR]: error,
        }))
      }
      render={({
        handleSubmit,
        submitting,
        submitError,
        invalid,
        pristine,
        dirtySinceLastSubmit,
      }) => (
        <Form onSubmit={handleSubmit} error>
          <Header as="h2" content="Login" color="teal" textAlign="center" />
          <Field name="email" component={TextInput} placeholder="Email" />
          <Field
            name="password"
            component={TextInput}
            placeholder="Password"
            type="password"
          />

          {submitError && !dirtySinceLastSubmit && (
            <Label error={submitError} text="Invalid email or password" />
          )}
          <br />
          <Button
            disabled={(invalid && !dirtySinceLastSubmit) || pristine}
            loading={submitting}
            color="teal"
            content="Login"
            type="submit"
            fluid
          />
        </Form>
      )}
    />
  );
};

export default RegisterForm;
