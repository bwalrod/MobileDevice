

import { FormsModule } from '@angular/forms';
import { ErrorInterceptorProvider } from './interceptors/ErrorInterceptor';
import { AlertifyService } from './_services/alertify.service';
import { HttpClientModule } from '@angular/common/http';
import { WinAuthInterceptorProvider } from './interceptors/WinAuthInterceptor';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BsDropdownModule, PaginationModule, ButtonsModule, TooltipModule } from 'ngx-bootstrap';

import { appRoutes } from './routes';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { DepartmentEditComponent } from './department-edit/department-edit.component';

import { DepartmentListComponent } from './department-list/department-list.component';

import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { DepartmentListResolver } from './_resolvers/department-list.resolver';
import { DepartmentEditResolver } from './_resolvers/department-edit.resolver';
import { ManufacturerListComponent } from './manufacturer-list/manufacturer-list.component';




@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      UserListComponent,
      UserEditComponent,
      DepartmentListComponent,
      DepartmentEditComponent,
      ManufacturerListComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      PaginationModule.forRoot(),
      BsDropdownModule.forRoot(),
      ButtonsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      TooltipModule.forRoot()
   ],
   providers: [
      AlertifyService,
      WinAuthInterceptorProvider,
      ErrorInterceptorProvider,
      UserEditResolver,
      UserListResolver,
      DepartmentListResolver,
      DepartmentEditResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
