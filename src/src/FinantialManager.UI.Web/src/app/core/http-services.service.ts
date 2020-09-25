import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ConfigService } from './config/config.service';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
    constructor(public http: HttpClient,
                public config: ConfigService) {
    }

  protected post<T>(url: string, data: any): Observable<T> {
    return this.http.post<T>(url, data);
  }

  protected get<T>(url: string, params?: HttpParams): Observable<T> {
    const options = {
      params: params
    }

    return this.http.get<T>(url, options);
  }
}