import { Injectable } from '@angular/core';
import { GetUserTask, Taskmodel } from '../../shared/models/taskmodel';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class Task {
  private readonly baseURL = `${environment.apiUrl}`
  constructor(private http: HttpClient) {

  }
  addTask(task: Taskmodel): Observable<any> {
    return this.http.post<any>(`${this.baseURL}Task/AddNote`, task);
  }
  updateTask(task: Taskmodel): Observable<any> {
    return this.http.post<any>(`${this.baseURL}Task/UpdateNote`, task);
  }
  deleteTask(task: Taskmodel): Observable<any> {
    return this.http.delete<any>(`${this.baseURL}Task/DeleteNote`,{body:task});
  }
  getAllTask(task: GetUserTask): Observable<any> {
    return this.http.get<any>(`${this.baseURL}Task/GetNotes`, { params: { UserGID: task.UserGID || '' } });
  }
}
