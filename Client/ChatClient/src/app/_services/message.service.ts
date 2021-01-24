import { Injectable , EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';



import { environment } from '../../environments/environment';
import { Message } from '../_models';

@Injectable({ providedIn: 'root' })
export class MessageService {
    private messageSubject: BehaviorSubject<Message>;
    public message: Observable<Message>;

    constructor(
        private router: Router,
        private http: HttpClient
    ) {

    }



    getReceiverAllMessage(senderId: number,receiverId:number) {
      console.log(senderId);
      console.log(receiverId);
        return this.http.get<Message[]>(`${environment.apiUrl}/chat/${senderId}/${receiverId}`);
    }
    sentMessage(message: Message) {
      return this.http.post(`${environment.apiUrl}/chat/create`, message);
  }




    delete(id: number) {
        return this.http.delete(`${environment.apiUrl}/chat/${id}`)
            .pipe(map(x => {

                return x;
            }));
    }
}
