import { Taskmodel } from "./taskmodel";

export interface LoginRequest{
    email:string;
    password:string;
}
export interface RegisterRequest{
    FirstName:string;
    Lastname:string;
    Email:string;
    Password:string;
    PhoneNo:string;
    Gender:string;
}
export interface ApiResponse{
    status:boolean;
    message:string;
    userGuid?:string;
    statuscode:number;
    Responselist:Taskmodel[]
}