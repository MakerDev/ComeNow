import { RootStore } from "./rootStore";
import { observable, action } from "mobx";

export default class ModalStore {
    rootStore : RootStore;

    constructor(rootStore : RootStore)
    {
        this.rootStore = rootStore;
    }

    //Mobx가 body 컴포넌트 전체를 스캔하지 않도록 함. 널인지 아닌지만 체크
    @observable.shallow modal = {
        open : false,
        body : null
    }

    //content will be a component
    @action openModal = (content : any) => {
        this.modal.open = true;
        this.modal.body = content;
    }
    @action closeModal = () => {
        this.modal.open = false;
        this.modal.body = null;
    }
}