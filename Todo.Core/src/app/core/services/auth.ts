import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse, LoginRequest, RegisterRequest } from '../../shared/models/auth.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private readonly STORAGE_KEY = 'userGuid';
  private readonly baseURL = `${environment.apiUrl}Auth`

  constructor(private http: HttpClient) {
  }

  isLoggedIn(): boolean {
    let userGuid = localStorage.getItem('userGuid');
    return userGuid !== null && userGuid !== '';
  }
  getUserGuid(): string | null {
    return localStorage.getItem(this.STORAGE_KEY)
  }
  setUserGuid(guid: string): void {
    localStorage.setItem(this.STORAGE_KEY, guid);
  }
  logout(): void {
    localStorage.removeItem(this.STORAGE_KEY);
  }

  login(data: LoginRequest): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(`${this.baseURL}/Login`, data);
  }
  register(data:RegisterRequest):Observable<ApiResponse>{
    return this.http.post<ApiResponse>(`${this.baseURL}/Register`,data);
  }
}
