import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  messages: string[] = [];

  constructor() { }

  add(message: string) {  // metoda pt a adauga mesajul dat de componenta in arrayul de mesaje
    this.messages.push(message);
  }

  clear() {
    this.messages = [];
  }
}
