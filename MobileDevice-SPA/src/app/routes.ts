
import { HomeComponent } from './home/home.component';
import { Routes } from '@angular/router';
import { DepartmentListComponent } from './department-list/department-list.component';
import { DepartmentEditComponent } from './department-edit/department-edit.component';
import { ManufacturerListComponent } from './manufacturer-list/manufacturer-list.component';
import { ManufacturerEditComponent } from './manufacturer-edit/manufacturer-edit.component';
import { DevicestatusListComponent } from './devicestatus-list/devicestatus-list.component';
import { DevicestatusEditComponent } from './devicestatus-edit/devicestatus-edit.component';
import { DeviceattributetypeListComponent } from './deviceattributetype-list/deviceattributetype-list.component';
import { DeviceattributetypeEditComponent } from './deviceattributetype-edit/deviceattributetype-edit.component';
import { ProducttypeListComponent } from './producttype-list/producttype-list.component';
import { ProducttypeEditComponent } from './producttype-edit/producttype-edit.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';




import { DepartmentListResolver } from './_resolvers/department-list.resolver';
import { DepartmentEditResolver } from './_resolvers/department-edit.resolver';
import { ProductTypeListResolver } from './_resolvers/producttype-list.resolver';
import { DeviceStatusListResolver } from './_resolvers/devicestatus-list.resolver';
import { DeviceStatusEditResolver } from './_resolvers/devicestatus-edit.resolver';
import { DeviceAttributeTypeListResolver } from './_resolvers/deviceattributetype-list.resolver';
import { DeviceAttributeTypeEditResolver } from './_resolvers/deviceattributetype-edit.resolver';
import { ManufacturerListResolver } from './_resolvers/manufacturer-list.resolver';
import { ManufacturerEditResolver } from './_resolvers/manufacturer-edit.resolver';
import { ProductTypeEditResolver } from './_resolvers/producttype-edit.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { UserEditResolver } from './_resolvers/user-edit.resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        children: [
            { path: 'users', component: UserListComponent, resolve: {users: UserListResolver}},
            { path: 'users/edit/:id', component: UserEditComponent, resolve: {user: UserEditResolver}},
            { path: 'departments', component: DepartmentListComponent, resolve: {departments: DepartmentListResolver}},
            { path: 'departments/edit/:id', component: DepartmentEditComponent, resolve: {department: DepartmentEditResolver}},
            // tslint:disable-next-line:max-line-length
            { path: 'deviceattributetypes', component: DeviceattributetypeListComponent, resolve: {deviceattributetypes: DeviceAttributeTypeListResolver}},
            // tslint:disable-next-line:max-line-length
            { path: 'deviceattributetypes/edit/:id', component: DeviceattributetypeEditComponent, resolve: {deviceattributetype: DeviceAttributeTypeEditResolver}},
            { path: 'devicestatuses', component: DevicestatusListComponent, resolve: {devicestatuses: DeviceStatusListResolver}},
            { path: 'devicestatuses/edit/:id', component: DevicestatusEditComponent, resolve: {devicestatus: DeviceStatusEditResolver}},
            { path: 'manufacturers', component: ManufacturerListComponent, resolve: {manufacturers: ManufacturerListResolver}},
            { path: 'manufacturers/edit/:id', component: ManufacturerEditComponent, resolve: {manufacturer: ManufacturerEditResolver}},
            { path: 'producttypes', component: ProducttypeListComponent, resolve: {producttypes: ProductTypeListResolver}},
            { path: 'producttypes/edit/:id', component: ProducttypeEditComponent, resolve: {producttype: ProductTypeEditResolver}}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];

