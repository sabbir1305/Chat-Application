import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, throwError, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Message } from '../_models';

@Injectable({ providedIn: 'root' })
export class SignalRService {


  constructor(private http: HttpClient) { }

  getReceiverAllMessage(senderId: number,receiverId:number) :Observable<Message[]> {
    console.log(senderId);
    console.log(receiverId);
      return this.http.get<Message[]>(`${environment.apiUrl}/messages/${senderId}/${receiverId}`);
  }
  sentMessage(message: Message): Observable<Message> {
    return this.http.post<Message>(`${environment.apiUrl}/messages/create`, message).pipe(
      catchError(this.handleError)
    );
}




  delete(id: number): Observable<{}> {
      return this.http.delete(`${environment.apiUrl}/messages/${id}`)
          .pipe(map(x => {

              return x;
          }));
  }

  deleteReceiverMsg(id: number): Observable<{}> {
    return this.http.delete(`${environment.apiUrl}/messages/ReceiverDelete/${id}`)
        .pipe(map(x => {

            return x;
        }));
}

deleteAllMsg(receiverId: number,senderId:number): Observable<{}> {
  return this.http.delete(`${environment.apiUrl}/messages/${senderId}/${receiverId}`)
      .pipe(map(x => {

          return x;
      }));
}






  private handleError(err) {
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }



}
