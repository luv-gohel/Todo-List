export interface Taskmodel {
    ID?: number;
    TaskGID?: string;
    UserGID: string;
    TaskTitle: string;
    TaskDescription: string;
    Priority: number;
    IsDeleted?: boolean;
    DeletedDate?: Date;
    UpdatedDate?: Date;
    CreatedDate?: Date;
}
export interface GetUserTask {
    UserGID?: string;
    Search?: string;
}
