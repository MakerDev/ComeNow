import { RootStore } from "./rootStore";
import { reaction, observable, action } from "mobx";

export default class ReceiverStore {
    rootStore: RootStore;
    

    constructor(rootStore: RootStore) {
      this.rootStore = rootStore;
    }


}