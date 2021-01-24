import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MomentModule } from 'ngx-moment';

import { UsersRoutingModule } from './users-routing.module';
import { LayoutComponent } from './layout.component';
import { ListComponent } from './list.component';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        UsersRoutingModule,
        MomentModule,
        BrowserModule


    ],
    declarations: [
        LayoutComponent,



    ]
})
export class UsersModule { }
