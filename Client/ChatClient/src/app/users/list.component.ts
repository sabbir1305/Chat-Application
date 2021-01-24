import { Component, Input, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { AccountService } from '../_services';

@Component({

  templateUrl: 'list.component.html' ,
  selector: 'app-list'

})

export class ListComponent implements OnInit {
  @Input()  users : any;

    constructor(private accountService: AccountService) {}

    ngOnInit() {
        // this.accountService.getAll()
        //     .pipe(first())
        //     .subscribe(users => this.users = users.filter(x => x.id !== this.accountService.userValue.id));
    }

    deleteUser(id: number) {
        const user = this.users.find(x => x.id === id);
        user.isDeleting = true;
        this.accountService.delete(id)
            .pipe(first())
            .subscribe(() => {
                this.users = this.users.filter(x => x.id !== id)
            });
    }
}
