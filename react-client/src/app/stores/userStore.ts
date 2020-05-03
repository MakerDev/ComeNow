import { observable, action, runInAction } from "mobx";
import { IUser, IUserForm } from "../models/user";
import agent from "../api/agent";
import { RootStore } from "./rootStore";
import { history } from "../..";

export default class UserStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;
  }
  @observable currentUser: IUser | null = null;

  @action login = async (loginForm: IUserForm) => {
    try {
      const user = await agent.User.login(loginForm);

      runInAction(() => {
        this.currentUser = user;
        this.rootStore.commonStore.setToken(user.token);

        //TODO : set proper history
        this.rootStore.modalStore.closeModal();
        history.push("/dashboard");
      });
    } catch (error) {
      console.log(error);
    }
  };

  @action register = async (registerForm : IUserForm) => {
    try {
      const user = await agent.User.register(registerForm);

      runInAction(()=>{
        this.currentUser = user;
        this.rootStore.commonStore.setToken(user.token);

        this.rootStore.modalStore.closeModal();
        history.push('/dashboard');
      })

    } catch (error) {
      console.log(error);
    }
  }

  @action getCurrentUser = async () => {
      try {
          const user = await agent.User.getCurrentUser();
          
          runInAction(() => {
              this.currentUser = user;
          });
      } catch (error) {
          console.log(error)
      }
  };
}


