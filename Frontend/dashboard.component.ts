import { Component, OnInit } from '@angular/core';
import { CursService } from '../curs.service';
import { Curs } from '../interfaces/curs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: [ './dashboard.component.css' ]
})
export class DashboardComponent implements OnInit {
  cursuri: Curs[] = [];

  constructor(private cursService: CursService) { }

  ngOnInit(): void {
    this.getCourses();
  }

  getCourses(): void {
    this.cursService.getCourses()
      .subscribe(cursuri => this.cursuri = cursuri.slice(1, 5));
  }
}
