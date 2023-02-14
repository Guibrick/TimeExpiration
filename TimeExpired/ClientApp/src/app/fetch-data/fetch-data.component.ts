import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Dates } from '../interfaces/dates';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
})
export class FetchDataComponent {
  public dates: Dates[] = [];

  constructor(http: HttpClient) {
    http.get<Dates[]>('https://localhost:5136/api/TimeLicensing').subscribe(
      (result) => {
        this.dates = result;
      },
      (error) => console.error(error)
    );
  }
}
