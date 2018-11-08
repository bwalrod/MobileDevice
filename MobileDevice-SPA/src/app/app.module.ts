

/* Modules */
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { BsDropdownModule, PaginationModule, ButtonsModule, TooltipModule, TypeaheadModule } from 'ngx-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';

/* Interceptors */
import { ErrorInterceptorProvider } from './interceptors/ErrorInterceptor';
import { WinAuthInterceptorProvider } from './interceptors/WinAuthInterceptor';

/* Services */
import { AlertifyService } from './_services/alertify.service';
import { UtilityService } from './_services/utility.service';

import { appRoutes } from './routes';

/* Components */
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { DepartmentEditComponent } from './department-edit/department-edit.component';
import { DepartmentListComponent } from './department-list/department-list.component';
import { DepartmentSelectComponent } from './common/department-select/department-select.component';
import { DeviceattributetypeListComponent } from './deviceattributetype-list/deviceattributetype-list.component';
import { DeviceattributetypeEditComponent } from './deviceattributetype-edit/deviceattributetype-edit.component';
import { DevicedatetypeListComponent } from './devicedatetype-list/devicedatetype-list.component';
import { DevicedatetypeEditComponent } from './devicedatetype-edit/devicedatetype-edit.component';
import { DevicestatusListComponent } from './devicestatus-list/devicestatus-list.component';
import { DevicestatusEditComponent } from './devicestatus-edit/devicestatus-edit.component';
import { DevicestatusSelectComponent } from './common/devicestatus-select/devicestatus-select.component';
import { ManufacturerListComponent } from './manufacturer-list/manufacturer-list.component';
import { ManufacturerEditComponent } from './manufacturer-edit/manufacturer-edit.component';
import { ManufacturerSelectComponent } from './common/manufacturer-select/manufacturer-select.component';
import { ProductcapacityListComponent } from './productcapacity-list/productcapacity-list.component';
import { ProductcapacityEditComponent } from './productcapacity-edit/productcapacity-edit.component';
import { ProductcapacitySelectComponent } from './common/productcapacity-select/productcapacity-select.component';
import { ProductmodelEditComponent } from './productmodel-edit/productmodel-edit.component';
import { ProductmodelListComponent } from './productmodel-list/productmodel-list.component';
import { ProductmodelSelectComponent } from './common/productmodel-select/productmodel-select.component';
import { ProducttypeListComponent } from './producttype-list/producttype-list.component';
import { ProducttypeEditComponent } from './producttype-edit/producttype-edit.component';
import { ProducttypeComponent } from './common/producttype/producttype.component';
import { AssigneeListComponent } from './assignee-list/assignee-list.component';
import { AssigneeEditComponent } from './assignee-edit/assignee-edit.component';
import { AssigneeSelectComponent } from './common/assignee-select/assignee-select.component';
import { AssigneeTypeaheadComponent } from './common/assignee-typeahead/assignee-typeahead.component';
import { AssigneeNgSelectComponent } from './common/assignee-ng-select/assignee-ng-select.component';
import { DeviceListComponent } from './device-list/device-list.component';
import { DeviceEditComponent } from './device-edit/device-edit.component';



/* Resolvers */
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { DepartmentListResolver } from './_resolvers/department-list.resolver';
import { DepartmentEditResolver } from './_resolvers/department-edit.resolver';
import { DeviceListResolver } from './_resolvers/device-list.resolver';
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
import { SimcardListResolver } from './_resolvers/simcard-list.resolver';
import { SimcardListComponent } from './simcard-list/simcard-list.component';
import { SimcardEditComponent } from './simcard-edit/simcard-edit.component';
import { SimcardEditResolver } from './_resolvers/simcard-edit.resolver';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductListResolver } from './_resolvers/product-list.resolver';
import { ProductEditResolver } from './_resolvers/product-edit.resolver';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { AssigneeListResolver } from './_resolvers/assignee-list.resolver';
import { AssigneeEditResolver } from './_resolvers/assignee-edit.resolver';
import { DeviceEditResolver } from './_resolvers/device-edit.resolver';




@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      UserListComponent,
      UserEditComponent,
      DepartmentListComponent,
      DepartmentEditComponent,
      DepartmentSelectComponent,
      ManufacturerListComponent,
      ManufacturerEditComponent,
      ProducttypeListComponent,
      ProducttypeEditComponent,
      DevicestatusListComponent,
      DevicestatusEditComponent,
      DevicestatusSelectComponent,
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
      ProductcapacitySelectComponent,
      SimcardListComponent,
      SimcardEditComponent,
      ProductListComponent,
      ProductEditComponent,
      AssigneeListComponent,
      AssigneeEditComponent,
      AssigneeSelectComponent,
      AssigneeTypeaheadComponent,
      AssigneeNgSelectComponent,
      DeviceListComponent,
      DeviceEditComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      PaginationModule.forRoot(),
      BsDropdownModule.forRoot(),
      ButtonsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      TooltipModule.forRoot(),
      TypeaheadModule.forRoot(),
      NgSelectModule
   ],
   providers: [
      AlertifyService,
      UtilityService,
      WinAuthInterceptorProvider,
      ErrorInterceptorProvider,
      AssigneeListResolver,
      AssigneeEditResolver,
      UserEditResolver,
      UserListResolver,
      DepartmentListResolver,
      DepartmentEditResolver,
      DeviceListResolver,
      DeviceEditResolver,
      DeviceAttributeTypeListResolver,
      DeviceAttributeTypeEditResolver,
      DeviceDateTypeListResolver,
      DeviceDateTypeEditResolver,
      DeviceStatusListResolver,
      DeviceStatusEditResolver,
      ManufacturerListResolver,
      ManufacturerEditResolver,
      ProductListResolver,
      ProductEditResolver,
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
