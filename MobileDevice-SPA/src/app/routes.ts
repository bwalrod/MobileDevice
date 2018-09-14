import { DepartmentEditResolver } from './_resolvers/department-edit.resolver';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { HomeComponent } from './home/home.component';
import { Routes } from '@angular/router';
import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { DepartmentListComponent } from './department-list/department-list.component';
import { DepartmentListResolver } from './_resolvers/department-list.resolver';
import { DepartmentEditComponent } from './department-edit/department-edit.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        children: [
            { path: 'users', component: UserListComponent, resolve: {users: UserListResolver}},
            { path: 'users/edit/:id', component: UserEditComponent, resolve: {user: UserEditResolver}},
            { path: 'departments', component: DepartmentListComponent, resolve: {departments: DepartmentListResolver}},
            { path: 'departments/edit/:id', component: DepartmentEditComponent, resolve: {department: DepartmentEditResolver}}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];

