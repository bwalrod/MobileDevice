import { AssigneeListResolver } from './_resolvers/assignee-list.resolver';
import { AssigneeListComponent } from './assignee-list/assignee-list.component';
import { ProductEditResolver } from './_resolvers/product-edit.resolver';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductListResolver } from './_resolvers/product-list.resolver';
import { ProductListComponent } from './product-list/product-list.component';
import { SimcardEditResolver } from './_resolvers/simcard-edit.resolver';
import { SimcardEditComponent } from './simcard-edit/simcard-edit.component';
import { SimcardListResolver } from './_resolvers/simcard-list.resolver';
import { ProductcapacityEditComponent } from './productcapacity-edit/productcapacity-edit.component';
import { ProductcapacityListResolver } from './_resolvers/productcapacity-list.resolver';
import { ProductcapacityListComponent } from './productcapacity-list/productcapacity-list.component';
import { ProductModelEditResolver } from './_resolvers/productmodel-edit.resolver';
import { ProductmodelEditComponent } from './productmodel-edit/productmodel-edit.component';
import { ProductModelListResolver } from './_resolvers/productmodel-list.resolver';
import { ProductmodelListComponent } from './productmodel-list/productmodel-list.component';
import { DeviceDateTypeEditResolver } from './_resolvers/devicedatetype-edit.resolver';



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
import { DevicedatetypeListComponent } from './devicedatetype-list/devicedatetype-list.component';
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
import { DeviceDateTypeListResolver } from './_resolvers/devicedatetype-list.resolver';
import { ManufacturerListResolver } from './_resolvers/manufacturer-list.resolver';
import { ManufacturerEditResolver } from './_resolvers/manufacturer-edit.resolver';
import { ProductTypeEditResolver } from './_resolvers/producttype-edit.resolver';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { DevicedatetypeEditComponent } from './devicedatetype-edit/devicedatetype-edit.component';
import { ProductCapacityEditResolver } from './_resolvers/productcapactity-edit.resolver';
import { SimcardListComponent } from './simcard-list/simcard-list.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        children: [
            { path: 'assignees', component: AssigneeListComponent, resolve: {assignees: AssigneeListResolver}},
            { path: 'users', component: UserListComponent, resolve: {users: UserListResolver}},
            { path: 'users/edit/:id', component: UserEditComponent, resolve: {user: UserEditResolver}},
            { path: 'departments', component: DepartmentListComponent, resolve: {departments: DepartmentListResolver}},
            { path: 'departments/edit/:id', component: DepartmentEditComponent, resolve: {department: DepartmentEditResolver}},
            // tslint:disable-next-line:max-line-length
            { path: 'deviceattributetypes', component: DeviceattributetypeListComponent, resolve: {deviceattributetypes: DeviceAttributeTypeListResolver}},
            // tslint:disable-next-line:max-line-length
            { path: 'deviceattributetypes/edit/:id', component: DeviceattributetypeEditComponent, resolve: {deviceattributetype: DeviceAttributeTypeEditResolver}},
            { path: 'devicedatetypes', component: DevicedatetypeListComponent, resolve: {devicedatetypes: DeviceDateTypeListResolver}},
            // tslint:disable-next-line:max-line-length
            { path: 'devicedatetypes/edit/:id', component: DevicedatetypeEditComponent, resolve: {devicedatetype: DeviceDateTypeEditResolver}},
            { path: 'devicestatuses', component: DevicestatusListComponent, resolve: {devicestatuses: DeviceStatusListResolver}},
            { path: 'devicestatuses/edit/:id', component: DevicestatusEditComponent, resolve: {devicestatus: DeviceStatusEditResolver}},
            { path: 'manufacturers', component: ManufacturerListComponent, resolve: {manufacturers: ManufacturerListResolver}},
            { path: 'manufacturers/edit/:id', component: ManufacturerEditComponent, resolve: {manufacturer: ManufacturerEditResolver}},
            // tslint:disable-next-line:max-line-length
            { path: 'productcapacities', component: ProductcapacityListComponent, resolve: {productcapacities: ProductcapacityListResolver}},
            // tslint:disable-next-line:max-line-length
            { path: 'productcapacities/edit/:id', component: ProductcapacityEditComponent, resolve: {productcapacity: ProductCapacityEditResolver}},
            { path: 'products', component: ProductListComponent, resolve: {products: ProductListResolver}},
            { path: 'products/edit/:id', component: ProductEditComponent, resolve: {product: ProductEditResolver}},
            { path: 'productmodels', component: ProductmodelListComponent, resolve: {productmodels: ProductModelListResolver}},
            { path: 'productmodels/edit/:id', component: ProductmodelEditComponent, resolve: {productmodel: ProductModelEditResolver}},
            { path: 'producttypes', component: ProducttypeListComponent, resolve: {producttypes: ProductTypeListResolver}},
            { path: 'producttypes/edit/:id', component: ProducttypeEditComponent, resolve: {producttype: ProductTypeEditResolver}},
            { path: 'simcards', component: SimcardListComponent, resolve: {simcards: SimcardListResolver}},
            { path: 'simcards/edit/:id', component: SimcardEditComponent, resolve: {simcard: SimcardEditResolver}}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];

