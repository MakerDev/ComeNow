import { IReceiver } from "./receiver";

export interface ICommand {
    id : number;
    commandName : string;
    message : string;
    receivers : IReceiver[]
}