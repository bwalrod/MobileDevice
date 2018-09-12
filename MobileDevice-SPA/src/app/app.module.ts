import { AlertifyService } from './_services/alertify.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { WinAuthInterceptor } from './interceptors/WinAuthInterceptor';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { appRoutes } from './routes';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { FormsModule } from '@angular/forms';
import { UserEditResolver } from './_resolvers/user-edit.resolver';





@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      UserListComponent,
      UserEditComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      RouterModule.forRoot(appRoutes)
    ],
   providers: [
       AlertifyService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: WinAuthInterceptor,
            multi: true
        },
        UserEditResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
