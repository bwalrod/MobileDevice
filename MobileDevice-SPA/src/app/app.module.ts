import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { WinAuthInterceptor } from './interceptors/WinAuthInterceptor';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule
   ],
   providers: [
    {
        provide: HTTP_INTERCEPTORS,
        useClass: WinAuthInterceptor,
        multi: true
      }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
