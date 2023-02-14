import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TimeLicensingService } from '../services/time.licensing.service';
import { Dates } from '../interfaces/dates';

@Component({
  selector: 'app-datepicker-component',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.css']
})
export class DatepickerComponent implements OnInit {
  dates: Dates[] = [];
  errorMessage = '';
  dateValue = { id: 0, value: '' };
  dateInput = '';

    constructor(private timeLicensingService: TimeLicensingService) {}

    ngOnInit() {
      this.getDate();
    }

    getDate() {
        this.timeLicensingService.getExpirationDate()
        .subscribe({ 
          next: (d) => this.dates = [d],
          error: (e) => this.errorMessage = e});}

    addDate() {
      this.timeLicensingService.addExpirationDate(this.dateValue)
      .subscribe(date => {
        this.getDate();
        this.reset();
        this.dateInput = date.value;
      })
      console.log("Date submitted.");
    }

    private reset() {
      this.dateValue.id = 0;
      this.dateValue.value = '';
   }
}
