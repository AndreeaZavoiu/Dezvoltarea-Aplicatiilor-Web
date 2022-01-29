import { Injectable } from '@angular/core';
import { Curs } from './interfaces/curs';
import { CURSURI } from './mock-data/mock-courses';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CursService {
                         
  private cursuriUrl = 'https://localhost:5001/api/Cursuri';  // URL to web api

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
    private messageService : MessageService) { }

    getCourses(): Observable<Curs[]> {
      return this.http.get<Curs[]>(this.cursuriUrl).pipe(
        tap(_ => this.log('fetched courses')),
        catchError(this.handleError<Curs[]>('getCourses', []))
    );
    }
   
    getCourse(id: number): Observable<Curs> {
    return this.http.get<Curs>( `${this.cursuriUrl}/${id}`).pipe(
      tap(_ => this.log('fetched courses')),
      catchError(this.handleError<Curs>('getCourses'))
  );
  }
  
  updateCourse(curs: Curs): Observable<any> {
    return this.http.put<Curs>(this.cursuriUrl, curs, this.httpOptions).pipe(
      tap(_ => this.log('fetched courses')),
      catchError(this.handleError<Curs>('getCourses'))
  );
  }

  addCourse(curs: Curs): Observable<any> {
    curs.id = 30
    curs.zi = 1
    curs.an = 3
    return this.http.post<any>(this.cursuriUrl, curs, this.httpOptions).pipe(
      tap(_ => this.log('add new course')),
      catchError(this.handleError<Curs>('getCourses'))
  );
  }

  deleteCourse(id: number): Observable<any> {
    return this.http.delete<any>(`${this.cursuriUrl}/${id}`, this.httpOptions).pipe(
      tap(_ => this.log('delete course')),
      catchError(this.handleError<Curs>('getCourses'))
  );
  }  

  private log(message: string) {
    this.messageService.add(`CursService: ${message}`);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
  
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
  
      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);
  
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
