import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { map, catchError } from 'rxjs/operators';
import { Dates } from '../interfaces/dates';

@Injectable({
  providedIn: 'root',
})
export class TimeLicensingService {
  constructor(private http: HttpClient) {}
  private controllerUrl = environment.apiServer + '/api/timelicensing';

  private extractData(res: any) {
    let body = res;
    return body;
  }
  private handleErrorObservable(error: any) {
    console.error(error.message || error);
    return throwError(() => error);
  }

  getExpirationDate(): Observable<Dates> {
    return this.http
      .get<Dates>(this.controllerUrl + '/getexp')
      .pipe();
  }

  addExpirationDate(date: Dates): Observable<Dates> {
    return this.http
      .post(this.controllerUrl, date)
      .pipe(map(this.extractData), catchError(this.handleErrorObservable));
  }
}
