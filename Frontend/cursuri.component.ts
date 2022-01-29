import { Component, OnInit } from '@angular/core';
//import { stringify } from 'querystring';
import { CursService } from '../curs.service';
import { Curs } from '../interfaces/curs';
import { MessageService } from '../message.service';
import { CURSURI } from '../mock-data/mock-courses';

@Component({
  selector: 'app-cursuri',
  templateUrl: './cursuri.component.html',
  styleUrls: ['./cursuri.component.css']
})
export class CursuriComponent implements OnInit {

  cursuri : Curs[] = [];

  constructor(private cursService: CursService, private messageService: MessageService) { }

  ngOnInit(): void {
    this.getCourses();
  }

  getCourses(): void {                                             
    this.cursService.getCourses() 
    .subscribe(cursuri => this.cursuri = cursuri); 
  }

  add(nume: string): void {
    nume = nume.trim();
    if (!nume) { return; }
    this.cursService.addCourse({ nume } as unknown as Curs)
      .subscribe(cursuri => {
        this.cursuri = cursuri;
      });
  }

  delete(curs: Curs): void {
    this.cursuri = this.cursuri.filter(h => h !== curs);
    this.cursService.deleteCourse(curs.id).subscribe(cursuri => this.cursuri = cursuri);
  }
}
