import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { CommonModule } from '@angular/common';
import { Message, User } from '../_models';
import { AccountService, AlertService, SignalRService } from '../_services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';


@Component({ templateUrl: 'home.component.html' })
export class HomeComponent  implements OnInit {
    user: User;
    users:User[];
    usersAll:User[];
    isUserSelected=0;
    form: FormGroup;
    senderId: number;
    receiverId: number;
    body: string;
    messages:Message[];
    receiverName:string;

    loading = false;
    submitted = false;
    allDeleting = false;
    constructor(private accountService: AccountService ,
       private chatService:SignalRService ,
       private formBuilder: FormBuilder,

        private router: Router,

        private alertService: AlertService,
        private messageService: SignalRService

       ) {

    }






    ngOnInit() {

      this.getAllUsers();
      console.log(this.users);
      this.usersAll = this.users;
      this.senderId=Number(this.accountService.userValue.id);


      const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl(environment.signalRUrl)
      .build();

    connection.start().then(function () {
      console.log('SignalR Connected!');
    }).catch(function (err) {
      return console.error(err.toString());
    });

    connection.on("BroadcastMessage", () => {
      this.loadMessage();
    });

    this.form = this.formBuilder.group({
      body: ['', Validators.required],
      receiverId:Number,
      senderId:Number


  });


    }

       getAllUsers(){
        this.accountService.getAll()
        .pipe(first())
        .subscribe(users => this.users = users.filter(x => x.id !== this.accountService.userValue.id));


        }

        onUserSelected(receiverId:number){
          this.receiverId = receiverId;
          this.setReceiverName();
          this.loadMessage();

        }

        setReceiverName(){
          this.accountService.getById(this.receiverId).pipe(first())
          .subscribe(
              data => {
                 this.receiverName=data.firstName+" "+data.lastName;

              },
              error => {
                  this.alertService.error(error);
                  this.loading = false;
              });
        }

        // convenience getter for easy access to form fields
    get f() { return this.form.controls; }

    onSubmit() {
        this.submitted = true;
        this.form.controls['receiverId'].setValue(this.receiverId);
        this.form.controls['senderId'].setValue(this.senderId);

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.form.invalid) {
            return;
        }

        this.loading = true;
        let mesg = this.form.value;
        console.log();
        this.messageService.sentMessage(mesg)
        .pipe(first())
        .subscribe(
            data => {
               // this.alertService.success('Message successfully sent');
                this.loading = false;
                this.form.patchValue({body: ''});
             //   this.loadMessage();
             mesg.sentDate = new Date();
             this.messages.push(mesg);

            },
            error => {
                this.alertService.error(error);
                this.loading = false;
            });



    }

    private loadMessage() {
if(this.receiverId>0){
  this.messageService.getReceiverAllMessage(this.senderId,this.receiverId)
            .pipe(first())
            .subscribe(
                data => {
                   this.messages=data;
                  // this.alertService.success("Message loaded");
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
}

    }

    public deleteMessage(id:number){
      console.log(id);
      const message = this.messages.find(x => x.id === id);
      message.isDeleting = true;
      this.messageService.delete(id)
            .pipe(first())
            .subscribe(() => {
                this.messages = this.messages.filter(x => x.id !== id)
            });
    }

    public deleteReceivedMessage(id:number){
      console.log(id);
      const message = this.messages.find(x => x.id === id);
      message.isDeleting = true;
      this.messageService.deleteReceiverMsg(id)
            .pipe(first())
            .subscribe(() => {
                this.messages = this.messages.filter(x => x.id !== id)
            });
    }

    public deleteAllMessage(receiverId:number){
      this.allDeleting=true;
      this.messageService.deleteAllMsg(receiverId, this.senderId)
            .pipe(first())
            .subscribe(() => {
             // this.alertService.success("All Message Deleted");
              this.allDeleting=false;
            });
    }

    // public filterUser(event: any){
    //   alert(event.target.value);

    //   this.users = this.usersAll.filter(function (item) {
    //     return !item.firstName.includes(event.target.value);
    //   });

    // }



}
