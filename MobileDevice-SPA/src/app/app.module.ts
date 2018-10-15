

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

/* Components */
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { DepartmentEditComponent } from './department-edit/department-edit.component';
import { DepartmentListComponent } from './department-list/department-list.component';
import { DeviceattributetypeListComponent } from './deviceattributetype-list/deviceattributetype-list.component';
import { DeviceattributetypeEditComponent } from './deviceattributetype-edit/deviceattributetype-edit.component';
import { DevicedatetypeListComponent } from './devicedatetype-list/devicedatetype-list.component';
import { DevicedatetypeEditComponent } from './devicedatetype-edit/devicedatetype-edit.component';
import { DevicestatusListComponent } from './devicestatus-list/devicestatus-list.component';
import { DevicestatusEditComponent } from './devicestatus-edit/devicestatus-edit.component';
import { ManufacturerListComponent } from './manufacturer-list/manufacturer-list.component';
import { ManufacturerEditComponent } from './manufacturer-edit/manufacturer-edit.component';
import { ManufacturerSelectComponent } from './common/manufacturer-select/manufacturer-select.component';
import { ProductcapacityListComponent } from './productcapacity-list/productcapacity-list.component';
import { ProductcapacityEditComponent } from './productcapacity-edit/productcapacity-edit.component';
import { ProductmodelEditComponent } from './productmodel-edit/productmodel-edit.component';
import { ProductmodelListComponent } from './productmodel-list/productmodel-list.component';
import { ProductmodelSelectComponent } from './common/productmodel-select/productmodel-select.component';
import { ProducttypeListComponent } from './producttype-list/producttype-list.component';
import { ProducttypeEditComponent } from './producttype-edit/producttype-edit.component';



/* Resolvers */
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { DepartmentListResolver } from './_resolvers/department-list.resolver';
import { DepartmentEditResolver } from './_resolvers/department-edit.resolver';
import { DeviceAttributeTypeEditResolver } from './_resolvers/deviceattributetype-edit.resolver';
import { DeviceAttributeTypeListResolver } from './_resolvers/deviceattributetype-list.resolver';
import { DeviceDateTypeListResolver } from './_resolvers/devicedatetype-list.resolver';
import { DeviceDateTypeEditResolver } from './_resolvers/devicedatetype-edit.resolver';
import { DeviceStatusEditResolver } from './_resolvers/devicestatus-edit.resolver';
import { DeviceStatusListResolver } from './_resolvers/devicestatus-list.resolver';
import { ManufacturerListResolver } from './_resolvers/manufacturer-list.resolver';
import { ManufacturerEditResolver } from './_resolvers/manufacturer-edit.resolver';
import { ProductcapacityListResolver } from './_resolvers/productcapacity-list.resolver';
import { ProductCapacityEditResolver } from './_resolvers/productcapactity-edit.resolver';
import { ProductModelEditResolver } from './_resolvers/productmodel-edit.resolver';
import { ProductModelListResolver } from './_resolvers/productmodel-list.resolver';
import { ProductTypeListResolver } from './_resolvers/producttype-list.resolver';
import { ProductTypeEditResolver } from './_resolvers/producttype-edit.resolver';
import { ProducttypeComponent } from './common/producttype/producttype.component';
import { SimcardListResolver } from './_resolvers/simcard-list.resolver';
import { SimcardListComponent } from './simcard-list/simcard-list.component';
import { SimcardEditComponent } from './simcard-edit/simcard-edit.component';
import { SimcardEditResolver } from './_resolvers/simcard-edit.resolver';



@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      UserListComponent,
      UserEditComponent,
      DepartmentListComponent,
      DepartmentEditComponent,
      ManufacturerListComponent,
      ManufacturerEditComponent,
      ProducttypeListComponent,
      ProducttypeEditComponent,
      DevicestatusListComponent,
      DevicestatusEditComponent,
      DeviceattributetypeListComponent,
      DeviceattributetypeEditComponent,
      DevicedatetypeListComponent,
      DevicedatetypeEditComponent,
      ProductmodelListComponent,
      ProducttypeComponent,
      ManufacturerSelectComponent,
      ProductmodelEditComponent,
      ProductmodelSelectComponent,
      ProductcapacityListComponent,
      ProductcapacityEditComponent,
      SimcardListComponent,
      SimcardEditComponent
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
      DepartmentEditResolver,
      DeviceAttributeTypeListResolver,
      DeviceAttributeTypeEditResolver,
      DeviceDateTypeListResolver,
      DeviceDateTypeEditResolver,
      DeviceStatusListResolver,
      DeviceStatusEditResolver,
      ManufacturerListResolver,
      ManufacturerEditResolver,
      ProductcapacityListResolver,
      ProductCapacityEditResolver,
      ProductModelListResolver,
      ProductModelEditResolver,
      ProductTypeListResolver,
      ProductTypeEditResolver,
      SimcardListResolver,
      SimcardEditResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
